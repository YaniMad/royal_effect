using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerManager : DestructableBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void Death()
    {
        base.Death();
        GameManager.Instance.EndGame(unitSide);
    }
}
