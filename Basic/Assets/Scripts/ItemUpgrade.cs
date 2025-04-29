using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemUpgrade : MonoBehaviour
{
    public UnityEvent upgrade;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI failText;
    public TextMeshProUGUI successText;
    public TextMeshProUGUI buttonText;
    public Button upgradeButton;
    public Image itemImage;

    float success = 90;
    float fail = 10;
    int upgradeLevel = 0;
    string itemName = "SWORD";

    void upgradeText()
    {
        successText.text = $"{success}%";
        failText.text = $"{fail}%";
        itemNameText.text = $"+{upgradeLevel} {itemName}";
        buttonText.text = "UPGRADE";
    }
    void tryUpgrade()
    {
        int upgradeIndex = Random.Range(0, 100);
        if(upgradeLevel == 10)
        {
            success = 90;
            fail = 10;
            upgradeLevel = 0;
            upgradeText();
        }
        else
        {
            if (upgradeIndex <= success)
            {
                Debug.Log("강화성공");
                success -= 0;
                fail += 0;
                upgradeLevel++;
                upgradeText();
                if (upgradeLevel == 10)
                {
                    successText.text = "Clear!";
                    failText.text = "Clear!";
                    itemNameText.text = "Clear!";
                    buttonText.text = "Retry";
                    //upgrade.RemoveAllListeners();
                    //upgrade = null;
                }
            }
            else
            {
                Debug.Log("강화실패");
                success = 90;
                fail = 10;
                upgradeLevel = 0;
                upgradeText();
            }
        }
        
    }
    private void Start()
    {
        upgradeText();
        upgrade.AddListener(tryUpgrade);
        upgradeButton.onClick.AddListener(upgrade.Invoke);
    }
}
