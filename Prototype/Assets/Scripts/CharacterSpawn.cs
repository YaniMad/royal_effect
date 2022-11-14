using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterSpawn : MonoBehaviour
{
    public List<GameObject> SpawnPrefab = new List<GameObject>();
    public GameObject spawnPoint;
    public bool isRandomized;

    [SerializeField] private Transform spawnTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
