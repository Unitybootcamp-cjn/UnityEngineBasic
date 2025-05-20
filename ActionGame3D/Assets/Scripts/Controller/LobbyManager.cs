using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public void OnGameStartButtonClick()
    {
        SceneManager.LoadScene("GameScene");
    }
}
