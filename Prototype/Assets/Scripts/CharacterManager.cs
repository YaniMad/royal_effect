using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CharacterManager : DestructableBehaviour
{
    //Stats
    [SerializeField] private int _manaCost;    
    
    //Cible
    private List<DestructableBehaviour> _target = new List<DestructableBehaviour>();

    void Start()
    {
        ReinitTarget();
    }

    void Update()
    {
        if(selectedTarget != null)
        {
            float distDiff = Vector3.Distance(selectedTarget.transform.parent.transform.position, transform.position);
            if (!_isAttacking && distDiff <= attackRange)
            {
                GetComponentInParent<NavMeshAgent>().destination = transform.position;
                Attack();
            } else if (!_isAttacking && distDiff > attackRange)
            {
                GetComponentInParent<NavMeshAgent>().destination = selectedTarget.transform.parent.transform.position;
            }
        }
        else
        {
            ReinitTarget();
        }
    }

    private void ReinitTarget()
    {
        selectedTarget = null;

        GetComponent<CharacterAnimation>().animator.SetBool("onRange", false);

        //_target = GameObject.FindGameObjectsWithTag(_tagToFollow);
        _target = FindObjectsOfType<DestructableBehaviour>().ToListPooled();

        float minDistance = 10000;

        _target = _target.FindAll(x => x.unitSide != unitSide);
        foreach(DestructableBehaviour currentTarget in _target)
        {
            Debug.Log(currentTarget.name);
            float distDiff = Vector3.Distance(currentTarget.transform.position, transform.position);
            //if (currentTarget.unitSide == unitSide) break;
            if (distDiff <= minDistance)
            {
                selectedTarget = currentTarget;
                minDistance = distDiff;
            }
        }
        //if (selectedTarget == null)
        //{
        //    selectedTarget = _towerToDestroy;
        //    //agent.destination = selectedTarget.transform.position;
        //}
    }
}
