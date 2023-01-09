using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class myIsWithinDistance : Action
{
    
    // Distance à laquelle l'ennemi doit être pour que l'action soit terminée avec succès
    public float distance;

    public SharedTransform target;

    
	public override void OnStart()
	{
	}

    public override TaskStatus OnUpdate()
    {
        // Calculer la distance entre l'ennemi et l'objet qui exécute l'action
        float enemyDistance = Vector3.Distance(transform.position, target.Value.position);

        // Si l'ennemi est à la distance spécifiée, terminer l'action avec succès
        if (enemyDistance <= distance)
        {
            return TaskStatus.Success;
        }
        
        else
        {
            return TaskStatus.Failure;
        }
    }
}
