using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CardData", order = 1)]
public class CardData : ScriptableObject
{
    [Header("CardInfo")]
    public string cardName;
    public int cost;
    public DestructableBehaviour prefabToInstantiate;
    public Sprite icon;

    [Header("UnitInfo")]
    public int maxHealth;
}
