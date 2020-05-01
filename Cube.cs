using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cube : MonoBehaviour
{

    public List<RotateOnAxis> RotatatableAxis;
    public bool rotate;
    public float dragThreshold = 10f;

    public Button[] Buttons;

    public void Start()
    {
        Buttons = this.GetComponentsInChildren<Button>();
        UpdateCube();
    }

    public void UpdateCube() {       

        foreach (Button b in Buttons) {
            b.UpdateButton();
        }

        RotatatableAxis.Clear();
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
