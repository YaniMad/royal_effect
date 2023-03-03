using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterHealth : MonoBehaviour
{
    [SerializeField] private Image _healthBarImage;

    [SerializeField] private DestructableBehaviour _destructableBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        _healthBarImage.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        int currentHealth = _destructableBehaviour._lifePoints;
        int maxHealth = _destructableBehaviour.cardData.maxHealth;

        Debug.Log($"{currentHealth} et {maxHealth}");

        _healthBarImage.fillAmount = (float)currentHealth / maxHealth;
    }
}
