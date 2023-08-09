using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Camera mainCam;

    [SerializeField] private BuildingManager buildingManager;
    [SerializeField] private TowerManager towerManager;
    
    private void Update()
    {
        InputHandler();
    }

    private void InputHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = mainCam.ScreenPointToRay (Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, 100))return;
            if (hit.transform.CompareTag("buildarea"))
            {
                buildingManager.SelectBuildingArea(hit.transform.GetComponent<Build>());
            }
            else if (hit.transform.CompareTag("tower"))
            {
                towerManager.SelectTower(hit.transform.GetComponent<Tower>());
            }
        }
    }

    public void CloseAllPanels()
    {
        buildingManager.CloseBuildingMenu();
        towerManager.CloseTowerPanel();
    }
    
}
