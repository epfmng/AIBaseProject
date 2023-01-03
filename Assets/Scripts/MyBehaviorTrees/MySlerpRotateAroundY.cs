using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Rotate slowly towards target.")]
    [TaskCategory("MyTasks")]
    public class MySlerpRotateAroundY : Action
    {
        [Tooltip("The Transform towards which the agent is rotating")]
        public SharedTransform target;
        [Tooltip("The Rotation slerping coef")]
        public SharedFloat slerpCoef;
        [Tooltip("The Arrival Angle magnitude")]
        public SharedFloat arrivalAngle;

        Quaternion qEndOrient;
        public override void OnStart()
        {
        }

        // Seek the destination. Return success once the agent has reached the destination.
        // Return running if the agent hasn't reached the destination yet
        public override TaskStatus OnUpdate()
        {
            if (!target.Value) return TaskStatus.Failure;

            Vector3 dir = Vector3.ProjectOnPlane(target.Value.position - transform.position, Vector3.up).normalized;
            qEndOrient = Quaternion.LookRotation(dir);

            Debug.DrawLine(transform.position, transform.position+dir*2, Color.red);
            //Debug.DrawLine(transform.position, transform.position + dir*4, Color.red);

            transform.rotation = Quaternion.Slerp(transform.rotation, qEndOrient, Time.deltaTime * slerpCoef.Value);
            if(Quaternion.Angle(transform.rotation,qEndOrient)< arrivalAngle.Value)
                    return TaskStatus.Success;

            return TaskStatus.Running;
        }
        

        public override void OnReset()
        {
            base.OnReset();
            target = null;
        }
    }
}