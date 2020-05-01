using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnAxis : MonoBehaviour
{

    public List<GameObject> cubes;
    public List<GameObject> rotatingCubes;
    public Vector3 rotation;
    public bool isLocked = true;
    public bool isRotating = false;
    public Transform Rubik;
    public Rubik rubik;

    public Vector3 axisPlane; //Used to determine rotate direction

    // Start is called before the first frame update
    void Start()
    {
        rubik = GetComponentInParent<Rubik>(); 
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

        //Debug.Log("Transform: " + transform.forward);
    }

    public Vector3 AxisPlane() {
        LocalDirections ld = new LocalDirections(this.transform);
        axisPlane = ld.N + ld.E;
        axisPlane.x = Mathf.Abs(axisPlane.x);
        axisPlane.y = Mathf.Abs(axisPlane.y);
        axisPlane.z = Mathf.Abs(axisPlane.z);
        return axisPlane;
    }

    public bool InPlane(Vector3 line) {

        Vector3 ap = AxisPlane();
        //Debug.Log("Line: " + line + " Plane: " + ap);
        if (line.x > 0.1) {
            if (ap.x > 0.1) {
                return true;
            }
        }

        if (line.y > 0.1)
        {
            if (ap.y > 0.1)
            {
                return true;
            }
        }

        if (line.z > 0.1)
        {
            if (ap.z > 0.1)
            {
                return true;
            }
        }


        return false;
        

    }
    void LockCubes() {
        foreach (GameObject cube in cubes) {
            cube.transform.SetParent(this.gameObject.transform);
        }
        isLocked = true;
        rubik.canRotateAxis = false;
        rubik.canUpdate = false;
    }

    void UnlockCubes() {
        foreach(GameObject cube in cubes) {
            cube.transform.SetParent(Rubik);
        }
        /* Need to Clear Cubes That are Rotatable Otherwise they get caught by other axis*/
        isLocked = false;
        rubik.canUpdate = true;
    }

    
    public void RotateAxis(float amt) {
        if (!isRotating)
        {
            isRotating = true;

            /*Take care if the axis is flipped */
            
            if (Vector3.Dot(transform.forward, Vector3.right) > 0 || Vector3.Dot(transform.forward, Vector3.up) < 0)
            {
                StartCoroutine(Rotate(Vector3.forward, amt, 0.5f));
            }
            else {
                StartCoroutine(Rotate(Vector3.forward, -amt, 0.5f));
            }
        }
    }

    public void UpdateAxis() {
        Debug.Log("Updating Axis");

        axisPlane = AxisPlane();

        cubes.Clear();

        RaycastHit hit;
        LocalDirections ld = new LocalDirections(this.transform);
        foreach (Vector3 dir in ld.AllDirections())
        {
            if (Physics.Raycast(ld.trans.position, dir, out hit, 20f))
            {
                Cube cube;
                if (hit.transform.gameObject.TryGetComponent<Cube>(out cube))
                {
                    if (!cubes.Contains(hit.transform.gameObject))
                        cubes.Add(hit.transform.gameObject);

                    cube.AddAxis(this);
                }
                else
                {
                    Debug.Log("Object missing Cube Script");
                }

            }
            else
            {
                Debug.Log("Miss");
            }
        }

    }

    IEnumerator Rotate(Vector3 axis, float angle, float duration = 1.0f)
    {
        LockCubes();
        Debug.Log("Start");
        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation;
        to *= Quaternion.Euler(axis * angle);

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            
            yield return null;
            
        }
        Debug.Log("Done Rotating");
        transform.rotation = to;
        UnlockCubes();
        isRotating = false;
        rubik.canRotateAxis = true;
        //rubik.UpdateRubiks();
    }

}
