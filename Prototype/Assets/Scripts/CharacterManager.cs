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

    public float _distDiff;
    

    

    //Cible
    private GameObject[] _target;
    [SerializeField] private string _tagToFollow;
    private Transform _targetPosition;
    

    //NavMesh Agent
    [SerializeField] private string towerTag;
    private GameObject _towerToDestroy;
    

    // Start is called before the first frame update
    void Start()
    {
        ReinitTarget();
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(selectedTarget);
        if(selectedTarget != null)
        {
            //_distDiff = Vector3.Distance(selectedTarget.transform.position, transform.position);
            Attack();
        }
        else
        {
            ReinitTarget();
        }
    }

   



    private void ReinitTarget()
    {
        selectedTarget = null;

        _target = GameObject.FindGameObjectsWithTag(_tagToFollow);
        

        float minDistance = 10000;

        foreach(GameObject currentTarget in _target)
        {
            float distDiff = Vector3.Distance(currentTarget.transform.position, transform.position);
            if (distDiff <= minDistance)
            {
                selectedTarget = currentTarget;
                minDistance = distDiff;
            }
            
        }


        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        if (selectedTarget != null)
        {
            agent.destination = selectedTarget.transform.position;
        }
        else if (selectedTarget == null)
        {
            agent.destination = _towerToDestroy.transform.position;
        }
    }
}
