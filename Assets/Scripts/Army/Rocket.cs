using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float m_MaxLifeDuration;
    [SerializeField] float m_DamageRadius;
    [SerializeField] float m_DamagePoints;

    Rigidbody m_Rigidbody;
    Transform m_Transform;

    private void Awake()
	{
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Transform = transform;
        Destroy(gameObject, m_MaxLifeDuration);
    }

    public void Shoot(Vector3 targetPos,float travelDuration)
	{
        StartCoroutine(BallisticMoveCoroutine(travelDuration,m_Transform.position,targetPos));
        Destroy(gameObject, travelDuration+Time.fixedDeltaTime);
    }

    IEnumerator BallisticMoveCoroutine(float travelTime, Vector3 startPos, Vector3 endPos)
	{
        float sqrTravelTime = travelTime * travelTime;

        float elapsedTime = 0;
        Vector3 startVelocity = (endPos - startPos - Physics.gravity * sqrTravelTime * .5f) / travelTime;
        m_Rigidbody.MoveRotation(Quaternion.LookRotation(startVelocity.normalized));

        while (elapsedTime<travelTime)
		{
            yield return new WaitForFixedUpdate();
            //Vector3 newPos = .5f*Physics.gravity* elapsedTime*elapsedTime +startVelocity*elapsedTime+startPos;
            //Vector3 moveVect = newPos - m_Rigidbody.position;
            //m_Rigidbody.MovePosition(m_Rigidbody.position + moveVect);
            //if (moveVect.sqrMagnitude > 0)
            //    m_Rigidbody.MoveRotation(Quaternion.LookRotation(moveVect.normalized));

            Vector3 newVelocity = startVelocity + Physics.gravity * elapsedTime;
            m_Rigidbody.AddForce(newVelocity-m_Rigidbody.velocity, ForceMode.VelocityChange);
            if(m_Rigidbody.velocity.sqrMagnitude>0) m_Rigidbody.MoveRotation(Quaternion.LookRotation(m_Rigidbody.velocity.normalized));
            elapsedTime += Time.fixedDeltaTime;
        }

        Destroy(gameObject);
        ExplosionManager.Instance.SpawnExplosionOnObject(m_Transform.position, m_Transform.forward, TerrainManager.Instance.TerrainGO,ExplosionSize.big);

        //inflict damage to nearby enemies
        Collider[] hitColliders = Physics.OverlapSphere(endPos, m_DamageRadius);
		foreach (var item in hitColliders)
		{
            if (!item.gameObject.CompareTag(gameObject.tag))
                item.GetComponentInChildren<Health>()?.InflictDamage(m_DamagePoints);
		}
    }

	//private void OnTriggerEnter(Collider other)
	//{
 //       //if (!other.CompareTag(gameObject.tag))
 //       //{
 //       //    ExplosionManager.Instance.SpawnExplosionOnObject(m_Transform.position,m_Transform.forward,other.gameObject);
 //       //    Destroy(gameObject);
 //       //}
 //   }
}
