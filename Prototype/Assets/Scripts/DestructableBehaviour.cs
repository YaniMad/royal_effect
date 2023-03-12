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
    public int _lifePoints;
    public float attackRange;
    public float baseAttackRange;
    public float _attackSpeed;
    public int _attackDamage;

    public DestructableBehaviour selectedTarget;
    [SerializeField] public CharacterAnimation characterAnimation;
    [HideInInspector] public bool _isAttacking;

    public void Attack()
    {
        CharacterManager currentCM = selectedTarget.GetComponent<CharacterManager>();
        if(currentCM != null)
        {
            _isAttacking = true;
            transform.LookAt(selectedTarget.transform.position);
            currentCM._lifePoints -= _attackDamage;
            if (characterAnimation) characterAnimation.animator.SetBool("onRange", true);
            if (currentCM._lifePoints <= 0)
            {
                currentCM.Death();
                if (characterAnimation) characterAnimation.animator.SetBool("onRange", false);
            }
            if (characterAnimation) StartCoroutine(attackCooldown(characterAnimation.animator.runtimeAnimatorController.animationClips[2].length));
        }

        else if (currentCM == null)
        {
            attackRange = 10f;
            TowerManager currentTM = selectedTarget.GetComponent<TowerManager>();
            _isAttacking = true;
            currentTM._lifePoints -= _attackDamage;
            if (characterAnimation) characterAnimation.animator.SetBool("onRange", true);
            if (currentTM._lifePoints <= 0)
            {
                currentTM.Death();
            }
        }
        if (characterAnimation) StartCoroutine(attackCooldown(characterAnimation.animator.runtimeAnimatorController.animationClips[2].length));
    }

    private IEnumerator attackCooldown(float _cooldown)
    {
        yield return new WaitForSeconds(_cooldown); 
        _isAttacking = false;
    }

    public virtual void Death()
    {
        if (GetComponent<CharacterManager>())
        {
            GameManager.Instance.objects.Remove(GetComponent<CharacterManager>()); 
            Debug.Log("Delete from list");
        }   
        Destroy(gameObject);
    }
}
