// MoveTo.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MoveTo : MonoBehaviour {

    public Transform goal;
    public NavMeshAgent goalNav;
    public Vector3 goalVector;
    public bool pause;
    NavMeshAgent agent;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetGoalNav(NavMeshAgent n) {
        goalNav = n;
    }

    public void Chase(NavMeshAgent t) {
        goalVector = t.transform.position + t.velocity;
    }

    private void Update() {
        if (!pause) {
            agent.destination = goalVector;
        }
    }
}
