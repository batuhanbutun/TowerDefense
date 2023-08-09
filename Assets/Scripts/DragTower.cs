using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTower : MonoBehaviour
{
    private Vector3 mOffSet;
    private float mZCoord;
    private bool canLand = true;

    [SerializeField] private Material NotLandedMaterial;
    [SerializeField] private Material LandedMaterial;
    [SerializeField] private MeshRenderer myMeshRenderer;
    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffSet = gameObject.transform.position - GetMouseWorldPos();
        mOffSet.y = 0f;
        myMeshRenderer.material = NotLandedMaterial;
    }
    
    private void OnMouseDrag()
    {
        Vector3 tempPosition = GetMouseWorldPos() + mOffSet;
        if (transform.position.z >= -3f && tempPosition.z > -3f)
            tempPosition.z = -3f;
        transform.position = tempPosition;
    }

    private void OnMouseUp()
    {
        myMeshRenderer.material = LandedMaterial;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(mousePoint);
        screenPosition.y = 0f;
        return screenPosition;
    }
    
}
