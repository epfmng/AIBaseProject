using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class myIsWithinAngle : Action
{
	public SharedTransform target;

	public override void OnStart()
	{
	}

	public override TaskStatus OnUpdate()
	{
		if (!target.Value) return TaskStatus.Failure;

        Vector3 dir = Vector3.ProjectOnPlane(target.Value.position - transform.position, Vector3.up).normalized;
        

        if (Vector3.Angle(transform.forward, dir) < 5f)
        {
            return TaskStatus.Success;
        }
        
        else
        {
            return TaskStatus.Failure;
        }
	}
}