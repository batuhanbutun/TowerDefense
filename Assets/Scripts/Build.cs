using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    [SerializeField] private GameObject archerTower, mageTower, cannonTower, barracks, emptyArea;

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
                break;
            case 1:
                OpenBarracks();
                break;
            case 2:
                OpenMageTower();
                break;
            case 3:
                OpenCannonTower();
                break;
        }
        isBuildingComplete = true;
    }
    
}
