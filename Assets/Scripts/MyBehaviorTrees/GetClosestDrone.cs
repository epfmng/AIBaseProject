using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class GetClosestDrone : Action
{
	public SharedTransform target;

	Drone[] drones;

	IArmyElement m_ArmyElement;

	Transform tMin = null;
    float minDist = Mathf.Infinity;

	public override void OnAwake()
	{
		m_ArmyElement =(IArmyElement) GetComponent(typeof(IArmyElement));
	}

	public override TaskStatus OnUpdate()
	{
		drones = m_ArmyElement.ArmyManager.GetAllEnemiesOfType<Drone>(true).ToArray();
		Vector3 currentPos = transform.position;

		foreach (Drone d in drones)
		{
			float dist = Vector3.Distance(d.transform.position, currentPos);
			if (dist < minDist)
			{
				tMin = d.transform;
				minDist = dist;
			}
		}

		
		target.Value = tMin;
		if (target.Value != null) return TaskStatus.Success;
		else return TaskStatus.Failure;
		
	}
}