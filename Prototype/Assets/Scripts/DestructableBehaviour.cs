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
            _isAttacking = true;
            transform.LookAt(selectedTarget.transform.position);
            currentCM.currentHealth -= _attackDamage;
            currentCM.healthBar.UpdateHealthBar();
            if (characterAnimation) characterAnimation.animator.SetBool("onRange", true);
            if (currentCM.currentHealth <= 0)
            {
                currentCM.Death();
                if (characterAnimation) characterAnimation.animator.SetBool("onRange", false);
            }
            if (characterAnimation) StartCoroutine(attackCooldown(characterAnimation.animator.runtimeAnimatorController.animationClips[1].length));
        }
        else if (currentCM == null)
        {
            Debug.Log("TOWER ATTACK");
            //attackRange = 10f;
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
        if (characterAnimation) StartCoroutine(attackCooldown(characterAnimation.animator.runtimeAnimatorController.animationClips[1].length));
    }

    private IEnumerator attackCooldown(float _cooldown)
    {
        yield return new WaitForSeconds(_cooldown); 
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
}
