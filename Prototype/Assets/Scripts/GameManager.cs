using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField]
    private List<GameObject> objects;
    public PlayerStats playerStats;
    public UICtrl UICtrl;

    public static GameManager Instance
    {
        get { return instance; }
    }
    public List<GameObject> Objects
    {
        get { return objects; }
    }

    private void Awake()
    {
        if(instance != this)
        {
            instance = this;
            objects = new List<GameObject>();
        }
    }

    public static void AddObject(GameObject go)
    {
        Instance.Objects.Add(go);
    }

    public void SpawnUnit(GameObject _prefab, Transform _parent, Vector3 _pos, CardData _data)
    {
        GameObject _go = Instantiate(_prefab, _parent);
        if (_go == null)
        {
            return;
        }
        _go.GetComponentInChildren<CharacterManager>().cardData = _data;
        _go.transform.position = _pos;
        AddObject(_go);
    }
}
