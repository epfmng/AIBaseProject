using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class myIsWithinDistanceTurret : Action
{
    
    // Distance à laquelle l'ennemi doit être pour que l'action soit terminée avec succès
    public float maxDistance;

    public SharedTransform target;

    IArmyElement m_ArmyElement;
    

	public override void OnStart()
	{
        m_ArmyElement =(IArmyElement) GetComponent(typeof(IArmyElement));
	}

    public override TaskStatus OnUpdate()
    {
        if (!m_ArmyElement.ArmyManager) return TaskStatus.Running;
        ArmyManagerRed armyRed = m_ArmyElement.ArmyManager as ArmyManagerRed;

        float enemyDistance = 0;

        if (target.Value != null)
        {
           
            enemyDistance = Vector3.Distance(transform.position, target.Value.position);
            
            if (target.Value.position == null)
            {
                Turret newTarget = armyRed.GetElement<Turret>();
                if (newTarget != null) enemyDistance = Vector3.Distance(transform.position, newTarget.transform.position);
            }
        }


        // Si l'ennemi est à la distance spécifiée, terminer l'action avec succès
        if (enemyDistance <= maxDistance) return TaskStatus.Success;
        else return TaskStatus.Failure;
        
    }
}
