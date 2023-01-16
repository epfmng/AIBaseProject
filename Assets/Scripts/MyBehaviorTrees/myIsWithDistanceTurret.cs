using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class myIsWithinDistanceTurret : Action
{
    
    // Distance à laquelle l'ennemi doit être pour que l'action soit terminée avec succès
    public float maxDistance;

    public SharedTransform target;

    //IArmyElement m_ArmyElement;

	public override void OnStart()
	{
        //m_ArmyElement =(IArmyElement) GetComponent(typeof(IArmyElement));
	}

    public override TaskStatus OnUpdate()
    {
        //if (!m_ArmyElement.ArmyManager) return TaskStatus.Running;

        float enemyDistance = 0;

        //ArmyManagerRed armyRed = m_ArmyElement.ArmyManager as ArmyManagerRed;
        //Turret turret = armyRed.GetElement<Turret>();
        
        // Calculer la distance entre l'ennemi et l'objet qui exécute l'action
        if (target != null) enemyDistance = Vector3.Distance(transform.position, target.Value.position);

        // Si l'ennemi est à la distance spécifiée, terminer l'action avec succès
        if (enemyDistance <= maxDistance) return TaskStatus.Success;
        else return TaskStatus.Failure;
        
    }
}
