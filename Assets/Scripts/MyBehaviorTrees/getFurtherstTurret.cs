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

	public float limitDist;

	public override void OnAwake()
	{
		m_ArmyElement =(IArmyElement) GetComponent(typeof(IArmyElement));
	}

	public override TaskStatus OnUpdate()
	{
		turrets = m_ArmyElement.ArmyManager.GetAllEnemiesOfType<Turret>(true).ToArray();
		Vector3 currentPos = transform.position;
		if (target.Value!=null) {
			foreach (Turret t in turrets)
			{
				if (!(t && t.transform)) continue;
				float dist = Vector3.Distance(t.transform.position, currentPos);
				if (dist > maxDist && dist < limitDist)
				{
					tMax = t.transform;
					maxDist = dist;
				}
				else tMax = t.transform;
			}
		}
		else return TaskStatus.Failure;

		
		target.Value = tMax;
		if (target.Value != null) return TaskStatus.Success;
		else return TaskStatus.Failure;
		
	}
}