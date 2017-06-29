using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour {

    private static PlayerMove playerMove;
    public static PlayerMove i {
        get {
            if(playerMove == null) {
                playerMove = GameObject.Find("player").GetComponent<PlayerMove>();
            }
            return playerMove;
        }
    }

    NavMeshAgent nav;
    public float speed;
    public float LeftStickDeadZone;
    public float RightStickDeadZone;
    public Vector3 input;
    public Vector3 input2;
    public Vector3 input3;

	// Use this for initialization
	void Awake () {
        nav = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float h2 = Input.GetAxis("RightAxisH");
        float v2 = Input.GetAxis("RightAxisV");

        float h3 = Input.GetAxis("Mouse X");
        float v3 = Input.GetAxis("Mouse Y");

        input = new Vector3(h, 0, v);
        if (input.magnitude < LeftStickDeadZone) input = Vector3.zero;

        input2 = new Vector3(h2, 0, v2);
        input3 = new Vector3(h3, 0, v3);
        if (input2.magnitude < RightStickDeadZone) input2 = Vector3.zero;

        nav.velocity = input * speed;
	}
}
