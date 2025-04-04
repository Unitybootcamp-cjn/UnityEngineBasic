using System;
using UnityEngine;
using Random = UnityEngine.Random; // 모호한 참조에 대한 명시를 하는 using 키워드

namespace Match3.InGame.LevelSystems
{
    public class Map : MonoBehaviour
    {
        [SerializeField] int _sizeX = 8;
        [SerializeField] int _sizeY = 12;
        [SerializeField] float _nodeWidth = 1;
        [SerializeField] float _nodeHeight = 1;
        [SerializeField] Vector3 _bottomCentor;
        Node[,] _nodes;
        [SerializeField] GameObject[] _basicBlocks;

        private void Awake()
        {
            _nodes = new Node[_sizeY, _sizeX];
        }

        private void Start()
        {
            SetNodesRandomly();
        }

        public void SetNodesRandomly()
        {
            Array typeArray = Enum.GetValues(typeof(NodeTypes));
            int blockIndex;
            NodeTypes nodeType;
            int totalNodeType = typeArray.Length - 1;
            GameObject block;

            for (int i = 0; i < _nodes.GetLength(0); i++)
            {
                for (int j = 0; j < _nodes.GetLength(1); j++)
                {
                    blockIndex = Random.Range(0, totalNodeType);
                    nodeType = (NodeTypes)(1 << blockIndex);
                    _nodes[i, j] = new Node(j, i, nodeType);
                    block = Instantiate(_basicBlocks[blockIndex]);

                    block.transform.position
                        = new Vector3(x: (j - (_sizeX - 1) * 0.5f) * _nodeWidth,
                                      y: (i + 0.5f) * _nodeHeight,
                                      z: 0f)
                          + _bottomCentor;
                        
                }
            }
        }
    }
}