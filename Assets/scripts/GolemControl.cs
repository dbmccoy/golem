using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GolemControl : MonoBehaviour {

    public Golem SelectedGolem;
    public Golem L1;
    public Golem L2;
    public Golem R1;
    public bool engaged;

    public List<Monster> Monsters;
    public Dictionary<Monster, List<Golem>> AssignmentsDict;

    public GameObject ind;
    
    //manual direction
    bool manualDirection;
    float directStartStamp;
    float directLength;
    Vector3 directPos;


    // Use this for initialization
    void Awake () {
        AssignmentsDict = new Dictionary<Monster, List<Golem>>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("L1")) {
            engaged = true;
            SelectedGolem = L1;
        }
        if (Input.GetButtonDown("L2")) {
            engaged = true;
            SelectedGolem = L2;
        }
        if (Input.GetButtonDown("R1")) {
            engaged = true;
            SelectedGolem = R1;
        }

        if (engaged) {
            SelectedGolem.GetComponent<Entity>().Highlight(Color.green);
        }


        Vector3 dir = PlayerMove.i.input2;
        if (engaged == true && dir.magnitude > .01f) {
            Monsters = MonsterScan(transform.position + dir.normalized * 10f);
            Monsters.ForEach(x => x.GetComponent<Entity>().Highlight(Color.green));

            if (!manualDirection) {
                manualDirection = true;
                directPos = SelectedGolem.pos;
            }
            directLength = Time.fixedTime - directStartStamp;

            directPos = directPos + (dir.normalized*Time.deltaTime) * (30f);
            DirectGolem(directPos);

            if (Monsters.Count > 0) {
                foreach (var m in Monsters) {
                    if (!SelectedGolem.targeting.Targets.Contains(m)) {
                        SelectedGolem.targeting.Targets.Add(m);
                    }
                }
            }
        }

        if(Input.GetButtonUp("L1") || Input.GetButtonUp("L2") || Input.GetButtonUp("R1")) {
            engaged = false;
            manualDirection = false;
            directPos = Vector3.zero;
            SelectedGolem.GetComponent<Entity>().UnHighlight();
            SelectedGolem = null;
            Monsters.ForEach(x => x.GetComponent<Entity>().UnHighlight());
            ind.GetComponent<MeshRenderer>().enabled = false;
        }
	}

    public void DirectGolem(Vector3 goal) {
        SelectedGolem.SetState(GolemState.moving);
        SelectedGolem.SetGoal(goal);
        ind.transform.position = goal;
        ind.GetComponent<MeshRenderer>().enabled = true;
    }



    public List<Monster> MonsterScan(Vector3 dir) {
        List<Monster> monsters = new List<Monster>();
        Ray ray = new Ray(transform.position, transform.position + dir);
        RaycastHit[] hits = Physics.SphereCastAll(ray, 10f, 100f);
        Debug.DrawRay(ray.origin, ray.direction);
        foreach(RaycastHit hit in hits) {
            if (hit.transform.GetComponentInParent<Monster>()) {
                Monster m = hit.transform.GetComponentInParent<Monster>();
                if (!monsters.Contains(m) && m.isAlive == true) {
                    monsters.Add(hit.transform.GetComponentInParent<Monster>());
                }
            }
        }
        
        return monsters;
    }
}
