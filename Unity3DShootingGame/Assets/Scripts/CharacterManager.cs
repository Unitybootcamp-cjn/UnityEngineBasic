using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{

    public Material[] CharacterMaterial;
    public Button Leftbutton;
    public Button Rightbutton;
    public Button Startbutton;
    public int materialIndex = 0;

    public static CharacterManager instance = null;

    Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();

        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        renderer.material = CharacterMaterial[materialIndex];

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
    }

    public void OnRightButton()
    {
        materialIndex += 1;
    }
    public void OnStartButton()
    {
        SceneManager.LoadScene("GameScene");
    }
}
