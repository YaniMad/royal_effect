using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField]
    private List<DestructableBehaviour> objects;
    public PlayerStats playerStats;
    public UICtrl UICtrl;

    public static GameManager Instance
    {
        get { return instance; }
    }
    public List<DestructableBehaviour> Objects
    {
        get { return objects; }
    }

    private void Awake()
    {
        if(instance != this)
        {
            instance = this;
            objects = new List<DestructableBehaviour>();
        }
    }

    public static void AddObject(DestructableBehaviour go)
    {
        Instance.Objects.Add(go);
    }

    public void SpawnUnit(DestructableBehaviour _prefab, Transform _parent, Vector3 _pos, DestructableBehaviour.UnitSide _unitSide = DestructableBehaviour.UnitSide.Ally)
    {
        DestructableBehaviour _go = Instantiate(_prefab, _parent);
        if (_go == null)
        {
            return;
        }
        _go.unitSide = _unitSide;
        _go.transform.position = _pos;
        AddObject(_go);
    }
}
