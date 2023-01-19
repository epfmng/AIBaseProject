using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class GetClosestTurret : Action
{
	public SharedTransform target;

	Turret[] turrets;

	IArmyElement m_ArmyElement;

	Transform tMin = null;
    float minDist = Mathf.Infinity;

	public override void OnAwake()
	{
		m_ArmyElement =(IArmyElement) GetComponent(typeof(IArmyElement));
	}

	public override TaskStatus OnUpdate()
	{
		if (!m_ArmyElement.ArmyManager) return TaskStatus.Running;
		ArmyManagerRed armyRed = m_ArmyElement.ArmyManager as ArmyManagerRed;

		turrets = armyRed.GetAllEnemiesOfType<Turret>(true).ToArray();
		Vector3 currentPos = transform.position;

		foreach (Turret t in turrets)
		{
			if (!(t && t.transform)) continue;
			float dist = Vector3.Distance(t.transform.position, currentPos);
			if (dist < minDist)
			{
				tMin = t.transform;
				minDist = dist;
			}
			else tMin = t.transform;
		}

		
		target.Value = tMin;
		if (target.Value != null) return TaskStatus.Success;
		else return TaskStatus.Failure;
		
	}
}