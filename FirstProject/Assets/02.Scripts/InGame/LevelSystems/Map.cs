using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Random = UnityEngine.Random; // 모호한 참조에 대한 명시를 하는 using 키워드

namespace Match3.InGame.LevelSystems
{
    public class Map : MonoBehaviour
    {
        [SerializeField] int _sizeX = 8;
        public int SizeX => _sizeX;
        [SerializeField] int _sizeY = 12;
        public int SizeY => _sizeY;
        [SerializeField] float _nodeWidth = 1;
        public float NodeWidth => _nodeWidth;
        [SerializeField] float _nodeHeight = 1;
        public float NodeHeight => _nodeHeight;
        [SerializeField] Vector3 _bottomCenter;
        public Vector3 BottomCenter => _bottomCenter;

        Node[,] _nodes;
        [SerializeField] GameObject[] _basicBlocks;
        Camera _camera;
        Vector3 _leftBottom;
        Vector3 _rightTop;
        float _boundLeft;
        float _boundBottom;
        float _boundRight;
        float _boundTop;

        [SerializeField] Vector3 _oscillationAmplitude;
        [SerializeField] Vector3 _oscillationVelocity;
        List<(int, int)> _selectedIndices;

        public event Action<int, int, float, float, Vector3> OnMapCreated;

        private void Awake()
        {
            _nodes = new Node[_sizeY, _sizeX];
            _selectedIndices = new List<(int, int)>(_sizeX * _sizeY);

            _leftBottom = new Vector3(x: (0 - (_sizeX - 1) * 0.5f) * _nodeWidth,
                                             y: (0 + 0.5f) * _nodeHeight,
                                             z: 0f) +
                                             _bottomCenter;

            _rightTop = new Vector3(x: ((_sizeX - 1) - (_sizeX - 1) * 0.5f) * _nodeWidth,
                                           y: ((_sizeY - 1) + 0.5f) * _nodeHeight,
                                           z: 0f) +
                                           _bottomCenter;

            _boundLeft = _leftBottom.x - _nodeWidth * 0.5f;
            _boundBottom = _leftBottom.y - _nodeHeight * 0.5f;
            _boundRight = _rightTop.x + _nodeWidth * 0.5f;
            _boundTop = _rightTop.y + _nodeHeight * 0.5f;
        }

        private void Start()
        {
            _camera = Camera.main;
            SetNodesRandomly();
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition); // 스크린에 터치한 위치에서 쏘는 선
                Plane plane = new Plane(Vector3.forward, _bottomCenter); // 퍼즐맵이 존재하는 가상의 평면 데이터

                if(plane.Raycast(ray, out float distance))
                {
                    Vector3 position = ray.GetPoint(distance);

                    (int x, int y) index = GetIndexFromPosition(position);

                    if(index.x >= 0 &&
                       index.y >= 0)
                    {
                        Debug.Log($"Seleted {index.x} {index.y}");
                        SelectIndex(index.x, index.y);
                    }
                    
                }
            }
            OscillateSelectedBlocks();
        }

        (int x,int y) GetIndexFromPosition(Vector3 position)
        {
            if(position.x < _boundLeft || position.x > _boundRight ||
               position.y <_boundBottom || position.y > _boundTop)
            {
                return (-1, -1);
            }

            int x = Mathf.RoundToInt(((position.x - _leftBottom.x) / _nodeWidth));
            int y = Mathf.RoundToInt(((position.y - _leftBottom.y) / _nodeHeight));

            return (x, y);
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
                    block = Instantiate(_basicBlocks[blockIndex]);
                    block.transform.position
                        = new Vector3(x: (j - (_sizeX - 1) * 0.5f) * _nodeWidth,
                                      y: (i + 0.5f) * _nodeHeight,
                                      z: 0f)
                          + _bottomCenter;

                    _nodes[i, j] = new Node(j, i, nodeType, block.transform);
                }
            }

            // 대리자호출시 () 로 직접 호출보다 Invoke 를 쓰는 이유는
            // 1. 제3자가 이 변수의 정의를 피킹하지 않아도 대리자라는 것을 알 수 있다.
            // 2. 필요시 ? Null Cheak 연산자 사용 편의도 있다.
            OnMapCreated?.Invoke(_sizeX, _sizeY, _nodeWidth, _nodeHeight, _bottomCenter);

            //OnMapCreated();
        }

        void SelectIndex(int x,int y)
        {
            _selectedIndices.Add((x, y));
        }

        void DeselectIndex(int x,int y)
        {
            if (_selectedIndices.Remove((x, y)))
            {
                _nodes[y,x].Block.rotation = Quaternion.identity;
            }
        }

        void OscillateSelectedBlocks()
        {
            float angleX = Mathf.Sin(Time.time * _oscillationVelocity.x) * _oscillationAmplitude.x;
            float angleY = Mathf.Sin(Time.time * _oscillationVelocity.y) * _oscillationAmplitude.y;
            float angleZ = Mathf.Sin(Time.time * _oscillationVelocity.z) * _oscillationAmplitude.z;
            Transform block;

            foreach ((int x, int y) index in _selectedIndices)
            {
                block = _nodes[index.y, index.x].Block;
                block.rotation = Quaternion.Euler(angleX, angleY, angleZ);
            }
        }

        bool CheckMatch(int x, int y,List<(int x,int y)> appendedResults)
        {
            bool result = false;
            // 현재 노드 타입
            NodeTypes currentTypes = _nodes[y, x].TypeFlags;

            List<(int x, int y)> tempTrackingList = new List<(int x, int y)>(16);
            int i = y;
            int j = x;

            // 위아래 3개 이상 매치 탐색

            // 위쪽 탐색
            i = y + 1; // 한 칸 위
            
            while (i < _sizeY) // 위쪽 맵 경계
            {
                // 한칸 위의 블록이 같은 타일이면
                if(_nodes[i,j].TypeFlags == currentTypes)
                {
                    tempTrackingList.Add((j, i)); // 추적대상등록
                }
                else
                {
                    break; // 다른 색 나오면 위쪽 탐색 끝
                }
                i++;// 한칸 위로
            }

            // 아래쪽 탐색
            i = y - 1; // 한 칸 아래로
            while (i >= 0) // 아래쪽 맵 경계  
            {
                // 한칸 아래의 블록이 같은 타일이면
                if (_nodes[i, j].TypeFlags == currentTypes)
                {
                    tempTrackingList.Add((j, i)); // 추적대상등록
                }
                else
                {
                    break; // 다른 색 나오면 아래쪽 탐색 끝
                }
                i--; // 한 칸 아래로
            }
            
            // 오른쪽 탐색
            j = x + 1; // 한 칸 오른쪽으로
            while (j < _sizeX) // 오른쪽 맵 경계
            {
                // 한칸 오른쪽 블록이 같은 타일이면
                if (_nodes[i, j].TypeFlags == currentTypes)
                {
                    tempTrackingList.Add((j, i)); // 추적대상등록
                }
                else
                {
                    break; // 다른 색 나오면 오른쪽 탐색 끝
                }
                i++; // 한칸 오른쪽으로
            }

            // 왼쪽 탐색
            j = x - 1; // 한 칸 왼쪽으로
            while (j >= 0) // 왼쪽 맵 경계
            {
                // 한칸 왼쪽 블록이 같은 타일이면
                if (_nodes[i, j].TypeFlags == currentTypes)
                {
                    tempTrackingList.Add((j, i)); // 추적대상등록
                }
                else
                {
                    break; // 다른 색 나오면 왼쪽 탐색 끝
                }
                i--; // 한칸 왼쪽으로
            }


            if (tempTrackingList.Count >= 2)
            {
                appendedResults.AddRange(tempTrackingList); // 매치조건된 블록들을 결과에 붙임
                result = true;
            }

            tempTrackingList.Clear();

            // 매치 조건이 하나라도 있다면 현재 위치도 결과에 추가해야한다 (현재위치도 파괴해야하니까)
            if (result)
            {
                appendedResults.Add((x, y));
            }
            return result;
        }
    }
}