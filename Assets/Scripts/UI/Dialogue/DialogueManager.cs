using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Inspired by: https://www.youtube.com/watch?v=_nRzoTzeyxU&t=6s
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    void Awake() { if (!Instance) Instance = this; }

    public TextMeshProUGUI dialogueText;

    private Queue<string> sentences;

    private DialoguesJSONParser.Dialogues dialogues;

    void Start()
    {
        sentences = new Queue<string>();
        dialogues = DialoguesJSONParser.Instance.dialoguesJson;
        QueueStartOfGameDialogue();
        TriggerDialogue();
    }

    void OnEnable()
    {
        GameStateManager.OnStateChange += StateChangeHandler;
    }

    void OnDisable()
    {
        GameStateManager.OnStateChange -= StateChangeHandler;
    }

    void StateChangeHandler(GameState newState)
    {
        switch (newState)
        {
            case GameState.PRE_ROUND:
                QueueStartOfRoundDialogue();
                break;
            default:
                break;
        }
        if (sentences.Count > 0)
        {
            TriggerDialogue();
        }
    }

    void TriggerDialogue()
    {
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        GameStateManager.Instance.EndDialogue();
    }

    void QueueStartOfGameDialogue()
    {
        int id = Random.Range(0, 3);
        string[] dlg = new string[] { };
        if (id == 0) dlg = dialogues.startOfGame[0].option_1;
        if (id == 1) dlg = dialogues.startOfGame[0].option_2;
        if (id == 2) dlg = dialogues.startOfGame[0].option_3;
        foreach (var sentence in dlg) sentences.Enqueue(sentence);
    }

    void QueueStartOfRoundDialogue()
    {
        int id = Random.Range(0, dialogues.startOfRound.Length);
        sentences.Enqueue(dialogues.startOfRound[id]);
    }

    void QueueFinalWaveDialogue(bool weaponUnlocked) { }

    void QueueEndGameDialogue(bool victory) { }
}
