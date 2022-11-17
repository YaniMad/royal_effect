using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterSpawn : MonoBehaviour
{
    public List<GameObject> SpawnPrefab = new List<GameObject>();
    public GameObject spawnPoint;
    public bool isRandomized;

    //Drag&Drop
    private GameObject selectedObject;

    [SerializeField] private Transform spawnTransform;
    

    // Update is called once per frame
    void Update()
    {
      if(Input.GetMouseButtonDown(0))
        {

        }

      if(selectedObject != null)
        {

        }
    }

    /*private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
    }*/
    public void SummonTroops()
    {
        int index = isRandomized ? Random.Range(0, SpawnPrefab.Count) : 0;
        if (SpawnPrefab.Count > 0)
        {
            //Instantiate(SpawnPrefab[index], spawnPoint.transform.position, transform.rotation);
            Instantiate(SpawnPrefab[index], spawnTransform);    
        }

    }
}
