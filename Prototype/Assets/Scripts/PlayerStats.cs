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
    private TextMeshPro textCurrentResources;
    [SerializeField]
    private TextMeshPro textMaxResources;
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
    public TextMeshPro TextCurrentResources
    {
        get { return textCurrentResources; }
    }
    public TextMeshPro TextMaxResources
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
        
    }
}

