using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateStage2coordinates : MonoBehaviour
{
    List<Vector3> stage2Coordinates = new List<Vector3>(); 

    List<(Vector3, bool)> direction1 = new List<(Vector3, bool)>();//TRUE=Selected/different, False=not-selected/same

    
    public void Stage2Coordinates(List<GameObject> selected, List<GameObject> unSelected, Vector3 baseColor)//change to whatever is provided
    {


        //convert to what makes sense
        foreach (GameObject selectedCircle in selected)
        {
            if(selectedCircle.GetComponent<data>().prefabxyY != baseColor)
            {
                float angle = Mathf.Atan2(baseColor[1] - selectedCircle.GetComponent<data>().prefabxyY[1], baseColor[0] - selectedCircle.GetComponent<data>().prefabxyY[0]);
                Debug.Log(angle);
            
            }
        }


    }
}
