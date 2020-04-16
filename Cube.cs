using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cube : MonoBehaviour
{

    public List<RotateOnAxis> RotatatableAxis;
    public bool rotate;
    public float dragThreshold = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /*
    // Update is called once per frame
    void FixedUpdate()
    {
        //LocalDirections ld = new LocalDirections(this.transform);      
        if (rotate) {
            RotatatableAxis[0].RotateAxis();
        }
    }*/

    Vector3 initialMP;
    Vector3 finalMP;
    private void OnMouseDown()
    {
        initialMP = Input.mousePosition;
        //Debug.Log("Drag: " + Input.mousePosition.x);
        //RotatatableAxis[0].RotateAxis();
    }

    private void determineAxisToRotate(Vector3 direction) {
        float x = direction.x;
        float y = direction.y;
        Debug.Log(direction);
        //Debug.Log(Mathf.Abs(xDrag));

        /* Treat z and x the same */
        if (Mathf.Abs(x) > dragThreshold && Mathf.Abs(x) > Mathf.Abs(y))
        {
            // Swipe Horizontal
            //Debug.Log("X: " + x);

            //Should change roa.axisPlane to abs or normalize it later
            RotateOnAxis axis = RotatatableAxis.Find(roa => (Mathf.Abs(roa.axisPlane.x) == 1 || Mathf.Abs(roa.axisPlane.z) == 1));
            float dir = Mathf.Sign(x);
            axis.RotateAxis(dir * 90);
            //Debug.Log(axis.axisPlane);
        }
        else if (Mathf.Abs(y) > dragThreshold && Mathf.Abs(y) > Mathf.Abs(x))
        {
            // Swipe Horizontal
            //Debug.Log("Y: " + x);           
            RotateOnAxis axis = RotatatableAxis.Find(roa => (Mathf.Abs(roa.axisPlane.y) == 1));
            float dir = Mathf.Sign(y);
            axis.RotateAxis(dir * 90);
            //Debug.Log(axis.axisPlane);
        }
        else
        {
            //Do nothing
        }
        /*
        foreach (RotateOnAxis roa in RotatatableAxis) {
            Debug.Log(roa.axisPlane);
        }*/
        
    }

    private void OnMouseUp()
    {
        finalMP = Input.mousePosition;
        Vector3 dragDirection = finalMP - initialMP;
        determineAxisToRotate(dragDirection);
    }



    public void ClearAxis() {
        RotatatableAxis.Clear();
    }
    public void AddAxis(RotateOnAxis roa) {
        if (!RotatatableAxis.Contains(roa))
        {
            RotatatableAxis.Add(roa);
        }
    }
}
