using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //if (other.transform.parent.transform.parent.tag == "Ally" || other.transform.parent.transform.parent.tag == "Ennemy")
        //{
        //    Animator control = other.GetComponentInParent<Animator>();
        //    control.SetBool("onGround", true);
        //}
    }
}
