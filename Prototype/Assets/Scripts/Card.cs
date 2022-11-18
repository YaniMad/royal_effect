using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
	private CardStats cardInfo;
	[SerializeField]
	private Image icon;
	[SerializeField]
	private TextMeshProUGUI cardName;
	[SerializeField]
	private TextMeshProUGUI cost;
	[SerializeField]
	private PlayerStats playerInfo;
	[SerializeField]
	private bool canDrag;

	public CardStats CardInfo
	{
		get { return cardInfo; }
		set { cardInfo = value; }
	}
	public Image Icon 
	{ 
		get { return icon; } 
		set { icon = value; } 
	}
	public TextMeshProUGUI CardName
	{
		get { return cardName; }
		set { cardName = value; }
	}
	public TextMeshProUGUI Cost
	{
		get { return cost; }
		set { cost = value; }
	}
	public PlayerStats PlayerInfo
	{
		get { return playerInfo; }
		set { playerInfo = value; }
	}
	public bool CanDrag
	{
		get { return canDrag; }
		set { canDrag = value; }
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if(!playerInfo.OnDragging)
		{
			if(canDrag)
			{
				playerInfo.OnDragging = true;
				transform.SetParent(GameFunctions.GetCanvas());
			}
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if(playerInfo.OnDragging)
		{
			transform.position = Input.mousePosition;
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
        Debug.Log("END DRAG");
        GameObject go = eventData.pointerCurrentRaycast.gameObject;

		if(go != null)
		{
			SpawnUnit();
		}

		playerInfo.OnDragging = false;
	}

	private void SpawnUnit()
	{
		if(playerInfo.GetCurrentResources >= cardInfo.Cost)
		{
            Debug.Log("BEGIN SPAWNUNIT");
            playerInfo.PlayersDeck.RemoveHand(cardInfo.Index);
			playerInfo.RemoveResources(CardInfo.Cost);
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(cardInfo.Prefab.name);
            GameFunctions.SpawnUnit(cardInfo.Prefab, playerInfo.UnitTransform, pos);

            Debug.Log("DESTROY");

            Destroy(gameObject);
		}
	}

	private void Update()
	{
		icon.sprite = cardInfo.Icon;
		cardName.text = cardInfo.CardName;
		cost.text = cardInfo.Cost.ToString();
	}

}
