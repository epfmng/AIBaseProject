using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class getFurtherstTurret : Action
{
	public SharedTransform target;

	Turret[] turrets;

	IArmyElement m_ArmyElement;

	Transform tMax = null;
    float maxDist = 0;

	public override void OnAwake()
	{
		m_ArmyElement =(IArmyElement) GetComponent(typeof(IArmyElement));
	}

	public override TaskStatus OnUpdate()
	{
		turrets = m_ArmyElement.ArmyManager.GetAllEnemiesOfType<Turret>(true).ToArray();
		Vector3 currentPos = transform.position;

		foreach (Turret t in turrets)
		{
			float dist = Vector3.Distance(t.transform.position, currentPos);
			if (dist > maxDist)
			{
				tMax = t.transform;
				maxDist = dist;
			}
		}

		
		target.Value = tMax.transform;
		if (target.Value != null) return TaskStatus.Success;
		else return TaskStatus.Failure;
		
	}
}