using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround: MonoBehaviour
{
    [SerializeField] Transform m_PivotPoint;
    [SerializeField] Vector3 m_PivotAxis;
    [SerializeField] float m_RotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(m_PivotPoint.position, m_PivotAxis, m_RotationSpeed * Time.deltaTime);        
    }
}
