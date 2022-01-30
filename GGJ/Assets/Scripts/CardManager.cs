using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;

namespace shuffle_list
{
    static class ExtensionsClass
    {
        private static System.Random rng = new System.Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}

public class CardManager : MonoBehaviour
{
    [Header("prefab")]
    [SerializeField] private List<GameObject> prefabCards = new List<GameObject>();
    [SerializeField] private Canvas canvas;
    [SerializeField] private Sprite imageCard;

    [Header("Numbers For Cards Placement")]
    [SerializeField] private float StartX;
    [SerializeField] private float StartY;
    [SerializeField] private float DistanceBetweenX;
    [SerializeField] private float DistanceBetweenY;
    [SerializeField] private int nbCollone;
    [SerializeField] private float nbLigne;

    [Header("Numbers GamePlay")]
    [SerializeField] private float timeBeforeFlip;


    private List<GameObject> cards = new List<GameObject>();

    private GameObject gameObject = default;
    private float elapsedTime = 0f;


    private void Start()
    {
        for (int j = 0; j < nbLigne; j++)
        {
            shuffle_list.ExtensionsClass.Shuffle<GameObject>(prefabCards);

            for (int i = 0; i < nbCollone; i++)
            {
                gameObject = Instantiate(prefabCards[i]);
                gameObject.transform.SetParent(canvas.transform);
                gameObject.transform.position = new Vector3(StartX + (DistanceBetweenX * i), StartY + (DistanceBetweenY * j));

                cards.Add(gameObject);
            }
           
        }
    }

    private void Update()
    {
        if (elapsedTime >= timeBeforeFlip)
        {
            //flip
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].GetComponent<Image>().sprite = imageCard;
            }
        }
        else elapsedTime += Time.deltaTime;
    }
}

