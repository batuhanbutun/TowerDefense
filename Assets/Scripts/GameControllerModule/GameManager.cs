using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Camera mainCam;

    [SerializeField] private BuildingManager buildingManager;
    [SerializeField] private TowerManager towerManager;
    
    [SerializeField] private ParticleSystem towerSelectedParticle;
    private Vector3 selectedParticleOffSet = new Vector3(0f, 0.01f, 0f);

    public Skill selectedSkill;
    public bool isSkillSelected = false;

    public bool test;

    private void OnEnable()
    {
        BuildingManager.closePanels += CloseParticle;
    }

    private void OnDisable()
    {
        BuildingManager.closePanels -= CloseParticle;
    }

    private void Update()
    {
        if(test)
            InputHandler();
    }

    private void InputHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = mainCam.ScreenPointToRay (Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, 100))return;
            if(BuildingManager.closePanels != null)
                BuildingManager.closePanels();
            if (hit.transform.CompareTag("buildarea"))
            {
                buildingManager.SelectBuildingArea(hit.transform.GetComponent<Build>());
                towerSelectedParticle.transform.position = hit.transform.position + selectedParticleOffSet;
                towerSelectedParticle.gameObject.SetActive(true);
                isSkillSelected = false;
            }
            else if (hit.transform.CompareTag("tower"))
            {
                towerManager.SelectTower(hit.transform.GetComponent<Tower>());
                towerSelectedParticle.transform.position = hit.transform.position + selectedParticleOffSet;
                towerSelectedParticle.gameObject.SetActive(true);
                isSkillSelected = false;
            }
            else if (hit.transform.CompareTag("ground") || hit.transform.CompareTag("enemy"))
            {
                BuildingManager.closePanels();
                if (isSkillSelected)
                {
                    selectedSkill.CastSkill(hit.point);
                    isSkillSelected = false;
                }
            }
        }
    }

    public void CloseParticle()
    {
        towerSelectedParticle.gameObject.SetActive(false);
    }

    public void SelectSkill(Skill skillToSelect)
    {
        selectedSkill = skillToSelect;
        if (selectedSkill.isSkillReady)
        {
            selectedSkill.SkillAnim();
            isSkillSelected = true;
        }
    }

    public void RestartChapter()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
}
