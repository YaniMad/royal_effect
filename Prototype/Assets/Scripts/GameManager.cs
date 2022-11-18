using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField]
    private List<GameObject> objects;

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
}
