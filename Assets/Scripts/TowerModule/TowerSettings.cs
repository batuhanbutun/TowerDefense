using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "TowerSettings/NewTower")]

public class TowerSettings : ScriptableObject
{
    [SerializeField] private List<int> _damageByLevels;
    [SerializeField] private List<float> _towerRangeByLevels;
    [SerializeField] private List<float> _fireRateByLevels;


    public List<int> damageByLevels { get { return _damageByLevels; } set { _damageByLevels = value; } }
    public List<float> towerRangeByLevels { get { return _towerRangeByLevels; } set { _towerRangeByLevels = value; } }
    public List<float> fireRateByLevels { get { return _fireRateByLevels; } set { _fireRateByLevels = value; } }


}
