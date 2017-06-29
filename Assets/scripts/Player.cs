using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private static Player player;
    public static Player i {
        get {
            if (player == null) {
                player = GameObject.Find("player").GetComponent<Player>();
            }
            return player;
        }
    }

    public Vector3 pos;
    public GolemControl gc;
    public Torso torso;

	// Use this for initialization
	void Awake () {
        gc = GetComponent<GolemControl>();
        torso = GetComponentInChildren<Torso>();
	}
	
	// Update is called once per frame
	void Update () {
        pos = transform.position;
	}
}
