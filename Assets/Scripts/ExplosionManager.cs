using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum ExplosionSize { small,medium,big}

public class ExplosionManager : MonoBehaviour
{
    [SerializeField] GameObject[] m_ExplosionPrefabs;
    [SerializeField] float m_ExplosionLifeDuration;
    [SerializeField] float m_ExplosionOffsetDistance;

    static ExplosionManager m_Instance;
    public static ExplosionManager Instance { get { return m_Instance; } }

    private void Awake()
	{
        if (!m_Instance) m_Instance = this;
        else Destroy(this);
	}

    public void SpawnExplosionOnObject(Vector3 pos,Vector3 dir, GameObject go,ExplosionSize size = ExplosionSize.medium)
	{
        Vector3 rayOrigin = pos - dir * 2;
        RaycastHit[] hits = Physics.RaycastAll(rayOrigin, dir);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.collider.gameObject == go)
            {
                pos = hit.point;
                break;
            }
        }
      
        GameObject newExplosionGO = Instantiate(m_ExplosionPrefabs[(int)size], pos- m_ExplosionOffsetDistance*dir, Quaternion.identity);
        Destroy(newExplosionGO, m_ExplosionLifeDuration);
	}

}
