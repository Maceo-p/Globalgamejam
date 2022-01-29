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
    public float gameTimer = 120f;
   

    private void Start()
    {
        sliderLeft.maxValue = 100;
        sliderLeft.value = 50;
        sliderRight.maxValue = 100;
        sliderRight.value = 50;
        DialogManager.Instance.NextSentenceAtTheEndOfTimer();
        
    }

    private void Update()
    {
        gameTimer -= Time.deltaTime;
        
        if (gameTimer <= 0)
        {
            DialogManager.Instance.NextSentenceAtTheEndOfTimer();
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
