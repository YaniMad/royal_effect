using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private Deck playersDeck;
    [SerializeField]
    private List<Image> resources;
    [SerializeField]
    private TextMeshProUGUI textCurrentResources;
    [SerializeField]
    private TextMeshProUGUI textMaxResources;
    [SerializeField]
    private float currentResources;
    [SerializeField]
    private Card cardPrefab;
    [SerializeField]
    private Transform handParent;
    [SerializeField]
    private Card nextCard;
    [SerializeField]
    private bool onDragging;
    [SerializeField]
    private Transform unitTransform;
    public Transform cam;
    public Card currentGrabbedCard;

    [Header("XR SETTINGS")]
    [SerializeField] public GameObject rightController;
    [SerializeField] public GameObject leftController;

    public Deck PlayersDeck
    {
        get { return playersDeck; }
    }
    public List<Image> Resources
    {
        get { return resources; }
    }
    public TextMeshProUGUI TextCurrentResources
    {
        get { return textCurrentResources; }
    }
    public TextMeshProUGUI TextMaxResources
    {
          get { return textMaxResources; }
    }
   public int GetCurrentResources
    {
        get
        {
            return (int)currentResources;
        }
    }
    public float CurrentResources
    {
        get { return currentResources; }  
        set { currentResources = value; }  
    }
    public bool OnDragging
    {
        get { return onDragging; }
        set { onDragging = value; }
    }
    public Transform UnitTransform
    {
        get { return unitTransform; }
        set { unitTransform = value; }
    }

    private void Start()
    {
        nextCard.cardData = playersDeck.DrawCard();
        nextCard.PlayerInfo = this;
        nextCard.SetCardContainerData();
        for (int i = 0; i < GameConstants.MAX_HAND_SIZE; i++)
        {
            DrawCard(new Vector3(handParent.position.x + i, handParent.position.y, handParent.position.z));
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("XRI_Right_TriggerButton") || Input.GetButtonDown("XRI_Left_TriggerButton"))
        {
            GetCard();
        }
        if (Input.GetButtonUp("XRI_Right_TriggerButton") || Input.GetButtonUp("XRI_Left_TriggerButton"))
        {
            if (currentGrabbedCard == null) return;
            UseCard();
        }

        if (GetCurrentResources < GameConstants.RESOURCE_MAX + 1)
        {
            resources[GetCurrentResources].fillAmount = currentResources - GetCurrentResources;
            currentResources += Time.deltaTime * GameConstants.RESOURCE_SPEED;
        }
        UpdateText();
    }

    private void GetCard()
    {
        RaycastHit hit;
        if (Physics.Raycast(rightController.transform.position, rightController.transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.transform.GetComponent<Card>() != null)
            {
                currentGrabbedCard = hit.transform.GetComponent<Card>();
            }
        }
    }

    private void UseCard()
    {
        RaycastHit hit;
        if (Physics.Raycast(rightController.transform.position, rightController.transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.transform.GetComponent<TriggerDetection>() != null)
            {
                currentGrabbedCard.PlayCard(hit.point);
            }
            else
            {
                currentGrabbedCard.MoveToStartPosition();
                currentGrabbedCard = null;
            }
        }
        else
        {
            currentGrabbedCard.MoveToStartPosition();
            currentGrabbedCard = null;
        }
    }

    private void UpdateText()
    {
        textCurrentResources.text = GetCurrentResources.ToString();
        textMaxResources.text = (GameConstants.RESOURCE_MAX + 1).ToString();
    }

    public void DrawCard(Vector3 _pos)
    {
        CardData _cardData = playersDeck.DrawCard();
        Card _card = Instantiate(cardPrefab, handParent);
        _card.PlayerInfo = this;
        _card.cardData = _cardData;
        _card.transform.position = _pos;
        _card.SetCardContainerData();
        _card.OriginalPosition = _pos;
        _card.OriginalRotation = _card.transform.rotation;
    }

    public void RemoveResources(int cost)
    {
        currentResources -= cost;
        for (int i = 0; i < resources.Count; i++)
        {
            resources[i].fillAmount = 0;
            if(i <= GetCurrentResources)
            {
                resources[i].fillAmount = 1;
            }
        }
    }

    public bool CheckIfEnoughRessource(int cost)
    {
        if (currentResources - cost >= 0) return true;
        else return false;
    }
}

