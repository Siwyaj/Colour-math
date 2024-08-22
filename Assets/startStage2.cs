using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startStage2 : MonoBehaviour
{
    public GameObject selectedKeeper;
    public GameObject blackBox;
    public Vector3 baseColor = new Vector3(0.3f, 0.6f, 0.3f);
    public GameObject circlePrefab;
    public List<Vector3> stage2Coordinates;
    int stage = 1;

    private void Start()
    {
        stage = 1;
    }

    public void Button()
    {
        if (stage == 1)
        {
            StartStage2();
        }
        if(stage == 2)
        {
            StartStage3();
        }
    }
    void StartStage2()
    {
        //!!!!LOG SELECTED AND UNSELECTED


        stage2Coordinates = blackBox.GetComponent<CalculateStage2coordinates>().Stage2Coordinates(selectedKeeper.GetComponent<SelectedKeeper>().selected, selectedKeeper.GetComponent<SelectedKeeper>().unselected, baseColor);
        
        foreach(GameObject leftover in selectedKeeper.GetComponent<SelectedKeeper>().unselectedGameobjects)
        {
            Destroy(leftover);
        }
        foreach(GameObject selected in selectedKeeper.GetComponent<SelectedKeeper>().selectedGameobjects)
        {
            Destroy(selected);
        }
        foreach(Vector3 coordinate in stage2Coordinates)
        {
            GameObject circle = Instantiate(circlePrefab, (coordinate - baseColor)*500, Quaternion.identity);
            Color circleColor = blackBox.GetComponent<ConvertToP3>().Convert(coordinate); 
            circle.GetComponent<SpriteRenderer>().color = circleColor; 
            selectedKeeper.GetComponent<SelectedKeeper>().unselected.Add(circle.GetComponent<data>().xyYCoordinate);

            data circleData = circle.GetComponent<data>();//gets the data scipt, notice type = data as this is name of data script
            circleData.xyYCoordinate = coordinate; //circle data set xyY
            circleData.P3Color = circleColor; //circle data set P3 color 


            /*
            Use CalculateDistances script on blackbox to calculate the distances from the base color/coordinate's xyY and p3 to this color/circle's xyY and p3
             */

            //CalculatexyYDistance takes two argument, the base xyY coordinate the the current coordinate
            //The result is saved in the data script
            circleData.xyYDistanceToBasexyY = blackBox.GetComponent<CalculateDistances>().CalculatexyYDistance(baseColor, coordinate);

            //CalculateP3Distance also takes two arguments, the base color and this color
            //In this script, the color was saved in a variable earlier, we just have to convert the base color via ConvertToP3 script
            //The result is saved in the data script
            circleData.P3ColorDistanceToBase = blackBox.GetComponent<CalculateDistances>().CalculateP3Distance(blackBox.GetComponent<ConvertToP3>().Convert(baseColor), circleColor);

        }

    }
    void StartStage3()
    {

    }
}
