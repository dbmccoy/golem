using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

    public Monster monster;
    public float timer;

	// Use this for initialization
	void Start () {
        StartCoroutine(WaveTimer(timer));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator WaveTimer(float t) {
        yield return new WaitForSeconds(t);
        Vector3 offset = new Vector3(Random.Range(-5, 5), 0, 20f);
        Instantiate(monster, Player.i.pos + offset, Quaternion.identity);
        StartCoroutine(WaveTimer(t));
    }
}
