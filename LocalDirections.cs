using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalDirections
{
    public Transform trans;

    public Vector3 N { get { return trans.TransformDirection(Vector3.up); } } 
    public Vector3 NE { get { return trans.TransformDirection(Vector3.up + Vector3.right); } }
    public Vector3 E { get { return trans.TransformDirection(Vector3.right); } }
    public Vector3 SE { get { return trans.TransformDirection(-Vector3.up + Vector3.right); } }
    public Vector3 S { get { return trans.TransformDirection(-Vector3.up); } }
    public Vector3 SW { get { return trans.TransformDirection(-Vector3.up + -Vector3.right); } }
    public Vector3 W { get { return trans.TransformDirection(-Vector3.right); } }
    public Vector3 NW { get { return trans.TransformDirection(Vector3.up + -Vector3.right); } }

    public Vector3[] AllDirections(){
        return new Vector3[]{ N, NE, E, SE, S, SW, W, NW }; } 

    public LocalDirections(Transform t) {
        trans = t;
    }


}
