using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableBehaviour : MonoBehaviour
{
    public enum UnitSide
    {
        Ally = 0,
        Ennemy = 1
    }

    public CardData cardData;
    public UnitSide unitSide;
    public int currentHealth;
    public float attackRange;
    public float baseAttackRange;
    public float _attackSpeed;
    public int _attackDamage;

    public DestructableBehaviour selectedTarget;
    [SerializeField] public CharacterAnimation characterAnimation;
    [HideInInspector] public bool _isAttacking;
    public UICharacterHealth healthBar;

    public virtual void Start()
    {
        currentHealth = cardData.maxHealth;
        healthBar.UpdateHealthBar();
    }

    public void Attack()
    {
        CharacterManager currentCM = selectedTarget.GetComponent<CharacterManager>();
        if(currentCM != null)
        {
            StartCoroutine(AttackRoutine(characterAnimation.animator.runtimeAnimatorController.animationClips[1].length));
        }
        else if (currentCM == null)
        {
            TowerManager currentTM = selectedTarget.GetComponent<TowerManager>();
            _isAttacking = true;
            currentTM.currentHealth -= _attackDamage;
            currentTM.healthBar.UpdateHealthBar();
            if (characterAnimation) characterAnimation.animator.SetBool("onRange", true);
            if (currentTM.currentHealth <= 0)
            {
                currentTM.Death();
            }
        }
        //if (characterAnimation) StartCoroutine(attack(characterAnimation.animator.runtimeAnimatorController.animationClips[1].length));
    }

    private IEnumerator AttackRoutine(float _cooldown)
    {
        _isAttacking = true;
        transform.LookAt(selectedTarget.transform.position);
        if (characterAnimation) characterAnimation.animator.SetBool("onRange", true);
        yield return new WaitForSeconds(_cooldown);
        selectedTarget.TakeDamage(_attackDamage);
        _isAttacking = false;
    }

    private void Death()
    {
        if (GetComponent<CharacterManager>())
        {
            GameManager.Instance.objects.Remove(GetComponent<CharacterManager>()); 
        }   
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar();
        if (currentHealth <= 0)
        {
            Death();
        }
    }
}
