using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField]
    public List<CharacterManager> objects;
    public PlayerStats playerStats;
    public UICtrl UICtrl;

    public static GameManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if(instance != this)
        {
            instance = this;
            //objects = FindObjectsOfType<CharacterManager>();

        }

        UICtrl._endGameUI.SetActive(false);
    }

    private void Start()
    {
        CharacterManager[] _characters = FindObjectsOfType<CharacterManager>();
        for (int i = 0; i < _characters.Length; i++)
        {
            objects.Add(_characters[i]);
        }
    }

    public static void AddObject(CharacterManager go)
    {
        Instance.objects.Add(go);
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
        UpdateAllUnitsTargets();
        AddObject(_go.GetComponent<CharacterManager>());
        
    }

    public void UpdateAllUnitsTargets()
    {
        foreach(CharacterManager _destructable in objects)
        {
            _destructable.ReinitTarget();
        }
    }

    public void EndGame(TowerManager.UnitSide _unitSide)
    {

        UICtrl._endGameUI.SetActive(true);

        UICtrl._endGameUI.transform.DOMoveY(15, 1)
            .SetEase(Ease.InCubic)
            .OnComplete(() => {
                Time.timeScale = 0;
            });

        if (_unitSide == DestructableBehaviour.UnitSide.Ally)
        {
           UICtrl.winLooseText.text = "You Lose";
        }
        else
        {
            UICtrl.winLooseText.text = "You Win";
        }
        
    }
}
