using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public int coin;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TowerManager towerManager;
    
    
    [SerializeField] private GameObject  archerTGray,  barracksTGray, mageTGray, cannonTGray;
    [SerializeField] private TextMeshProUGUI  archerCostText,  barracksCostText, mageCostText, cannonCostText;
    public int archerTCost, barracksTCost, mageTCost, cannonTCost;
    [SerializeField] private GameObject buildingPanel;

    private bool isPanelOpen = false;

    public DOTweenAnimation coinAnimation;

    private void Start()
    {
        coinText.text = coin.ToString();
    }

    public void EarnCoin(int amount)
    {
        coin += amount;
        coinText.text = coin.ToString();
        if (isPanelOpen)
        {
            archerTGray.SetActive(!HaveCoin(archerTCost));
            barracksTGray.SetActive(!HaveCoin(barracksTCost));
            mageTGray.SetActive(!HaveCoin(mageTCost));
            cannonTGray.SetActive(!HaveCoin(cannonTCost));
        }
        towerManager.CoinControl();
        if(coinAnimation!= null)
         coinAnimation.DOPlay();
    }

    public bool HaveCoin(int amount)
    {
        if (coin >= amount)
            return true;
        return false;
    }

    public void SpendCoin(int amount)
    {
        coin -= amount;
        coinText.text = coin.ToString();
    }

    public void OpenBuildingPanel()
    {
        archerTGray.SetActive(!HaveCoin(archerTCost));
        barracksTGray.SetActive(!HaveCoin(barracksTCost));
        mageTGray.SetActive(!HaveCoin(mageTCost));
        cannonTGray.SetActive(!HaveCoin(cannonTCost));
        archerCostText.text = archerTCost.ToString();
        barracksCostText.text = barracksTCost.ToString();
        mageCostText.text = mageTCost.ToString();
        cannonCostText.text = cannonTCost.ToString();
        buildingPanel.SetActive(true);
        isPanelOpen = true;
    }

    public void CloseBuildingPanel()
    {
        archerTGray.SetActive(false);
        barracksTGray.SetActive(false);
        mageTGray.SetActive(false);
        cannonTGray.SetActive(false);
        buildingPanel.SetActive(false);
        isPanelOpen = false;
    }
    
}
