using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    GameObject marker;
    GameObject goalInd;

	// Use this for initialization
	void Awake () {
        marker = (GameObject)Instantiate(Resources.Load("marker"),transform.position,Quaternion.identity,transform);
        marker.GetComponent<MeshRenderer>().enabled = false;
        goalInd = (GameObject)Instantiate(Resources.Load("goalIndicator"), transform.position, Quaternion.identity, transform);
        goalInd.GetComponent<MeshRenderer>().enabled = false;
    }
	
    public void Highlight(Color c) {
        marker.GetComponent<MeshRenderer>().enabled = true;
        marker.GetComponent<MeshRenderer>().material.color = c;
    }

    public void UnHighlight() {
        marker.GetComponent<MeshRenderer>().enabled = false;
    }

    public void ShowInd(Vector3 pos) {
        goalInd.GetComponent<MeshRenderer>().enabled = true;
        goalInd.transform.position = pos;
        showingInd = true;
    }
    bool showingInd;

	// Update is called once per frame
	void Update () {
        marker.transform.position = transform.position;
        if (!showingInd) {
            goalInd.GetComponent<MeshRenderer>().enabled = false;
        }
	}

    private void LateUpdate() {
        showingInd = false;
    }
}
