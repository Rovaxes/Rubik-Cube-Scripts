using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{

    public List<RotateOnAxis> RotatatableAxis;
    public bool rotate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //LocalDirections ld = new LocalDirections(this.transform);      
        if (rotate) {
            RotatatableAxis[0].RotateAxis();
        }
    }



    private void OnMouseEnter()
    {
        Debug.Log("OnDrag");
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
