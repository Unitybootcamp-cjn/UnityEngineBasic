using UnityEngine;
using UnityEngine.UIElements;

namespace Match3.InGame.LevelSystems
{
    public class Bound : MonoBehaviour
    {
        // TODO : 블록의 갯수와 크기에 따라서 상하좌우로 Bound의 크기를 늘리고 줄인다.
        // 받아야 할 파라미터 : sizex, sizey(블록의 갯수), width, height(블록의 크기)

        Transform _transform;
        float _transformY;
        float _transformX;
        Map _map = new Map();
        Vector3 boundVector = new Vector3();

        private void Awake()
        {
            _map = FindAnyObjectByType<Map>();
            _transform = GetComponent<Transform>();
        }

        private void Start()
        {
            _transformX = (float)(_map.SizeX) * (_map.NodeWidth);
            _transformY = (float)(_map.SizeY) * (_map.NodeHeight);
            
            if(_transform.name == "Bound")
            {
                boundVector = new Vector3(-(_transformX / 2f + 0.5f - _map.BottomCenter.x), ((_transformY / 2f) + _map.BottomCenter.y), _map.BottomCenter.z);
                this.transform.localScale = new Vector3(1f, _transformY, 1f);
            }
            else if(_transform.name == "Bound1")
            {
                boundVector = new Vector3((_transformX / 2f + 0.5f + _map.BottomCenter.x), ((_transformY / 2f) + _map.BottomCenter.y), _map.BottomCenter.z);
                this.transform.localScale = new Vector3(1f, _transformY, 1f);
            }
            else if( _transform.name == "Bound2")
            {
                boundVector = new Vector3(_map.BottomCenter.x, _map.BottomCenter.y - 0.5f, _map.BottomCenter.z);
                this.transform.localScale = new Vector3(_transformX + 2, 1f, 1f);
            }
            else if( _transform.name == "Bound3")
            {
                boundVector = new Vector3(_map.BottomCenter.x, ((_transformY / 2f) + _map.BottomCenter.y) - 0.5f, _map.BottomCenter.z + 1);
                this.transform.localScale = new Vector3(_transformX + 2, _transformY + 1, 1f);
            }
            else
            {
                Debug.LogError("이 컴포넌트는 Bound 컴포넌트가 아닙니다");
            }
            this.transform.position = boundVector;
        }
    }
}