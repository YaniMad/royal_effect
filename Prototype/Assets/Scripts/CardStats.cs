using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[System.Serializable]
public class CardStats
{
    [SerializeField]
	private int index;
    [SerializeField]
    private string cardName;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private int cost;
	[SerializeField]
	private GameObject prefab;

    public int Index
	{
		get { return index; }
		set { index = value; }
	}
	public string CardName
	{
		get { return cardName; }
		set { cardName = value; }
	}
   public Sprite Icon
	{
		get { return icon; }
		set { icon = value; }
	}
    public int Cost
	{
		get { return cost; }
		set { cost = value; }
	}
	public GameObject Prefab
	{
		get { return prefab; }
		set { prefab = value; }	
	}

}
