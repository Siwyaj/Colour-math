using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToSelect : MonoBehaviour
{
    public GameObject SelectedKeeper;
    private void Start()
    {
        SelectedKeeper = GameObject.FindWithTag("keeper");
    }
    void OnMouseDown()
    {
        SelectedKeeper.GetComponent<SelectedKeeper>().unselected.Remove(gameObject);
        SelectedKeeper.GetComponent<SelectedKeeper>().selected.Add(gameObject);
        Destroy(gameObject);
    }
}
