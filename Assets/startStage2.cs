using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startStage2 : MonoBehaviour
{
    public GameObject selectedKeeper;
    public GameObject blackBox;
    public GameObject circlePrefab;
    List<Vector3> coordinates = new List<Vector3>();
    Vector3 baseColor = new Vector3(210f / 255f, 121f / 255f, 117f / 255f);
    public List<Vector3> stage2Coordinates;
    public List<Vector3> stage3Coordinates;
    int stage = 1;

    Vector3 baseColorxyY;

    List<Color> colors = new List<Color>()
    {
        Color.red,
        Color.yellow,
        Color.green,
        Color.blue
    };

    private void Start()
    {
        (coordinates, baseColorxyY) = blackBox.GetComponent<CalculatexyYCoordinates>().CreateCoordinates(baseColor);
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


        stage2Coordinates = blackBox.GetComponent<CalculateStage2coordinates>().Stage2Coordinates(selectedKeeper.GetComponent<SelectedKeeper>().selected, selectedKeeper.GetComponent<SelectedKeeper>().unselected, baseColorxyY);

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
            GameObject circle = Instantiate(circlePrefab, (coordinate - coordinates[0]) *500, Quaternion.identity);
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
        stage3Coordinates = blackBox.GetComponent<CalculateEndResult>().CalculateEndPoints(selectedKeeper.GetComponent<SelectedKeeper>().stage2Selected, selectedKeeper.GetComponent<SelectedKeeper>().unselected, coordinates[0]);
        
        foreach (GameObject leftover in selectedKeeper.GetComponent<SelectedKeeper>().unselectedGameobjects)
        {
            Destroy(leftover);
        }
        foreach (GameObject selected in selectedKeeper.GetComponent<SelectedKeeper>().selectedGameobjects)
        {
            Destroy(selected);
        }


        int iColor = 0;
        foreach (Vector3 coordinate in stage3Coordinates)
        {
            GameObject circle = Instantiate(circlePrefab, (coordinate - coordinates[0]) * 500, Quaternion.identity);
            Color circleColor = blackBox.GetComponent<ConvertToP3>().Convert(coordinate);
            circle.GetComponent<SpriteRenderer>().color = colors[iColor%4];
            iColor++;
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
