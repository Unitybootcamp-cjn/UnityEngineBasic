using TMPro;
using UnityEngine;

namespace Match3.InGame.LevelSystems
{
    public class ScoringText : MonoBehaviour
    {
        public int Score
        {
            get => _targetScore;
            set
            {
                _targetScore = value;
                _scoringSpeed = (_targetScore - _currentScore) / _duration;
            }
        }

        private int _targetScore;
        private int _currentScore;
        private float _scoringSpeed;
        [SerializeField] float _duration = 0.5f;
        [SerializeField] private TextMeshPro _score;

        private void Update()
        {
            if(_currentScore < _targetScore)
            {
                _currentScore += Mathf.RoundToInt(_scoringSpeed * Time.deltaTime);

                if(_currentScore >= _targetScore)
                {
                    _currentScore = _targetScore;
                }

                _score.text = _currentScore.ToString();
            }
        }
    }
}