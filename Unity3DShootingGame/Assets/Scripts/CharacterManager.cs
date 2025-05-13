using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{

    public Material[] CharacterMaterial;
    public Button Leftbutton;
    public Button Rightbutton;
    public GameObject CheckUI;
    public GameObject Character;
    Renderer renderer;

    public int materialIndex = 0;

    public static CharacterManager instance = null;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        renderer = Character.GetComponent<Renderer>();

        CheckUI.SetActive(false);
    }

    void Update()
    {
        if (materialIndex == 0)
        {
            Leftbutton.interactable = false;
            return;
        }
        else
        {
            Leftbutton.interactable = true;
        }
        if (materialIndex == CharacterMaterial.Length - 1)
        {
            Rightbutton.interactable = false;
            return;
        }
        else
        {
            Rightbutton.interactable = true;
        }
    }

    public void OnLeftButton()
    {
        materialIndex -= 1;
        renderer.material = CharacterMaterial[materialIndex];
    }

    public void OnRightButton()
    {
        materialIndex += 1;
        renderer.material = CharacterMaterial[materialIndex];
    }

    public void OnGameStartButton()
    {
        CheckUI.SetActive(true);
    }

    public void OnYesButton()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void OnNoButton()
    {
        CheckUI.SetActive(false);
    }
}
