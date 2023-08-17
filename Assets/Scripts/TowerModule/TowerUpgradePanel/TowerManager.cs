using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private GameObject towerPanel;
    [SerializeField] private GameObject upgradeCard;
    [SerializeField] private GameObject upgradeCardGray;
    [SerializeField] private TextMeshProUGUI upgradeCostText;
    [SerializeField] private GameObject sellCard;

    [SerializeField] private ParticleSystem levelUpParticle;
    
    public Tower selectedTower;

    private Shop shop;
    private bool isPanelOpen = false;

    private void OnEnable()
    {
        shop = Shop.Instance;
        BuildingManager.closePanels += CloseTowerPanel;
    }

    private void OnDisable()
    {
        BuildingManager.closePanels -= CloseTowerPanel;
    }

    private void OpenTowerPanel()
    {
        upgradeCostText.text = selectedTower.towerUpgradeCost.ToString();
        towerPanel.gameObject.SetActive(true);
        upgradeCard.SetActive(selectedTower.towerLevel < selectedTower.maxTowerLevel);
        upgradeCardGray.SetActive(!shop.HaveCoin(selectedTower.towerUpgradeCost));
        isPanelOpen = true;
    }

    public void CoinControl()
    {
        if (isPanelOpen)
        {
            upgradeCardGray.SetActive(!shop.HaveCoin(selectedTower.towerUpgradeCost));
        }
    }
    
    public void SelectTower(Tower tower)
    {
        selectedTower = tower;
        selectedTower.PlaySelectedAnimation();
        OpenTowerPanel();
    }

    public void LevelUpTower()
    {
        if (shop.HaveCoin(selectedTower.towerUpgradeCost))
        {
            shop.SpendCoin(selectedTower.towerUpgradeCost);
            selectedTower.LevelUp();
            BuildingManager.closePanels();
            levelUpParticle.transform.position = selectedTower.transform.position + Vector3.up * 2;
            levelUpParticle.Play();
        }
    }

    public void SellTower()
    {
        selectedTower.GetComponentInParent<Tower>().Sell();
        selectedTower.GetComponentInParent<Build>().SellTower();
        shop.EarnCoin(100);
        BuildingManager.closePanels();
    }

    public void CloseTowerPanel()
    {
        towerPanel.gameObject.SetActive(false);
        isPanelOpen = false;
    }

}
