using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GolemTargeting : MonoBehaviour {

    public List<Monster> Targets;
    public Monster Target;
    public float TargetDistance;
    public bool hasTarget;
    GolemControl gc;
    Golem self;

	// Use this for initialization
	void Awake () {
        self = GetComponent<Golem>();
        gc = Player.i.gc;
        StartCoroutine(AttackCoolDown(2f));
	}
	
	// Update is called once per frame
	void Update () {
		if(hasTarget && Target.isAlive) {
            TargetDistance = Vector3.Distance(self.pos, Target.transform.position);
            EvalTarget();
            if(self.state == GolemState.chasing) {
                self.move.Chase(Target.nav);
            }
        }
        else {
            ClearTarget();
            Targets = IdentifyTargets();
            EvalTargets();
        }
    }

    public Monster ClosestTarget(List<Monster> targets) {
        Monster ReturnTarget = targets[0];
        float distance = 0f;
        foreach(Monster m in targets) {
            if (Vector3.Distance(m.transform.position, Player.i.transform.position) > distance) {
                ReturnTarget = m;
            }
        }
        return ReturnTarget;
    }

    public void SetTarget(Monster target) {
        Debug.Log("Set target");
        Target = target;
        hasTarget = true;
    }

    public bool chasing;

    public void ChaseTarget(Monster target) {
        SetTarget(target);
        chasing = true;
        self.state = GolemState.chasing;
    }

    void ClearTarget() {
        self.SetState(GolemState.following);
        Target = null;
        hasTarget = false;
        chasing = false;
    }

    public void EvalTargets() {
        if(Targets.Count > 0) {
            var t = ClosestTarget(Targets);
            if(TargetDistance < 20) ChaseTarget(t);
        }
    }

    public List<Monster> IdentifyTargets() {
        Debug.Log("identify targets");
        List<Monster> allMonsters = GameObject.Find("Hierarchy").GetComponentsInChildren<Monster>().ToList();
        List<Monster> monsters = new List<Monster>();
        foreach (var m in allMonsters) {
            if(Vector3.Distance(m.transform.position,self.pos) < 20f && m.isAlive) {
                monsters.Add(m);
            }
        }
        return monsters;
    }

    public bool isReadyToAttack;

    public void EvalTarget() {
        if(Target.isAlive == false) {
            ClearTarget();
        }
        if(TargetDistance < 5 && isReadyToAttack) {
            Target.Damage(10f);
            Target.GetComponent<Rigidbody>().AddExplosionForce(1000, transform.position, 10f);
            Debug.Log("bam");
            isReadyToAttack = false;
            StartCoroutine(AttackCoolDown(2f));
        }
    }

    IEnumerator AttackCoolDown(float t) {
        yield return new WaitForSeconds(t);
        isReadyToAttack = true;
    }
}
