using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public PlayerMovement player;
    public Text hp_text;

    [SerializeField]
    private GameObject gameOverPanel;

    private int coin = 0;

    [HideInInspector]
    public bool isGameOver = false;

    private void Awake()
    {
        gameOverPanel.SetActive(false);
        ScoreManager.instance.SetScoreText();
    }
    void Start()
    {

        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        hp_text.text = $"HP : {player.hp}";
    }

    public void SetGameOver()
    {
        isGameOver = true;
        ShowGameOverPanel();
    }

    void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnExitButtonEnter()
    {
#if UNITY_EDITOR // ����Ƽ ������ �ʿ����� �۾�
        UnityEditor.EditorApplication.isPlaying = false;
        //������ �ٷ� ������ ���(�����, �����)
#else
        Application.Quit(); // ���� ��Ȱ��ȭ�Ǵ� �ڵ尡 �ٷ� ����
#endif
    }
}
