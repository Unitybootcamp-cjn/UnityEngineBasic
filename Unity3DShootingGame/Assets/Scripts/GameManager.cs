using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    public PlayerMovement player;
    public Text hp_text;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        hp_text.text = $"HP : {player.hp}";
    }
}
