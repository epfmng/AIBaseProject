using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] float m_LifeDuration;
    [SerializeField] float m_Acceleration;
    [SerializeField] float m_TranslationMaxSpeed;
    float m_TranslationSpeed;
    
    Rigidbody m_Rigidbody;
    Transform m_Transform;

    float m_InitHeightFromGround;

    [SerializeField] float m_DamagePoints;

    private void Awake()
	{
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Transform = transform;

        Destroy(gameObject, m_LifeDuration);
        m_TranslationSpeed = 0;

        Vector3 posOnTerrain=Vector3.zero;
        Vector3 normalOnTerrain = Vector3.zero;
        if (TerrainManager.Instance.GetVerticallyAlignedPositionOnTerrain(m_Transform.position, ref posOnTerrain,ref normalOnTerrain))
        {
            m_InitHeightFromGround = Vector3.Distance(posOnTerrain, m_Transform.position);
            //Debug.Log("m_InitHeightFromGround = " + m_InitHeightFromGround);
        }
        else Destroy(gameObject);

    }

    public void SetStartSpeed(float startSpeed)
	{
        this.m_TranslationSpeed = startSpeed;
	}

    // Update is called once per frame
    void FixedUpdate()
    {        
        m_TranslationSpeed = Mathf.Min(m_TranslationMaxSpeed, m_TranslationSpeed + Time.fixedDeltaTime * m_Acceleration);
        float dist = m_TranslationSpeed * Time.fixedDeltaTime;

        Vector3 nextPosition = m_Rigidbody.position + dist * transform.forward;
        Vector3 normalOnTerrain = Vector3.zero;

        if (TerrainManager.Instance.GetVerticallyAlignedPositionOnTerrain(nextPosition, ref nextPosition,ref normalOnTerrain))
        {
            //position
            nextPosition += Vector3.up * m_InitHeightFromGround;
            
            if(Vector3.Distance(nextPosition,m_Rigidbody.position)>0)
                m_Rigidbody.MovePosition(m_Rigidbody.position+(nextPosition- m_Rigidbody.position).normalized*dist);

            //orientation
            Quaternion qStraightUpRot = Quaternion.FromToRotation(m_Transform.up, normalOnTerrain);
            Quaternion newtOrientation = Quaternion.Slerp(m_Rigidbody.rotation, qStraightUpRot * m_Rigidbody.rotation, Time.fixedDeltaTime );
            m_Rigidbody.MoveRotation(newtOrientation);
        }
        else Destroy(gameObject);
    }

	private void OnTriggerEnter(Collider other)
	{
        if (!other.CompareTag(gameObject.tag))
        {
            ExplosionManager.Instance.SpawnExplosionOnObject(m_Transform.position,m_Transform.forward,other.gameObject,ExplosionSize.small);
            Destroy(gameObject);

            other.GetComponentInChildren<Health>()?.InflictDamage(m_DamagePoints);
        }
    }
}
