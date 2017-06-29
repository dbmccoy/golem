using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public float Power;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.GetComponent<Monster>()) {
            collision.transform.GetComponent<Monster>().Damage(Power);
        }
    }

    void TransmitDamage() {

    }
}
