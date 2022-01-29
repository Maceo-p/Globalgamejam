using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    private static DialogManager _instance = null;
    public static DialogManager Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;
    }

    public DialogData dialogueLeft;
    public DialogData dialogueRight;
    public Text textDialogueLeft;
    public Text textDialogueRight;
    private int i = 0;
    private int j = 0;

    public void NextSentenceAtTheEndOfTimer()
    {
        if(i >= dialogueLeft.sentences.Length)
        {
            i = 0;
        } else if (j >= dialogueRight.sentences.Length)
        {
            j = 0;
        }

        textDialogueRight.text = dialogueRight.sentences[i];
        textDialogueLeft.text = dialogueLeft.sentences[i];
        StartCoroutine(SignAppear());
        GameManager.Instance.gameTimer = 6f;
        i++;
        j++;
    }

    IEnumerator SignAppear()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.ChooseRandomSign();
    }
}
