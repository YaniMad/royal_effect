using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /*private void OnTriggerStay(Collider other)
    {
        Debug.Log("En range");
        if (other.tag == "Ennemy" || other.tag == "EnnemyTower")
        {
            animator.SetBool("onRange", true);
        }
    }*/
}
