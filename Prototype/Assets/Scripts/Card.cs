using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public CardData cardData;
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

	public CardData CardInfo
	{
		get { return cardData; }
		set { cardData = value; }
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
				if (playerInfo.CheckIfEnoughRessource(cardData.cost))
                {
					playerInfo.OnDragging = true;
					transform.SetParent(GameManager.Instance.UICtrl.transform);
				}
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
        GameObject go = eventData.pointerCurrentRaycast.gameObject;
		if(go != null)
		{
			SpawnUnit();
		} else
        {
			Debug.Log("NOT ENOUGH ELIXIR");
		}
		playerInfo.OnDragging = false;
	}

	private void SpawnUnit()
	{
		if(playerInfo.GetCurrentResources >= cardData.cost)
		{
            playerInfo.PlayersDeck.RemoveHand(cardData);
			playerInfo.RemoveResources(cardData.cost);
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.transform.position, Camera.main.ScreenPointToRay(Input.mousePosition).direction, out hit, Mathf.Infinity))
			{
				Vector3 pos = hit.point;
				GameManager.Instance.SpawnUnit(cardData.prefabToInstantiate, playerInfo.UnitTransform, pos);
				Destroy(gameObject);
			}
		}
	}

	private void Start()
	{
		SetCardContainerData();
	}

	public void SetCardContainerData()
    {
		icon.sprite = cardData.icon;
		cardName.text = cardData.cardName;
		cost.text = cardData.cost.ToString();
	}

}
