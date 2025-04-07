using Unity.Mathematics;
using UnityEngine;

namespace Match3.InGame.LevelSystems
{
    public class MapBound : MonoBehaviour
    {
        [SerializeField] Map _map;
        [SerializeField] Transform _background;
        [SerializeField] Transform _bottom;
        [SerializeField] Transform _left;
        [SerializeField] Transform _right;

        private void OnEnable()
        {
            _map.OnMapCreated += ResizeBounds;
        }

        private void OnDisable()
        {
            _map.OnMapCreated -= ResizeBounds;
        }

        void Start()
        {

        }

        void ResizeBounds(int x, int y, float width, float height, Vector3 bottomCenter)
        {
            _background.localScale = new Vector3(x * width + 2f, y * height + 1, 1f);
            _background.position = bottomCenter + Vector3.up * (y * height - 1) * 0.5f + Vector3.forward;

            _bottom.localScale = new Vector3(x * width + 2f, 1f, 1f);
            _bottom.position = bottomCenter + Vector3.down * 0.5f;

            _left.localScale = new Vector3(1f, y * height, 1f);
            _left.position = bottomCenter + Vector3.left * (x * width + 1f) * 0.5f + Vector3.up * (y * height * 0.5f);

            _right.localScale = new Vector3(1f, y * height, 1f);
            _right.position = bottomCenter + Vector3.right * (x * width + 1f) * 0.5f + Vector3.up * (y * height * 0.5f);
        }
    }
}

