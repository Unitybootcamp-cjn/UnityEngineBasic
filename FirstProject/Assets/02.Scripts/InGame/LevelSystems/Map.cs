using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;
using Random = UnityEngine.Random; // 모호한 참조에 대한 명시를 하는 using 키워드

namespace Match3.InGame.LevelSystems
{
    public class Map : MonoBehaviour
    {
        [Header("Map spec")]
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

        [Header("Physics")]
        [SerializeField] Vector3 _gravity = new Vector3(0f, -4f, 0f);
        int _animationCount;

        [Header("Oscillation animation")]
        [SerializeField] Vector3 _oscillationAmplitude;
        [SerializeField] Vector3 _oscillationVelocity;
        List<(int x, int y)> _oscillatingIndices;
        List<(int x, int y)> _matchResults;

        [Header("Swap Failed animation")]
        [SerializeField] float _swapFailedAnimSpeed = 2f;

        [Header("Swipe spec")]
        [SerializeField] float _swipeMinDelta; // Swipe 동작으로 인식하기위한 최소 드래그 거리
        Vector3 _dragBeginPosition;
        bool _isDragging;

        (int x, int y) _selectedIndex = (-1, -1);

        bool IsSelected => _selectedIndex.Item1 >= 0 && _selectedIndex.Item2 >= 0;

        public event Action<int, int, float, float, Vector3> OnMapCreated;

        private void Awake()
        {
            _nodes = new Node[_sizeY, _sizeX];
            _oscillatingIndices = new List<(int, int)>(_sizeX * _sizeY);
            _matchResults = new List<(int, int)>(_sizeX * _sizeY);

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
            HandleInput();
            OscillateSelectedBlocks();
        }

        void HandleInput()
        {
            if (_animationCount > 0)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                OnMouseButton0Down();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                OnMouseButton0Up();
            }
            else if (Input.GetMouseButton(0))
            {
                OnMouseButton0();
            }
        }

        void OnMouseButton0Down()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition); // 스크린에 터치한 위치에서 쏘는 선
            Plane plane = new Plane(Vector3.forward, _bottomCenter); // 퍼즐맵이 존재하는 가상의 평면 데이터

            // plane과 ray의 교차점을 찾고, ray.Origin에서 교차점까지의 거리를 반환
            if (plane.Raycast(ray, out float distance))
            {
                Vector3 position = ray.GetPoint(distance); // ray직선 위에서 교차점까지 떨어진 거리만큼 떨어진 위치의 점을 받아옴(교차점 받아옴)

                (int x, int y) index = GetIndexFromPosition(position);

                if(IsValid(index.x, index.y))
                {
                    _isDragging = true;
                    _dragBeginPosition = Input.mousePosition;
                    Debug.Log($"Seleted {index.x} {index.y}");
                    SelectIndex(index.x, index.y);
                }
            }
        }

        void OnMouseButton0Up()
        {
            DeselectIndex();
        }

        void OnMouseButton0()
        {
            if (IsSelected == false)
                return;

            Vector3 swipe = Input.mousePosition - _dragBeginPosition;

            if(swipe.magnitude >= _swipeMinDelta)
            {
                float theta = Mathf.Atan2(swipe.y , swipe.x);
                float degree = Mathf.Rad2Deg * theta;
                int dirX = 0;
                int dirY = 0;

                // 오른쪽
                if(degree >= -30f && 
                   degree <= +30f)
                {
                    dirX = 1;
                }
                //위쪽
                else if(degree >= +60f &&
                        degree <= +120f)
                {
                    dirY = 1;
                }
                //왼쪽
                else if(degree >= +150f ||
                        degree <= -150f)
                {
                    dirX = -1;
                }
                //아래쪽
                else if(degree <= -60f &&
                        degree >= -120f)
                {
                    dirY = -1;
                }
                else
                {
                    DeselectIndex();
                    return;
                }

                StartCoroutine(C_TrySwap(_selectedIndex.x, _selectedIndex.y, _selectedIndex.x + dirX, _selectedIndex.y + dirY));

                DeselectIndex();
            }
        }

        IEnumerator C_TrySwap(int x1, int y1, int x2, int y2)
        {
            bool result = false; // 스왑 가능한지 저장하는 변수

            // 좌표 유효한지
            if(IsValid(x2,y2) == false ||
               IsValid(x1,y1) == false)
            {
                yield break;
            }
                
            // 원래 노드값 저장
            Node node1 = _nodes[y1, x1];
            Node node2 = _nodes[y2, x2];

            // 스왑
            _nodes[y1, x1] = node2;
            _nodes[y2, x2] = node1;

            _matchResults.Clear();
            if (CheckMatch(x1, y1, _matchResults))
            {
                result = true;
            }

            if (CheckMatch(x2, y2, _matchResults))
            {
                result = true;
            }

            // 스왑가능하면 매치조건 맞는거 삭제하면서 터뜨리는 애니메이션 진행
            if(result)
            {
                yield return StartCoroutine(C_SwapSuccessedAnimation(x1, y1, x2, y2));

                // y축 아래서부터 y축 위로 올라가면서 순회를 해야하기 때문에 터뜨려야하는 인뎃스를 오름차순 정렬해야함.
                _matchResults.Sort((index1, index2) =>
                {

                    if (index1.y > index2.y)
                        return 1;
                    else
                        return -1;
                });

                // 얘네들 파괴해야하니까 건들지말라고 마킹함   
                for (int i = 0; i < _matchResults.Count; i++)
                {
                    Destroy(_nodes[_matchResults[i].y, _matchResults[i].x].Block.gameObject);
                    _nodes[_matchResults[i].y, _matchResults[i].x].IsScheduledForDestroy = true;
                    _nodes[_matchResults[i].y, _matchResults[i].x].Block = null;
                }



                for (int i = 0; i < _matchResults.Count; i++)
                {
                    // 파괴한 블록 위부터 젤 위까지 탐색하면서 파괴된 자리에 떨어뜨릴 블록 찾음
                    for (int row = _matchResults[i].y + 1; row < _sizeY; row++)
                    {
                        int col = _matchResults[i].x;
                        // 얘도 파괴됐으니까 다음꺼 찾아야함
                        if (_nodes[row, col].IsScheduledForDestroy)
                            continue;

                        // 떨어질 블록 찾음
                        float fallingDistance = row - _matchResults[i].y * _nodeHeight;
                        Vector3 end = GetPositionFromIndex(col, _matchResults[i].y);
                        Vector3 start = end + Vector3.up * fallingDistance;
                        _nodes[_matchResults[i].y, col].Block = _nodes[row, col].Block;
                        _nodes[row, col].Block = null;

                        StartCoroutine(C_FallingAnimation(_nodes[_matchResults[i].y, col].Block, start, end));
                    }
                }


                // TODO : 전체 맵을 순회하지 않고 필요한 노드만 순회할 수 있게끔 코드 최적화
                for (int col = 0; col < _sizeX; col++)
                {
                    int emptyNodes = 0;

                    for (int row = 0; row < _sizeY; row++)
                    {
                        // 빈칸이면 메모
                        if (_nodes[row, col].IsScheduledForDestroy)
                        {
                            emptyNodes++;
                        }
                        // 빈칸 아니면 여태까지 빈칸 갯수만큼 아래로 떨어뜨림
                        else if (emptyNodes > 0)
                        {
                            int targetRow = row - emptyNodes;
                            _nodes[targetRow, col] = _nodes[row, col];
                            _nodes[targetRow, col].IsScheduledForDestroy = false;
                            _nodes[row, col].Block = null;
                            Vector3 start = GetPositionFromIndex(col, row);
                            Vector3 end = GetPositionFromIndex(col, targetRow);
                            StartCoroutine(C_FallingAnimation(_nodes[targetRow, col].Block, start, end));
                        }
                    }

                    // 남은 빈칸 채워주기
                    for (int i = 0; i < emptyNodes; i++)
                    {
                        int targetRow = _sizeY - emptyNodes + i;

                        Vector3 start = GetPositionFromIndex(col, _sizeY - 1) + Vector3.up * (i + 1) * _nodeHeight;
                        Vector3 end = GetPositionFromIndex(col, targetRow);
                        Transform block = SpawnRandomBlock();
                        block.position = start;

                        _nodes[targetRow, col].Block = block;
                        _nodes[targetRow, col].IsScheduledForDestroy = false;
                        StartCoroutine(C_FallingAnimation(block, start, end));
                    }
                }
            }
            // 스왑 불가능하면 스왑실패를 알리는 애니메이션 진행
            else
            {
                // 스왑 안되니까 데이터 원상복구
                _nodes[y1, x1] = node1;
                _nodes[y2, x2] = node2;

                // 스왑 실패 애니메이션 실행
                StartCoroutine(C_SwapFailedAnimation(x1, y1, x2, y2));
            }
        }

        IEnumerator C_FallingAnimation(Transform block, Vector3 start, Vector3 end)
        {
            _animationCount++;

            Vector3 velocity = Vector3.zero;

            while (block.position.y > end.y)
            {
                velocity += _gravity * Time.deltaTime;
                block.Translate(velocity * Time.deltaTime);

                yield return null;
            }

            block.position = end;

            _animationCount--;
        }

        
        IEnumerator C_SwapFailedAnimation(int x1,int y1, int x2, int y2)
        {
            _animationCount++;
            Node node1 = _nodes[y1, x1];
            Node node2 = _nodes[y2, x2];
            Vector3 node1Position = node1.Block.position;
            Vector3 node2Position = node2.Block.position;
            float elapsedTime = 0; // 경과시간

            while(_swapFailedAnimSpeed * 2f * elapsedTime < 1f)
            {
                node1.Block.transform.position = Vector3.Lerp(node1Position, node2Position, _swapFailedAnimSpeed * 2f * elapsedTime);
                node2.Block.transform.position = Vector3.Lerp(node2Position, node1Position, _swapFailedAnimSpeed * 2f * elapsedTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            elapsedTime = 0;

            while(_swapFailedAnimSpeed * 2f * elapsedTime < 1f)
            {
                node1.Block.transform.position = Vector3.Lerp(node2Position, node1Position, _swapFailedAnimSpeed * 2f * elapsedTime);
                node2.Block.transform.position = Vector3.Lerp(node1Position, node2Position, _swapFailedAnimSpeed * 2f * elapsedTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            node1.Block.transform.position = node1Position;
            node2.Block.transform.position = node2Position;
            _animationCount--;
        }
        IEnumerator C_SwapSuccessedAnimation(int x1, int y1, int x2, int y2)
        {
            _animationCount++;
            Node node1 = _nodes[y1, x1];
            Node node2 = _nodes[y2, x2];

            Vector3 node1Position = node1.Block.position;
            Vector3 node2Position = node2.Block.position;
            float elapsedTime = 0; // 경과시간

            while (_swapFailedAnimSpeed * 2f * elapsedTime < 1f)
            {
                node1.Block.transform.position = Vector3.Lerp(node1Position, node2Position, _swapFailedAnimSpeed * 2f * elapsedTime);
                node2.Block.transform.position = Vector3.Lerp(node2Position, node1Position, _swapFailedAnimSpeed * 2f * elapsedTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            node1.Block.transform.position = node2Position;
            node2.Block.transform.position = node1Position;
            _animationCount--;
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

        Vector3 GetPositionFromIndex(int x,int y)
        {
            return new Vector3(x: (x - (_sizeX - 1) * 0.5f) * _nodeWidth,
                               y: (y + 0.5f) * _nodeHeight,
                               z: 0f)
                   + _bottomCenter;

        }

        Transform SpawnRandomBlock()
        {
            Array typeArray = Enum.GetValues(typeof(NodeTypes));
            int blockIndex;
            NodeTypes nodeType;
            int totalNodeType = typeArray.Length - 1;
            GameObject block;

            blockIndex = Random.Range(0, totalNodeType);
            nodeType = (NodeTypes)(1 << blockIndex);
            block = Instantiate(_basicBlocks[blockIndex]);

            return block.transform;
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

                    _nodes[i, j] = new Node(nodeType, block.transform);
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
            _selectedIndex = (x, y);
            _oscillatingIndices.Add((x, y));
        }

        void DeselectIndex()
        {
            if (_oscillatingIndices.Remove(_selectedIndex))
            {
                _nodes[_selectedIndex.y,_selectedIndex.x].Block.rotation = Quaternion.identity;
            }
            _selectedIndex = (-1, -1);
        }

        bool IsValid(int x,int y)
        {
            return y >= 0 && y < _nodes.GetLength(0) &&
                   x >= 0 && x < _nodes.GetLength(1);
        }

        void OscillateSelectedBlocks()
        {
            float angleX = Mathf.Sin(Time.time * _oscillationVelocity.x) * _oscillationAmplitude.x;
            float angleY = Mathf.Sin(Time.time * _oscillationVelocity.y) * _oscillationAmplitude.y;
            float angleZ = Mathf.Sin(Time.time * _oscillationVelocity.z) * _oscillationAmplitude.z;
            Transform block;

            foreach ((int x, int y) index in _oscillatingIndices)
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

            if (tempTrackingList.Count >= 2)
            {
                appendedResults.AddRange(tempTrackingList); // 매치조건된 블록들을 결과에 붙임
                result = true;
            }

            tempTrackingList.Clear();

            i = y;
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
                j++; // 한칸 오른쪽으로
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
                j--; // 한칸 왼쪽으로
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