using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageLookHolder : MonoBehaviour
{
    [SerializeField] public Transform LookAt;
    [SerializeField] public Vector3 offset;

    
    private void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(LookAt.position + offset);
        if (transform.position != pos)
            transform.position = pos;
    }
}
