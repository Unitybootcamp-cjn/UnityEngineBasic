using UnityEngine;
using UnityEngine.UIElements;

namespace Match3.InGame.LevelSystems
{
    public class Bound : MonoBehaviour
    {
        // TODO : 블록의 갯수와 크기에 따라서 상하좌우로 Bound의 크기를 늘리고 줄인다.
        // 받아야 할 파라미터 : sizex, sizey(블록의 갯수), width, height(블록의 크기)

        Transform _transform;
        float _transformX;
        float _transformY;
        Map _map = new Map();
        GameObject _bound1;
        Vector3 bound1Vector = new Vector3();


        private void Start()
        {
            _transformX = _transformX = (float)(_map._sizeX) * (_map._nodeWidth);
            _transformY = (float)(_map._sizeY) * (_map._nodeHeight);
            bound1Vector = new Vector3(-(_transformX/2f + 0.5f), 0f, 0f);
            Debug.Log($"{_transformX} {_transformY}");

            this.transform.position = bound1Vector;
            this.transform.localScale = new Vector3(1f, _transformY, 1f);
        }
    }
}