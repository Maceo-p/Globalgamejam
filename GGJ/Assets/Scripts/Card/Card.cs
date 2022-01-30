using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int id;
    [SerializeField] public Sprite originalSprite;
    [SerializeField] private Image image;


    public void Reveal()
    {
        image.sprite = originalSprite;
    }

}
