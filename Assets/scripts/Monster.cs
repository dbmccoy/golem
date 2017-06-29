using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour {

    public float health;
    public bool isAlive;
    public bool isChasing;

    public NavMeshAgent nav;
    MoveTo move;

	// Use this for initialization
	void Start () {
        Player.i.gc.AssignmentsDict.Add(GetComponent<Monster>(), new List<Golem>());
        move = GetComponent<MoveTo>();
        nav = GetComponent<NavMeshAgent>();
        //Debug.Log(Player.i.gc.AssignmentsDict.Count);
	}

    public void Damage(float dmg) {
        health = health - dmg;
        if (health <= 0) {
            Die();
        }
    }

    public void Die() {
        //Destroy(transform.gameObject);
        isChasing = false;
        move.pause = true;
        isAlive = false;
        GetComponent<NavMeshAgent>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (isChasing) {
            move.goalVector = Player.i.pos;
        }
	}
}
