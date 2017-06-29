using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour {


    public Vector3 pos;
    public GolemState state;

    private Vector3 playerOffset;
    public bool isFollowing;

    public GolemTargeting targeting;
    public MoveTo move;

    

	// Use this for initialization
	void Awake () {
        move = GetComponent<MoveTo>();
        targeting = GetComponent<GolemTargeting>();
	}
	
	// Update is called once per frame
	void Update () {
        pos = transform.position;
        if (state == GolemState.following) { //following
            if (!isFollowing) {
                Debug.Log("set to follow");
                isFollowing = true;
                playerOffset = pos - Player.i.pos;
                Debug.Log(playerOffset);
            }
            move.goalVector = Player.i.pos + playerOffset;
        }

        if(state == GolemState.moving) { //moving
            if(Vector3.Distance(pos, move.goalVector) < .1f) {
                state = GolemState.following;
            }
        }
	}

    public void SetState(GolemState s) {
        isFollowing = false;
        state = s;
    }

    public void SetGoal(Vector3 g) {
        //Debug.Log("goal set to " + g);
        move.goalVector = g;
    }

}

public enum GolemState {
    following,
    moving,
    engaging,
    engaged,
    chasing
}
