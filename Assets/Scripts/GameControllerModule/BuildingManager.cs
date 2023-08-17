using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private GameObject buildingPanel;
    
    private Build selectedBuildingArea;

    private Shop shop;
    
    public delegate void ClosePanelsDelegate();

    public static ClosePanelsDelegate closePanels;
    
    private void OnEnable()
    {
        if(shop == null)
            shop = Shop.Instance;
        closePanels += CloseBuildingMenu;
    }

    private void OnDisable()
    {
        closePanels -= CloseBuildingMenu;
    }

    public void SelectBuildingArea(Build buildingArea)
    {
        selectedBuildingArea = buildingArea;
        if (!selectedBuildingArea.isBuildingComplete)
        {
            Shop.Instance.OpenBuildingPanel();
            selectedBuildingArea.transform.GetComponent<DOTweenAnimation>().DOPlay();
        }
    }

    public void CloseBuildingMenu()
    {
        selectedBuildingArea = null;
        Shop.Instance.CloseBuildingPanel();
    }

    public void BuildTower(int buildingNo)
    {
        selectedBuildingArea.BuildTower(buildingNo);
        buildingPanel.SetActive(false);
        if (closePanels != null) closePanels();
    }

}
