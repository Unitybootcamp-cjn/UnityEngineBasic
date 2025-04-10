using TMPro;
using UnityEngine;

namespace Match3.InGame.LevelSystems
{
    public class InGameScore : MonoBehaviour
    {
        [SerializeField] ScoringText _scoringText;
        [SerializeField] TextMeshPro _comboStack;
        [SerializeField] Map _map;

        private void Start()
        {
            _map.OnScoreChanged += (score, comboStack) =>
            {
                _scoringText.Score = score;

                if (comboStack > 0)
                    _comboStack.text = (comboStack + 1).ToString();
                else
                    _comboStack.text = string.Empty;
            };
        }
    }
}