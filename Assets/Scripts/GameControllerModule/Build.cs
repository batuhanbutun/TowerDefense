using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    [SerializeField] private GameObject archerTower, mageTower, cannonTower, barracks, emptyArea;
    [SerializeField] private ParticleSystem buildParticle;
    public bool isBuildingComplete;

    private Shop shop;
    private void OpenArcherTower() { archerTower.SetActive(true);}
    private void OpenMageTower() { mageTower.SetActive(true); }
    private void OpenCannonTower() { cannonTower.SetActive(true);}
    private void OpenBarracks() { barracks.SetActive(true); }

    private void Start()
    {
        shop = FindObjectOfType<Shop>();
    }

    public void BuildTower(int buildingNo)
    {
        emptyArea.SetActive(false);
        switch (buildingNo)
        {
            case 0:
                OpenArcherTower();
                shop.SpendCoin(shop.archerTCost);
                break;
            case 1:
                shop.SpendCoin(shop.barracksTCost);
                OpenBarracks();
                break;
            case 2:
                shop.SpendCoin(shop.mageTCost);
                OpenMageTower();
                break;
            case 3:
                shop.SpendCoin(shop.cannonTCost);
                OpenCannonTower();
                break;
        }

        buildParticle.transform.position = transform.position + Vector3.up;
        buildParticle.Play();
        isBuildingComplete = true;
        GetComponent<BoxCollider>().enabled = false;
    }

    public void SellTower()
    {
        emptyArea.SetActive(true);
        GetComponent<BoxCollider>().enabled = true;
        isBuildingComplete = false;
    }
    
}
