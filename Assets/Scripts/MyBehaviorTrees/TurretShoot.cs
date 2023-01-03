using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("MyTasks")]
[TaskDescription("Turret shoots a rocket towards target")]

public class TurretShoot: Action
{
	public SharedTransform target;
	Turret turret;

	public override void OnAwake()
	{
		turret = GetComponent<Turret>();
	}

	public override TaskStatus OnUpdate()
	{
		if (turret!= null && target.Value != null)
		{
			turret.Shoot(target.Value.position);			
			return TaskStatus.Success;
		}
		else return TaskStatus.Failure;
	}


}

//QUARANTINE
/*
 * turret.ArmyManager.UnlockArmyElement(gameObject);
 */