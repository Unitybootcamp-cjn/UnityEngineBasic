using UnityEngine;

namespace Test
{
    public class Test_InputManager : MonoBehaviour
    {
        [SerializeField] Transform _target;
        [SerializeField] float _speed = 2f;
        bool _isDragging;

        // Input 클래스는 Update에서만 쓰는 것
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Space Down");
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("Space Press");
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                Debug.Log("Space Up");
            }

            //HandleTarget();
        }

        void HandleTarget()
        {
            float h = Input.GetAxisRaw("Horizontal"); // 수평축입력(왼쪽 오른쪽 방향키)
            float v = Input.GetAxisRaw("Vertical"); // 수직축입력(위쪽 아래쪽 방향키)
            Vector3 direction = new Vector3(h, 0f, v).normalized; //단위벡터 구하기
            Debug.Log($"Direction : {h} {v}");
            Vector3 velocity = direction * _speed; // 속도 구하기
            Vector3 deltaPosition = velocity * Time.deltaTime; // 현재 프레임 위치 변화량 구하기
            _target.Translate(deltaPosition); // 위치 변환
        }

        /// <summary>
        /// GetMouseButtonDown(0)이 마우스 왼쪽
        /// GetMouseButtonDown(1)이 마우스 오른쪽
        /// GetMouseButtonDown(2)이 마우스 스크롤
        /// </summary>
        void MouseDrag()
        {
            if (_isDragging)
            {
                if(Input.GetMouseButtonUp(0) == false)
                {
                    _isDragging = false;
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _isDragging = true;
                }
            }
        }
    }
}

