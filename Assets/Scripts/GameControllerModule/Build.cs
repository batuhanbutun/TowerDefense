using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    [SerializeField] private GameObject archerTower, mageTower, cannonTower, barracks, emptyArea;
    [SerializeField] private ParticleSystem buildParticle;
    public bool isBuildingComplete;
    
    
    public void OpenArcherTower() { archerTower.SetActive(true);}
    public void OpenMageTower() { mageTower.SetActive(true); }
    public void OpenCannonTower() { cannonTower.SetActive(true);}
    public void OpenBarracks() { barracks.SetActive(true); }

    public void BuildTower(int buildingNo)
    {
        emptyArea.SetActive(false);
        switch (buildingNo)
        {
            case 0:
                OpenArcherTower();
                Shop.Instance.SpendCoin(Shop.Instance.archerTCost);
                break;
            case 1:
                Shop.Instance.SpendCoin(Shop.Instance.barracksTCost);
                OpenBarracks();
                break;
            case 2:
                Shop.Instance.SpendCoin(Shop.Instance.mageTCost);
                OpenMageTower();
                break;
            case 3:
                Shop.Instance.SpendCoin(Shop.Instance.cannonTCost);
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
