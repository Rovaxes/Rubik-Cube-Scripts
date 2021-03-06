﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public List<RotateOnAxis> RotatableAxis;
    private Cube cube;
    public float dragThreshold = 10f;
    public bool u;
    
    // Start is called before the first frame update
    void Start()
    {
        cube = GetComponentInParent<Cube>();
        UpdateButton();
    }

    // Update is called once per frame
    void Update()
    {
        //RotatableAxis = cube.RotatatableAxis.FindAll(x => x.InPlane(VectorAbs(this.transform.forward)));
        if (u) {
            UpdateButton();
        }
        Debug.DrawRay(this.transform.position, this.transform.forward, Color.blue);
    }

    public void UpdateButton() {
        RotatableAxis = cube.RotatatableAxis.FindAll(x => x.InPlane(VectorAbs(this.transform.forward)));
    }

    Vector3 initialMP;
    Vector3 finalMP;
    private void OnMouseDown()
    {
        initialMP = Input.mousePosition;
    }

    
    private void determineAxisToRotate(Vector3 direction)
    {
        //Debug.Log("xRP: " + RotatePlane1 + " yRP: " + RotatePlane2);
        RotateOnAxis xRoa = RotatableAxis.Find(x => x.AxisPlane().x > 0.1);
        RotateOnAxis yRoa = RotatableAxis.Find(y => y.AxisPlane().y > 0.1);
        /*
        Debug.Log(xRoa);
        Debug.Log(yRoa);
        */

        float xMag = Mathf.Abs(direction.x);
        float yMag = Mathf.Abs(direction.y);
        if (xMag > dragThreshold && xMag > yMag)
        {
            Debug.Log("Hor");
            float xDir = Mathf.Sign(direction.x);
            Vector3 RotateDir = new Vector3(xDir * 90, 0);
            if (xRoa.rubik.canRotateAxis)
            {
                xRoa.RotateAxis(xDir * 90);
            }
        }
        else if (yMag > dragThreshold && yMag > xMag)
        {
            Debug.Log("Ver");
            float yDir = Mathf.Sign(direction.y);
            Vector3 RotateDir = new Vector3(0, yDir * 90);
            if (yRoa.rubik.canRotateAxis)
            {
                yRoa.RotateAxis(yDir * 90);
            }
        }
        else {
            //Do nothing message
        }

        Debug.Log(direction);
    }

    private void OnMouseUp()
    {
        finalMP = Input.mousePosition;
        Vector3 dragDirection = finalMP - initialMP;
        determineAxisToRotate(dragDirection);
    }

    private Vector3 VectorAbs(Vector3 plane) {
        Vector3 simplePlane = plane;
        simplePlane.x = Mathf.Abs(simplePlane.x);
        simplePlane.y = Mathf.Abs(simplePlane.y);
        simplePlane.z = Mathf.Abs(simplePlane.z);

        return simplePlane;

    }
}
