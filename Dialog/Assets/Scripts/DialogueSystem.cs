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
        // 텍스트, 이미지 큐 초기화
        sentences = new Queue<string>(dialogueData.lines);
        sprites = new Queue<Sprite>(dialogueData.images);

        nextButton.onClick.AddListener(OnNextButtonClicked);

        DisplayNext();
    }

    private void OnNextButtonClicked()
    {
        // 타이핑 중이면 즉시 완성
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

        // 텍스트
        currentSentence = sentences.Dequeue();
        typingCoroutine = StartCoroutine(TypeSentence(currentSentence));

        // 이미지
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
        // 대화 종료 시 추가 처리(이벤트 호출 등)
    }
}