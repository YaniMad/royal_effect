using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CharacterManager : DestructableBehaviour
{   
    //Cible
    private List<DestructableBehaviour> _target = new List<DestructableBehaviour>();
    public NavMeshAgent navMeshAgent;

    public override void Start()
    {
        base.Start();
        ReinitTarget();
    }

    void Update()
    {
        if(selectedTarget != null)
        {
            float distDiff = Vector3.Distance(selectedTarget.transform.position, transform.position);
            if (!_isAttacking && distDiff <= navMeshAgent.stoppingDistance)
            {
                navMeshAgent.destination = transform.position;
                Attack();
            } else if (!_isAttacking && distDiff > navMeshAgent.stoppingDistance)
            {
                navMeshAgent.destination = selectedTarget.transform.position;
            }
        }
        else
        {
            ReinitTarget();
        }
    }

    public void ReinitTarget()
    {
        selectedTarget = null;

        if (characterAnimation) characterAnimation.animator.SetBool("onRange", false);
     
        _target = FindObjectsOfType<DestructableBehaviour>().ToListPooled();

        float minDistance = 10000;

        _target = _target.FindAll(x => x.unitSide != unitSide);
        foreach(DestructableBehaviour currentTarget in _target)
        {
            float distDiff = Vector3.Distance(currentTarget.transform.position, transform.position);                       
            if (distDiff <= minDistance)
            {
                selectedTarget = currentTarget;
                minDistance = distDiff;
            }
        }

    }
}
