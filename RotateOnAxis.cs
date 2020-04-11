using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnAxis : MonoBehaviour
{

    public List<GameObject> cubes;
    public Vector3 rotation;
    public bool isLocked = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LocalDirections ld = new LocalDirections(this.transform);
        Debug.DrawRay(ld.trans.position, ld.N * 20, Color.yellow);
        Debug.DrawRay(ld.trans.position, ld.NE * 20, Color.yellow);
        Debug.DrawRay(ld.trans.position, ld.E * 20, Color.yellow);
        Debug.DrawRay(ld.trans.position, ld.SE * 20, Color.yellow);
        Debug.DrawRay(ld.trans.position, ld.S * 20, Color.yellow);
        Debug.DrawRay(ld.trans.position, ld.SW * 20, Color.yellow);
        Debug.DrawRay(ld.trans.position, ld.W * 20, Color.yellow);
        Debug.DrawRay(ld.trans.position, ld.NW * 20, Color.yellow);

        //When touched then initiate lock
        RaycastHit hit;
        foreach (Vector3 dir in ld.AllDirections()) {
            if (Physics.Raycast(ld.trans.position, dir, out hit, 20f))
            {
                Debug.Log("Hit: " + hit.transform);
                if(!cubes.Contains(hit.transform.gameObject))
                cubes.Add(hit.transform.gameObject);
            }
            else {
                Debug.Log("Miss");
            }
        }

        this.transform.rotation = Quaternion.Euler(rotation);
        LockCubes();
    }

    void LockCubes() {
        foreach (GameObject cube in cubes) {
            cube.transform.SetParent(this.gameObject.transform);
        }
    }
}
