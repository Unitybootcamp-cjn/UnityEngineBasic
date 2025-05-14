using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("== UI Component ==")]
    public TextMeshProUGUI currentScoreUI;
    public TextMeshProUGUI highScoreUI;
    public TextMeshProUGUI coinUI;
    public TextMeshProUGUI bombUI;

    [Header("== Fields ==")]
    public int currentScore;
    public int highScore;
    public int coin = 0;
    public int bomb = 0;

    //점수에 대한 프로퍼티 설계
    public int Score
    {
        get
        {
            return currentScore;
        }
        set
        {
            currentScore = value;
            currentScoreUI.text = "Current Score : " + currentScore;

            if(currentScore > highScore)
            {
                highScore = currentScore;
                highScoreUI.text = "High Score : " + highScore;

                PlayerPrefs.SetInt("HIGH_SCORE", highScore);
                PlayerPrefs.Save();
            }
        }
    }

    public void SetScoreText()
    {
        currentScoreUI.text = "Current Score : " + currentScore;
        highScoreUI.text = "High Score : " + PlayerPrefs.GetInt("HIGH_SCORE");
        coinUI.text = coin.ToString();
        bombUI.text = bomb.ToString();
    }

    #region 싱글톤
    public static ScoreManager instance = null;

    private void Awake()
    {
        highScore = PlayerPrefs.GetInt("HIGH_SCORE");
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion


}
