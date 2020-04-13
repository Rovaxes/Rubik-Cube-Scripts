using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubik : MonoBehaviour
{
    public RotateOnAxis[] faces;
    public Cube[] cubes;
    // Start is called before the first frame update
    void Start()
    {
        faces = GetComponentsInChildren<RotateOnAxis>();
        cubes = GetComponentsInChildren<Cube>();
        UpdateAllAxis();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAllAxis() {

        foreach (RotateOnAxis roa in faces) {
            roa.UpdateAxis();           
        }
    }
}
