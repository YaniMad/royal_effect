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
	[SerializeField] private Image icon;
	public Image outline;
	[SerializeField] private TextMeshProUGUI cardName;
	[SerializeField] private TextMeshProUGUI cost;
	[SerializeField] private PlayerStats playerStats;
	[SerializeField] private bool canDrag;
	[SerializeField] public Rigidbody rb;
	[SerializeField] public BoxCollider col;

	Vector3 originalPosition;
	Quaternion originalRotation;

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

	public Vector3 OriginalPosition
    {
		get { return originalPosition; }
		set { originalPosition = value; }
	}
	public Quaternion OriginalRotation
	{
		get { return originalRotation; }
		set { originalRotation = value; }
	}

	public void PlayCard(Vector3 _targetPosition)
    {
		SpawnUnit(_targetPosition);
		playerStats.DrawCard(originalPosition);
		transform.DOScale(transform.localScale.x+.05f, .2f).OnComplete(() =>
        {		
			transform.DOScale(0, .3f).OnComplete(() =>
			{
				transform.DOMove(_targetPosition, .3f).OnComplete(() =>
				{
					Destroy(gameObject);
				});
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
		if (isNextCard) col.enabled = false;
	}

	public void MoveToStartPosition()
    {
		transform.DOMove(originalPosition, 1f);
		transform.DORotateQuaternion(originalRotation, 1f).OnComplete(() =>
		{
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			if (GetComponent<Swish>() != null) GetComponent<Swish>().ResetAnimation();
		});
	}

    public void Update()
    {
		RaycastHit hit;
		if (Physics.Raycast(playerStats.rightController.transform.position, playerStats.rightController.transform.forward, out hit, Mathf.Infinity))
		{
			if (hit.transform.GetComponent<Card>() == this)
			{
				outline.color = Color.white;
			} else
            {
				outline.color = Color.clear;
            }
		} else
        {
			outline.color = Color.clear;
		}
	}
}
