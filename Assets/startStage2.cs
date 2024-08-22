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
    public List<Vector3> stage3Coordinates;
    int stage = 1;

    private void Start()
    {
        stage = 1;
    }

    public void Button()
    {
        
        if(stage == 2)
        {
            StartStage3();

        }
        if (stage == 1)
        {
            StartStage2();
            stage = 2;
            selectedKeeper.GetComponent<SelectedKeeper>().stage = 2;
        }
    }
    void StartStage2()
    {
        //!!!!LOG SELECTED AND UNSELECTED


        stage2Coordinates = blackBox.GetComponent<CalculateStage2coordinates>().Stage2Coordinates(selectedKeeper.GetComponent<SelectedKeeper>().selected, selectedKeeper.GetComponent<SelectedKeeper>().unselected, baseColor);

        selectedKeeper.GetComponent<SelectedKeeper>().selected.Clear();
        selectedKeeper.GetComponent<SelectedKeeper>().unselected.Clear();

        foreach (GameObject leftover in selectedKeeper.GetComponent<SelectedKeeper>().unselectedGameobjects)
        {
            Destroy(leftover);
        }
        foreach(GameObject selected in selectedKeeper.GetComponent<SelectedKeeper>().selectedGameobjects)
        {
            Destroy(selected);
        }
        selectedKeeper.GetComponent<SelectedKeeper>().unselectedGameobjects.Clear();
        foreach (Vector3 coordinate in stage2Coordinates)
        {
            GameObject circle = Instantiate(circlePrefab, (coordinate - baseColor)*500, Quaternion.identity);
            Color circleColor = blackBox.GetComponent<ConvertToP3>().Convert(coordinate); 
            circle.GetComponent<SpriteRenderer>().color = circleColor;

            data circleData = circle.GetComponent<data>();//gets the data scipt, notice type = data as this is name of data script
            circleData.xyYCoordinate = coordinate; //circle data set xyY
            selectedKeeper.GetComponent<SelectedKeeper>().unselected.Add(circle.GetComponent<data>().xyYCoordinate);
            selectedKeeper.GetComponent<SelectedKeeper>().unselectedGameobjects.Add(circle);
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
        stage3Coordinates = blackBox.GetComponent<CalculateEndResult>().CalculateEndPoints(selectedKeeper.GetComponent<SelectedKeeper>().stage2Selected, selectedKeeper.GetComponent<SelectedKeeper>().unselected, baseColor);
        Debug.Log("Length of stage3Coordinates:" + stage3Coordinates.Count);
        Debug.Log("placement of first Vector in stage3Coordinates" + stage3Coordinates[0]);
        foreach (GameObject leftover in selectedKeeper.GetComponent<SelectedKeeper>().unselectedGameobjects)
        {
            Destroy(leftover);
        }
        foreach (GameObject selected in selectedKeeper.GetComponent<SelectedKeeper>().selectedGameobjects)
        {
            Destroy(selected);
        }


        Debug.Log("Stage3 function ran");

        foreach (Vector3 coordinate in stage3Coordinates)
        {
            GameObject circle = Instantiate(circlePrefab, (coordinate - baseColor) * 500, Quaternion.identity);
            Color circleColor = blackBox.GetComponent<ConvertToP3>().Convert(coordinate);
            circle.GetComponent<SpriteRenderer>().color = Color.red;
            selectedKeeper.GetComponent<SelectedKeeper>().unselectedGameobjects.Add(circle);
            selectedKeeper.GetComponent<SelectedKeeper>().unselected.Add(coordinate);

            data circleData = circle.GetComponent<data>();//gets the data scipt, notice type = data as this is name of data script
            circleData.xyYCoordinate = coordinate; //circle data set xyY
            circleData.P3Color = circleColor; //circle data set P3 color 
            selectedKeeper.GetComponent<SelectedKeeper>().unselected.Add(coordinate*10);


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
}
