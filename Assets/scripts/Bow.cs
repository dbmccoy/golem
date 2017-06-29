using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour {

    public float maxPower;
    public float drawSpeed;
    public float aimSpeed;

    public Vector3 input;
    public Vector3 aim;
    public Vector3 angle;
    public float angleY;

    public bool isDrawn;

    Quaternion rot;

    public Arrow arrow;
    LineRenderer lr;
    CameraPerlinShake shake;

	// Use this for initialization
	void Start () {
        lr = GetComponent<LineRenderer>();
        shake = CameraController.i.GetComponentInChildren<CameraPerlinShake>();
	}

    private float drawStart;
    public float drawLength;
    public float currentPower;

    private float drawOffsetPoint;
    private Vector3 aimLerp;
	// Update is called once per frame
	void Update () {


        if (Input.GetButton("Fire") && (PlayerMove.i.input2.magnitude > 0)) {
            if (!isDrawn) {
                drawStart = Time.fixedTime;
                drawOffsetPoint = 0;
                aimLerp = Vector3.zero;
                isDrawn = true;
            }
            drawLength = Time.fixedTime - drawStart;
            var targetAim = -PlayerMove.i.input2.normalized;
            aim = Vector3.Lerp(aim, targetAim, aimSpeed);
            Debug.DrawLine(Player.i.pos, Player.i.pos + aim, Color.blue);
            angleY = Vector3.Angle(Vector3.forward, aim);
            if (Mathf.Sign(aim.x) == -1) angleY = angleY * -1;

            angle = new Vector3(0, angleY, 0);
            rot = Quaternion.Euler(angle);
            Player.i.torso.transform.rotation = rot;

            lr.enabled = true;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, transform.position + aim * 30f);

            //camera shake
            shake.Enable();
            shake.magnitude = Mathf.Lerp(shake.magnitude, .75f, Time.deltaTime);
            

        }

        if (Input.GetButton("Fire"))
        {
            Debug.Log("fire");
        }

        if (Input.GetButton("Fire") && aim.magnitude > 0 && PlayerMove.i.input2.magnitude == 0) {

            Shoot(rot);
            shake.Disable();
            shake.magnitude = 0;
            isDrawn = false;
            lr.enabled = false;
            aim = Vector3.zero;
        }
        aimLerp = Vector3.Lerp(aimLerp, aim,Time.deltaTime * 3);
        //drawOffsetPoint = Mathf.Clamp(Mathf.Lerp(drawOffsetPoint, aim.magnitude * 4f, Time.deltaTime * 10),0,2);
        drawOffsetPoint = Mathf.Clamp(Mathf.Lerp(drawOffsetPoint, aim.magnitude * 4f, Time.deltaTime * 10f), 0, 2);
        CameraController.i.drawOffset = aimLerp * drawOffsetPoint;

    }


    Arrow Shoot(Quaternion _rot) {
        Arrow _arrow = Instantiate(arrow, transform.position + Vector3.up, _rot);
        currentPower = Mathf.Clamp(drawSpeed * drawLength, 50, maxPower);
        _arrow.GetComponent<Rigidbody>().velocity = aim * currentPower;
        return _arrow;
    }
}
