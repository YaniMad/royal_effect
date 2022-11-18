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

    }
}

