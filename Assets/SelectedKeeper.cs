using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedKeeper : MonoBehaviour
{
    public int stage=1;

    public List<GameObject> selectedGameobjects = new List<GameObject>();
    public List<GameObject> unselectedGameobjects = new List<GameObject>();

    public List<Vector3> selected = new List<Vector3>();
    public List<Vector3> unselected = new List<Vector3>();

    public List<Vector3> stage2Selected = new List<Vector3>();
    public List<Vector3> stage2UnSelected = new List<Vector3>();
    public List<GameObject> stage2UnselectedGameobjects = new List<GameObject>();


}
