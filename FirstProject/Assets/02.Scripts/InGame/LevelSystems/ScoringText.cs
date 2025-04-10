using System;
using TMPro;
using UnityEngine;

namespace Match3.InGame.LevelSystems
{
    /// <summary>
    /// 설정된 점수로 일정 시간에 걸쳐서 텍스트를 갱신
    /// </summary>
    public class ScoringText : MonoBehaviour
    {
        /// <summary>
        /// 텍스트에 설정하고 싶은 점수
        /// 목표점수 및 목표점수까지 갱신하기 위한 점수올라가는 속력 갱신
        /// </summary>
        public int Score
        {
            get => (int)_targetScore;
            set
            {
                _targetScore = value;
                _scoringSpeed = (_targetScore - _currentScore) / _duration;
            }
        }

        private double _targetScore; // 목표 점수
        private double _currentScore; // 현재 점수
        private double _scoringSpeed; // 현재 점수에서 목표 점수로 _duration 내에 올릴 속력
        [SerializeField] float _duration = 0.5f; // 현재 점수에서 목표점수까지 도달하는데 걸리는 시간
        [SerializeField] private TextMeshPro _score; // 점수 보여줄 텍스트

        private void Update()
        {
            // 아직 목표점수에 도달하지 못했다면
            if(_currentScore < _targetScore)
            {
                _currentScore += _scoringSpeed * Time.deltaTime; // 속력에 따라 현재점수 증가

                // 목표점수 도달
                if(_currentScore >= _targetScore)
                {
                    _currentScore = _targetScore;
                }

                _score.text = ((int)Math.Round(_currentScore)).ToString(); // 현재 점수로 텍스트 갱신
            }
        }
    }
}