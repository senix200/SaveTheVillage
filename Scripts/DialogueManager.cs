using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text titleText;
    public Text dialogueText;
    public Image avatarImage;
    public bool dialogueActive;

    private string titleLine;
    public string[] dialogueLines;
    public int currentDialogueLine;

    private PlayerController playerController;


    private void Start()
    {
        dialogueActive = false;
        dialogueBox.SetActive(false);
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            currentDialogueLine++;


            if (currentDialogueLine >= dialogueLines.Length)
            {
                playerController.isTalking = false;
                currentDialogueLine = 0;
                dialogueActive = false;
                avatarImage.enabled = false;
                dialogueBox.SetActive(false);
            }
            else
            {
                titleText.text = titleLine;
                dialogueText.text = dialogueLines[currentDialogueLine];
            }
        }
    }

    public void ShowDialogue(string[] lines, string title)
    {
        currentDialogueLine = 0;
        dialogueLines = lines;
        dialogueActive = true;
        titleLine = title;
        dialogueBox.SetActive(true);
        titleText.text = titleLine;
        dialogueText.text = dialogueLines[currentDialogueLine];
        playerController.isTalking = true;
    }

    public void ShowDialogue(string[] lines, Sprite sprite, string title)
    {
        ShowDialogue(lines, title);
        avatarImage.enabled = true;
        avatarImage.sprite = sprite;
    }
}
