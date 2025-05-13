using UnityEngine;
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
        EnemyManager enemymanager = FindObjectOfType<EnemyManager>();
        if (enemymanager != null)
        {
            enemymanager.StopEnemyRoutine();
        }

        Invoke("ShowGameOverPanel", 1f);
    }

    void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);

    }
}
