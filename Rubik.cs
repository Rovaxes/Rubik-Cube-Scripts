using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubik : MonoBehaviour
{
    public RotateOnAxis[] faces;
    public Cube[] cubes;
    public bool canUpdate;

    public bool canRotateAxis;

    // Start is called before the first frame update
    void Start()
    {
        faces = GetComponentsInChildren<RotateOnAxis>();
        cubes = GetComponentsInChildren<Cube>();
        UpdateAllCubes();
        UpdateAllAxis();
        canRotateAxis = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canUpdate) {
            UpdateRubiks();
        }
    }

    public void UpdateRubiks() {
        UpdateAllCubes();
        UpdateAllAxis();    
    }

    public void UpdateAllAxis() {

        foreach (RotateOnAxis roa in faces) {
            roa.UpdateAxis();           
        }
    }

    public void UpdateAllCubes()
    {

        foreach (Cube cube in cubes)
        {
            cube.UpdateCube();
        }
    }
}
