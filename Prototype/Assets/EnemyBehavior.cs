using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    [SerializeField] DestructableBehaviour.UnitSide unitSide;
    [SerializeField] List<CardData> unitsToSummon = new List<CardData>();
    [SerializeField] float currentResources;
    [SerializeField] EnemySpawnZone spawnZone;

    void Start()
    {
        
    }

    void Update()
    {
        currentResources += Time.deltaTime * GameConstants.RESOURCE_SPEED;

        if (currentResources >= unitsToSummon[0].cost)
        {
            Vector3 _pos = new Vector3(Random.Range(spawnZone.transform.position.x + spawnZone.size.x / 2, spawnZone.transform.position.x - spawnZone.size.x / 2), 0,
            Random.Range(spawnZone.transform.position.z + spawnZone.size.y / 2, spawnZone.transform.position.z - spawnZone.size.y / 2));
            GameManager.Instance.SpawnUnit(unitsToSummon[0].prefabToInstantiate, transform, _pos, unitSide);
            currentResources -= unitsToSummon[0].cost;
            unitsToSummon.Add(unitsToSummon[0]);
            unitsToSummon.Remove(unitsToSummon[0]);
        }
    }
}
