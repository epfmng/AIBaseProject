using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class TestNavMeshAgent : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] float m_TranslationSpeed;
    [SerializeField] float m_AngularSpeed;
    [SerializeField] float m_ArriveDistance = 0.1f;

    [SerializeField] List<Transform> m_Waypoints;
    int m_WaypointIndex = 0;


    private void Awake()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();

        // set the speed and angular speed
        navMeshAgent.speed = m_TranslationSpeed;
        navMeshAgent.angularSpeed = m_AngularSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        GoToWaypoint(m_WaypointIndex);
    }

    void GoToWaypoint(int waypointIndex)
    {
        navMeshAgent.SetDestination(m_Waypoints[m_WaypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < m_ArriveDistance)
        {
            m_WaypointIndex = (m_WaypointIndex + 1) % m_Waypoints.Count;
            GoToWaypoint(m_WaypointIndex);
        }
    }
}
