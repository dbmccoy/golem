using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private static CameraController _cc;
    public static CameraController i {
        get {
            if(_cc == null) {
                _cc = GameObject.Find("Main Camera").GetComponent<CameraController>();
            }
            return _cc;
        }
    }

    private Player player;
    public float OffsetY;
    public float OffsetZ;

	// Use this for initialization
	void Start () {
        player = Player.i;
	}
	
	// Update is called once per frame
	void Update () {
        var current = new Vector3(player.transform.position.x + drawOffset.x, OffsetY, player.transform.position.z + OffsetZ+ drawOffset.z);
        transform.position = Vector3.Lerp(current, current + drawOffset, Time.deltaTime*.4f);
	}

    public Vector3 drawOffset;
}
