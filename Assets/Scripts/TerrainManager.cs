using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    static TerrainManager m_Instance;
    public static TerrainManager Instance { get { return m_Instance; } }

    [SerializeField] LayerMask m_TerrainLayerMask;

    [SerializeField] GameObject m_TerrainGO;
    public GameObject TerrainGO => m_TerrainGO;

    private void Awake()
    {
        if (!m_Instance) m_Instance = this;
        else Destroy(this);
    }

    public bool GetVerticallyAlignedPositionOnTerrain(Vector3 pos, ref Vector3 terrainPos, ref Vector3 terrainNormal)
    {
        RaycastHit hit;
        if (Physics.Raycast(pos + Vector3.up * 1000, Vector3.down, out hit, float.PositiveInfinity, m_TerrainLayerMask.value))
        {
            terrainPos = hit.point;
            terrainNormal = hit.normal;
            return true;
        }
        return false;
    }

}
