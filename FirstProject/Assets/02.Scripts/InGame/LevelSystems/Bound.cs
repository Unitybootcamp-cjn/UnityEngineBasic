using UnityEngine;
using UnityEngine.UIElements;

namespace Match3.InGame.LevelSystems
{
    public class Bound : MonoBehaviour
    {
        // TODO : 블록의 갯수와 크기에 따라서 상하좌우로 Bound의 크기를 늘리고 줄인다.
        // 받아야 할 파라미터 : sizex, sizey(블록의 갯수), width, height(블록의 크기)
        
        Map _map = new Map();
        Vector3 boundVector = new Vector3();

        private void Awake()
        {
            _map = FindAnyObjectByType<Map>(); // Map 클래스를 쓰는 오브젝트를 변수에 저장
        }

        private void Start()
        {
            float _transformX = (float)(_map.SizeX) * (_map.NodeWidth); // Map클래스의 SizeX(X축 블록의 갯수)와 NodeWidth(블록의 가로 길이)를 곱함.
            float _transformY = (float)(_map.SizeY) * (_map.NodeHeight);// Map클래스의 SizeY(Y축 블록의 갯수)와 NodeHeight(블록의 세로 길이)를 곱함.

            // 이 컴포넌트의 이름을 확인하는 조건문
            switch (transform.name)
            {
                case "Bound":
                    boundVector = new Vector3(-(_transformX / 2f + 0.5f - _map.BottomCenter.x),
                                         ((_transformY / 2f) + _map.BottomCenter.y),
                                         _map.BottomCenter.z); // 해당 Bound에 맞는 Vector3좌표 계산
                    transform.localScale = new Vector3(1f, _transformY, 1f); // 해당 Bound에 맞는 스케일값 계산
                    break;
                case "Bound1":
                    boundVector = new Vector3((_transformX / 2f + 0.5f + _map.BottomCenter.x),
                                         ((_transformY / 2f) + _map.BottomCenter.y),
                                         _map.BottomCenter.z);
                    transform.localScale = new Vector3(1f, _transformY, 1f);
                    break;
                case "Bound2":
                    boundVector = new Vector3(_map.BottomCenter.x,
                                          _map.BottomCenter.y - 0.5f,
                                          _map.BottomCenter.z);
                    transform.localScale = new Vector3(_transformX + 2, 1f, 1f);
                    break;
                case "Bound3":
                    boundVector = new Vector3(_map.BottomCenter.x,
                                         ((_transformY / 2f) + _map.BottomCenter.y) - 0.5f,
                                         _map.BottomCenter.z + 1);
                    transform.localScale = new Vector3(_transformX + 2, _transformY + 1, 1f);
                    break;
                default:
                    Debug.LogError("이 컴포넌트는 Bound 컴포넌트가 아닙니다");
                    break;
            }
            transform.position = boundVector; // 컴포넌트 위치 변경
        }
    }
}