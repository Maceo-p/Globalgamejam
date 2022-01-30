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
    private AudioSource audioSource;
    private AudioClip audioClip;
    private int i = 0;
    private int j = 0;
    public float textSpeed = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioClip = GetComponent<AudioClip>();
    }

    public void NextSentenceAtTheEndOfTimer()
    {
        if(i >= dialogueLeft.sentences.Length)
        {
            i = 0;
        } 
        if (j >= dialogueRight.sentences.Length)
        {
            j = 0;
        }
        StopAllCoroutines();
        StartCoroutine(TextShowingUpCharByChar(dialogueLeft.sentences[i], dialogueRight.sentences[i]));
        GameManager.Instance.gameTimer = 10f;       
    }


    IEnumerator TextShowingUpCharByChar(string sentenceLeft, string sentenceRight)
    {
        textDialogueLeft.text = dialogueLeft.name + " :\n";
        textDialogueRight.text = dialogueRight.name + " :\n";
        audioClip = AudioManager.Instance.audioLeftMan[i];
        audioSource.clip = audioClip;
        audioSource.Play();
        foreach (char letter in sentenceLeft.ToCharArray())
        {
            textDialogueLeft.text += letter;
            yield return new WaitForSeconds(textSpeed);

        }
        audioSource.Stop();
        audioClip = AudioManager.Instance.audioRightMan[j];
        audioSource.clip = audioClip;
        audioSource.Play();

        foreach (char letter in sentenceRight.ToCharArray())
         {
             textDialogueRight.text += letter;
             yield return new WaitForSeconds(textSpeed);
         }
        audioSource.Stop();

        yield return new WaitForSeconds(0.2f);
        StartCoroutine(SignAppear());
        i++;
        j++;
    }
    IEnumerator SignAppear()
    {
        yield return new WaitForSeconds(0.2f);
        GameManager.Instance.ChooseRandomSign();
    }
}
