using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableBehaviour : MonoBehaviour
{
    public int _lifePoints;
    public float attackRange;
    public float _attackSpeed;
    public int _attackDamage;

    public GameObject selectedTarget;

    private bool _isAttacking;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        float distDiff = Vector3.Distance(selectedTarget.transform.position, transform.position);
      
            CharacterManager currentCM = selectedTarget.GetComponent<CharacterManager>();
            if(currentCM != null)
            {
                if (distDiff <= attackRange && !_isAttacking)
                {
                attackRange = attackRange;
                    _isAttacking = true;
                    transform.LookAt(selectedTarget.transform.position);
                    currentCM._lifePoints -= _attackDamage;

                    Invoke(nameof(ResetAttack), _attackSpeed);

                if (currentCM._lifePoints <= 0)
                    {
                        Death();

                    }
                }
                    
            }

            else if(currentCM == null)
            {
            
                attackRange = 10f;
                TowerManager currentTM = selectedTarget.GetComponent<TowerManager>();

                if(distDiff <= attackRange && !_isAttacking)
                {
                    _isAttacking = true;
                    currentTM._lifePoints -= _attackDamage;

                    Invoke(nameof(ResetAttack), _attackSpeed);
                if (currentTM._lifePoints <= 0)
                    {
                        Death();
                    }
                }    
            }
            
            
    }

    private void ResetAttack()
    {
        _isAttacking = false;
    }

    private void Death()
    {

        Destroy(selectedTarget);
        // selectedTarget = null;
    }
}
