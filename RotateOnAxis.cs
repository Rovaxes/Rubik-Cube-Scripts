using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnAxis : MonoBehaviour
{

    public List<GameObject> cubes;
    public Vector3 rotation;
    public bool isLocked = true;
    public bool isRotating = false;
    public Transform Rubik;
    public Rubik rubik;
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

        //When touched then initiate lock
        //this.transform.rotation = Quaternion.Euler(rotation);
    }

    void LockCubes() {
        foreach (GameObject cube in cubes) {
            cube.transform.SetParent(this.gameObject.transform);

            Cube c;
            if (cube.TryGetComponent<Cube>(out c)) {
                c.ClearAxis();
            }
        }
        isLocked = true;
    }

    void UnlockCubes() {
        foreach(GameObject cube in cubes) {
            cube.transform.SetParent(Rubik);
        }
        /* Need to Clear Cubes That are Rotatable Otherwise they get caught by other axis*/
        isLocked = false;
        rubik.UpdateAllAxis();
    }

    
    public void RotateAxis() { 
        //Vector3 newRotation = this.transform.rotation.eulerAngles + new Vector3(0, 0, 90f);
        //this.transform.rotation = Quaternion.Euler(newRotation);
        if (!isRotating)
        {
            isRotating = true;
            StartCoroutine(Rotate(new Vector3(0, 0, 1), 90, 1.0f));
        }
    }

    public void UpdateAxis() {
        Debug.Log("Updating Axis");
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
        
        transform.rotation = to;
        UnlockCubes();
        isRotating = false;
    }

}
