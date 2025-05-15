using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [Header("Pause UI Panel")]
    [Tooltip("ESC 눌렀을 때 켜질 UI Panel")]
    public GameObject pausePanel;

    bool isPaused = false;

    void Start()
    {
        // 시작할 때 반드시 꺼두기
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    void Update()
    {
        // ESC 키 누를 때마다 토글
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void OnResumeButton()
    {
        ResumeGame();
    }

    void PauseGame()
    {
        // 시간 멈추기
        Time.timeScale = 0f;
        // 물리 업데이트도 멈추고 싶으면 (기본 FixedDeltaTime 비율로 자동 처리됨)
        // Audio도 함께 멈추기
        AudioListener.pause = true;
        // UI 보이기
        if (pausePanel != null)
            pausePanel.SetActive(true);

        isPaused = true;
    }

    void ResumeGame()
    {
        // 시간 흐르게
        Time.timeScale = 1f;
        // Audio도 재생
        AudioListener.pause = false;
        // UI 숨기기
        if (pausePanel != null)
            pausePanel.SetActive(false);

        isPaused = false;
    }
}