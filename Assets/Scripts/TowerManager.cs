using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private GameObject towerPanel;
    [SerializeField] private GameObject upgradeCard;
    [SerializeField] private GameObject sellCard;
    
    public Tower selectedTower;


    public void SelectTower(Tower tower)
    {
        selectedTower = tower;
        towerPanel.gameObject.SetActive(true);
        upgradeCard.SetActive(selectedTower.towerLevel < selectedTower.maxTowerLevel);
    }

    public void LevelUpTower()
    {
        selectedTower.LevelUp();
        towerPanel.gameObject.SetActive(false);
    }

    public void CloseTowerPanel()
    {
        towerPanel.gameObject.SetActive(false);
    }
  
}
