using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    public GameObject blackBox;
    public GameObject circlePrefab;
    public List<Vector3> coordinates;
    public GameObject selectedKeeper;
    GameObject circle;

    Vector3 baseColour = new Vector3(0.3f, 0.6f, 0.3f);  
    void Start()
    {

        coordinates = new List<Vector3>();
        coordinates.AddRange(blackBox.GetComponent<CalculatexyYCoordinates>().CreateCoordinates(baseColour));

        
        foreach (Vector3 coordinate in coordinates)
        {
            circle = Instantiate(circlePrefab, (coordinate - baseColour)*360, Quaternion.identity);
            Color circleColor = blackBox.GetComponent<ConvertToP3>().Convert(coordinate);
            circle.GetComponent<SpriteRenderer>().color = circleColor;
            selectedKeeper.GetComponent<SelectedKeeper>().selected.Add(circle);


            data circleData = circle.GetComponent<data>();//gets the data scipt, notice type = data as this is name of data script
            circleData.prefabxyY = coordinate; //circle data set xyY
            circleData.P3Color = circleColor; //circle data set P3 color 
            circleData.xyYDistanceToBasexyY = blackBox.GetComponent<CalculateDistances>().CalculatexyYDistance(coordinates[0], coordinate);
            circleData.P3ColorDistanceToBase = blackBox.GetComponent<CalculateDistances>().CalculateP3Distance(blackBox.GetComponent<ConvertToP3>().Convert(baseColour), circleColor);
            circleData.distanceDifference = circleData.xyYDistanceToBasexyY - circleData.P3ColorDistanceToBase;
        }
    }
}
