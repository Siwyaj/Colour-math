using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToSelect : MonoBehaviour
{
    public SelectedKeeper SelectedKeeper;
    private void Start()
    {
        SelectedKeeper = GameObject.FindWithTag("keeper").GetComponent<SelectedKeeper>();
    }
    void OnMouseDown()
    {
        if (SelectedKeeper.stage==1)
        {
            SelectedKeeper.unselected.Remove(gameObject.GetComponent<data>().xyYCoordinate);
            SelectedKeeper.unselectedGameobjects.Remove(gameObject);
            SelectedKeeper.selected.Add(gameObject.GetComponent<data>().xyYCoordinate);
            gameObject.SetActive(false);
        }
        else if (SelectedKeeper.stage == 2)
        {
            SelectedKeeper.unselected.Remove(gameObject.GetComponent<data>().xyYCoordinate);
            SelectedKeeper.unselectedGameobjects.Remove(gameObject);
            SelectedKeeper.stage2Selected.Add(gameObject.GetComponent<data>().xyYCoordinate);
            gameObject.SetActive(false);
        }


        
    }
}
