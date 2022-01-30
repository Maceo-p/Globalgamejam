using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

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
    [SerializeField] private Sprite imageDos;
    [SerializeField] private PointerEventData m_PointerEventData;
    [SerializeField] private EventSystem m_EventSystem;

    [Header("Numbers For Cards Placement")]
    [SerializeField] private float StartX;
    [SerializeField] private float StartY;
    [SerializeField] private float DistanceBetweenX;
    [SerializeField] private float DistanceBetweenY;
    [SerializeField] private int nbCollone;
    [SerializeField] private float nbLigne;

    [Header("Numbers GamePlay")]
    public float timeBeforeFlip;

    public static bool isGameOver = false;


    private List<GameObject> cards = new List<GameObject>();

    private GameObject gameObject = default;
    private float elapsedTime = 0f;
    private bool firstFlip = true;

    private GraphicRaycaster m_Raycaster;
    private Coroutine currentCoroutine = null;

    private AudioSource cardFlipping;



    private void Start()
    {
        cardFlipping = GetComponent<AudioSource>(); 
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
        m_Raycaster = canvas.GetComponent<GraphicRaycaster>();
    }

    private void Update()
    {
        if (elapsedTime >= timeBeforeFlip && firstFlip)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].GetComponent<Image>().sprite = imageDos;
            }
            firstFlip = false;
        }
        else elapsedTime += Time.deltaTime;


        if (Input.GetMouseButtonDown(0))
        {
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                //Debug.Log("Hit " + result.gameObject.name);

                if (currentCoroutine == null)
                {
                    currentCoroutine = StartCoroutine(FlipCoroutine(result.gameObject));
                }

            }
        }

        if (isGameOver)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].SetActive(false);
            }
        }


        //Vector3 mousePos = Input.mousePosition;
        //Vector3 dir = mousePos - Camera.main.WorldToScreenPoint(transform.position);
        //RaycastHit2D hit = Physics2D.Raycast(canvas.transform.position, dir);
        //Debug.DrawRay(canvas.transform.position, dir, Color.blue, 4f);
    }

    public IEnumerator FlipCoroutine(GameObject card)
    {
        card.GetComponent<Image>().sprite = card.GetComponent<Card>().originalSprite;
        cardFlipping.Play();

        if (card.GetComponent<Card>().id == GameManager.Instance.leftManID || card.GetComponent<Card>().id == GameManager.Instance.rightManID)
        {
            Debug.Log("Same ID");
            for (int i = 0; i < GameManager.Instance.imagesShowed.Length; i++)
            {
                if(GameManager.Instance.imagesShowed[i].sprite == card.GetComponent<Image>().sprite)
                {
                    Debug.Log("Same sprite");
                    GameManager.Instance.SliderUpdate(GameManager.Instance.sliderRight, GameManager.Instance.sliderLeft, true);
                    break;
                }
                else
                {
                    if (GameManager.Instance.leftManID == card.GetComponent<Card>().id)
                    {
                        GameManager.Instance.SliderUpdate(GameManager.Instance.sliderLeft, GameManager.Instance.sliderRight, false);
                        DialogManager.Instance.NextSentenceAtTheEndOfTimer();
                    }
                    if (GameManager.Instance.rightManID == card.GetComponent<Card>().id)
                    {
                        GameManager.Instance.SliderUpdate(GameManager.Instance.sliderRight, GameManager.Instance.sliderLeft, false);
                        DialogManager.Instance.NextSentenceAtTheEndOfTimer();
                    }

                }
            }
        } else
        {
            Debug.Log("Not Same ID");

            GameManager.Instance.SliderUpdate(GameManager.Instance.sliderRight, GameManager.Instance.sliderLeft, true);
        }

        yield return new WaitForSeconds(3f);

        card.GetComponent<Image>().sprite = imageDos;

        currentCoroutine = null;
    }
}

