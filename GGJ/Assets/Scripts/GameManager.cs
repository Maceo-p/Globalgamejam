using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;
    }


    public Slider sliderLeft;
    public Slider sliderRight;
    public Image[] imagesShowed;
    public int[] id;
    public Sprite[] spritesList;
    public GameObject panelGameOver;

    public float gameTimer = 120f;
    public int randSign;
    private int previousRand;
    public int leftManID;
    public int rightManID;

    private float _damage = 10;
    public float GetSetDamage { get { return _damage; } set { _damage = value; } }

    private void Start()
    {
        panelGameOver.SetActive(false);
        gameTimer = CardManager.FindObjectOfType<CardManager>().timeBeforeFlip;
        sliderLeft.maxValue = 100;
        sliderLeft.value = 50;
        sliderRight.maxValue = 100;
        sliderRight.value = 50;
        randSign = Random.Range(0, 1);
        for(int i = 0; i < imagesShowed.Length; i++)
        {
            imagesShowed[i].enabled = false;
        }
    }

    private void Update()
    {
        gameTimer -= Time.deltaTime;
        
        if (gameTimer <= 0) // once the timer done (round over), pause until the next round
        {
            for (int i = 0; i < imagesShowed.Length; i++)
            {
                imagesShowed[i].enabled = false;
            }
            DialogManager.Instance.NextSentenceAtTheEndOfTimer();
        }

        if(sliderLeft.value >= sliderLeft.maxValue || sliderRight.value >= sliderRight.maxValue)
        {
            panelGameOver.SetActive(true);
            CardManager.isGameOver = true;
            Time.timeScale = 0;
        }
    }

    public void ChooseRandomSign()
    {
        for (int i = 0; i < imagesShowed.Length; i++)
        {
            leftManID = previousRand;

            imagesShowed[i].enabled = true;
            randSign = Random.Range(0, 10);
            while(randSign == previousRand)
            {
                randSign = Random.Range(0, 10);
            }
            imagesShowed[i].sprite = spritesList[randSign];
            previousRand = randSign;
            rightManID = randSign;
        }
    }   

    public void SliderUpdate(Slider losingPoints, Slider winningPoints, bool bothLoses)
    {
        if (!bothLoses)
        {
            losingPoints.value -= GetSetDamage;
            winningPoints.value += GetSetDamage;
        }
        else
        {
            losingPoints.value += GetSetDamage;
            winningPoints.value += GetSetDamage;
        }
    }

    private void OnGUI()
    {
        int minutes = Mathf.FloorToInt(gameTimer / 60F);
        int seconds = Mathf.FloorToInt(gameTimer - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        GUI.Label(new Rect(Screen.width / 2, 10, 400, 200), niceTime);
    }

}
