using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


[TaskCategory("MyTasks")]
[TaskDescription("Drone Shoots")]

public class DroneShoot : Action
{
	Drone drone;

	public override void OnStart()
	{
		drone = GetComponent<Drone>();
	}

	public override TaskStatus OnUpdate()
	{
		if (drone)
		{
			drone.Shoot();
			
			return TaskStatus.Success;
		}
		else return TaskStatus.Failure;
	}
}

//QUARANTINE
/*
 * //drone.ArmyManager.UnlockArmyElement(gameObject);
 */