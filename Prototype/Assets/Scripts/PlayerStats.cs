using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private GameObject cardPrefab;
    [SerializeField]
    private Transform handParent;
    [SerializeField]
    private Card nextCard;
    [SerializeField]
    private bool onDragging;
    [SerializeField]
    private Transform unitTransform;
     
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
        playersDeck.Start();
    }

    private void Update()
    {
        if(GetCurrentResources < GameConstants.RESOURCE_MAX + 1)
        {
            resources[GetCurrentResources].fillAmount = currentResources - GetCurrentResources;
            currentResources += Time.deltaTime * GameConstants.RESOURCE_SPEED;
        }

        UpdateText();
        UpdateDeck();
    }

    private void UpdateText()
    {
        textCurrentResources.text = GetCurrentResources.ToString();
        textMaxResources.text = (GameConstants.RESOURCE_MAX + 1).ToString();
    }

    private void UpdateDeck()
    {
        if(playersDeck.Hand.Count < GameConstants.MAX_HAND_SIZE)
        {
            CardStats cs = playersDeck.DrawCard();
            GameObject go = Instantiate(cardPrefab, handParent);
            Card c = go.GetComponent<Card>();
            c.PlayerInfo = this;
            c.CardInfo = cs;
        }

        nextCard.CardInfo = playersDeck.NextCard;
        nextCard.PlayerInfo = this;
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

