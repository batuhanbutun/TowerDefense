using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{

    [SerializeField] private GameObject buildingPanel;
    
    private Build selectedBuildingArea;

    public void SelectBuildingArea(Build buildingArea)
    {
        selectedBuildingArea = buildingArea;
        if(!selectedBuildingArea.isBuildingComplete)
            buildingPanel.SetActive(true);
    }

    public void CloseBuildingMenu()
    {
        selectedBuildingArea = null;
        buildingPanel.SetActive(false);
    }

    public void BuildTower(int buildingNo)
    {
        selectedBuildingArea.BuildTower(buildingNo);
        buildingPanel.SetActive(false);
    }

}
