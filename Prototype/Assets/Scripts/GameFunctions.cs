using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFunctions 
{
    public static Transform GetCanvas()
    {
        return GameObject.Find(GameConstants.HUD_CANVAS).transform;
    }

    public static void SpawnUnit(GameObject prefab, Transform parent, Vector3 pos)
    {
        Debug.Log("GF " + prefab.name);
        GameObject go = GameObject.Instantiate(prefab, parent);
        if (go == null)
        {
            Debug.Log("Instantiate failed");
            return;
        }
       // go.transform.position = new Vector3(pos.x, 4.6f, pos.z);
        GameManager.AddObject(go);
    }
}
