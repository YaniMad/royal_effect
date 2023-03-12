using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterHealth : MonoBehaviour
{
    [SerializeField] private Image _healthBarImage;

    [SerializeField] private DestructableBehaviour _destructableBehaviour;

    public void UpdateHealthBar()
    {
        float currentHealth = _destructableBehaviour.currentHealth;
        float maxHealth = _destructableBehaviour.cardData.maxHealth;

        //Debug.Log($"{currentHealth} et {maxHealth}");

        _healthBarImage.fillAmount = currentHealth / maxHealth;
    }
}
