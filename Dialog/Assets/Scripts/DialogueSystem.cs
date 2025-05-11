using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [Header("Data & UI References")]
    public DialogueData dialogueData;
    public Text dialogueText;
    public Image dialogueImage;
    public Button nextButton;

    [Header("Typing Settings")]
    public float typingSpeed = 0.05f;

    private Queue<string> sentences;
    private Queue<Sprite> sprites;
    private Coroutine typingCoroutine;
    private string currentSentence;

    void Start()
    {
        // �ؽ�Ʈ, �̹��� ť �ʱ�ȭ
        sentences = new Queue<string>(dialogueData.lines);
        sprites = new Queue<Sprite>(dialogueData.images);

        nextButton.onClick.AddListener(OnNextButtonClicked);

        DisplayNext();
    }

    private void OnNextButtonClicked()
    {
        // Ÿ���� ���̸� ��� �ϼ�
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = currentSentence;
            typingCoroutine = null;
            return;
        }

        DisplayNext();
    }

    private void DisplayNext()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // �ؽ�Ʈ
        currentSentence = sentences.Dequeue();
        typingCoroutine = StartCoroutine(TypeSentence(currentSentence));

        // �̹���
        if (sprites.Count > 0)
        {
            Sprite nextSprite = sprites.Dequeue();
            dialogueImage.sprite = nextSprite;
            dialogueImage.preserveAspect = true;
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char c in sentence)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        typingCoroutine = null;
    }

    private void EndDialogue()
    {
        dialogueText.text = "";
        nextButton.interactable = false;
        // ��ȭ ���� �� �߰� ó��(�̺�Ʈ ȣ�� ��)
    }
}