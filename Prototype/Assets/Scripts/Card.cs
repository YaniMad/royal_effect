using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Card : MonoBehaviour
{
	public CardData cardData;
	[SerializeField]
	private Image icon;
	public Image outline;
	[SerializeField]
	private TextMeshProUGUI cardName;
	[SerializeField]
	private TextMeshProUGUI cost;
	[SerializeField]
	private PlayerStats playerStats;
	[SerializeField]
	private bool canDrag;

	public Vector3 originalPosition;
	public Quaternion originalRotation;

	public bool isNextCard = false;

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
		get { return playerStats; }
		set { playerStats = value; }
	}
	public bool CanDrag
	{
		get { return canDrag; }
		set { canDrag = value; }
	}

	public void PlayCard(Vector3 _targetPosition)
    {
		SpawnUnit(_targetPosition);
		playerStats.DrawCard(originalPosition);
		transform.DOScale(transform.localScale.x+.05f, .2f).OnComplete(() =>
        {		
			transform.DOScale(0, .3f).OnComplete(() =>
			{
				Destroy(gameObject);
			});
		});

	}

	private void SpawnUnit(Vector3 _targetPosition)
	{
		if(playerStats.GetCurrentResources >= cardData.cost)
		{
            playerStats.PlayersDeck.RemoveHand(cardData);
			playerStats.RemoveResources(cardData.cost);
			GameManager.Instance.SpawnUnit(cardData.prefabToInstantiate, playerStats.UnitTransform, _targetPosition, cardData);
		}
	}

	public void SetCardContainerData()
    {
		icon.sprite = cardData.icon;
		cardName.text = cardData.cardName;
		cost.text = cardData.cost.ToString();
	}
}
