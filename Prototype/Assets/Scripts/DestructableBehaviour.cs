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

    public UnitSide unitSide;
    public int _lifePoints;
    public float attackRange;
    public float baseAttackRange;
    public float _attackSpeed;
    public int _attackDamage;

    public DestructableBehaviour selectedTarget;

    [HideInInspector] public bool _isAttacking;
    public void Attack()
    {
        CharacterManager currentCM = selectedTarget.GetComponentInChildren<CharacterManager>();
        if(currentCM != null)
        {
            //if (distDiff <= attackRange)
            //{
                _isAttacking = true;
                //attackRange = baseAttackRange;
                transform.LookAt(selectedTarget.transform.position);
                currentCM._lifePoints -= _attackDamage;
                GetComponent<CharacterAnimation>().animator.SetBool("onRange", true);
                Debug.Log(this.name + "attaque" + currentCM.name);

                //Invoke(nameof(ResetAttack), _attackSpeed);
                if (currentCM._lifePoints <= 0)
                {
                    Debug.Log("kills enemy");
                    Death();
                    GetComponent<CharacterAnimation>().animator.SetBool("onRange", false);
                }
                StartCoroutine(attackCooldown(GetComponent<CharacterAnimation>().animator.runtimeAnimatorController.animationClips[2].length));
            //}
        }

            else if(currentCM == null)
            {
            
                attackRange = 10f;
                TowerManager currentTM = selectedTarget.GetComponentInChildren<TowerManager>();

                //if(distDiff <= attackRange && !_isAttacking)
                //{
                    _isAttacking = true;
                    currentTM._lifePoints -= _attackDamage;
                GetComponent<CharacterAnimation>().animator.SetBool("onRange", true);
                //Invoke(nameof(ResetAttack), _attackSpeed);
                    if (currentTM._lifePoints <= 0)
                        {
                            Death();
                        }
                    }
                    StartCoroutine(attackCooldown(GetComponent<CharacterAnimation>().animator.runtimeAnimatorController.animationClips[2].length));
        //}


    }

    private IEnumerator attackCooldown(float _cooldown)
    {
        yield return new WaitForSeconds(_cooldown); 
        _isAttacking = false;
        Debug.Log("can attack again");
    }

    private void Death()
    {

        Destroy(selectedTarget.transform.parent.gameObject);
    }
}
