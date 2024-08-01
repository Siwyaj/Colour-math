using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    public GameObject blackBox;
    public GameObject circlePrefab;
    public List<Vector3> coordinates;
    public GameObject selectedKeeper;

    public Vector3 baseColor = new Vector3(0.2296f, 0.2897f, 0.2815f);  
    void Start()
    {

        coordinates = new List<Vector3>();
        coordinates.AddRange(blackBox.GetComponent<CalculatexyYCoordinates>().CreateCoordinates(baseColor));

        
        foreach (Vector3 coordinate in coordinates)
        {
            GameObject circle = Instantiate(circlePrefab, (coordinate - baseColor)*500, Quaternion.identity);//Instantiate the circle prefab, i do this at the coordinates of the circle for the test do this according to your game
            Color circleColor = blackBox.GetComponent<ConvertToP3>().Convert(coordinate); //Get the color based on the coordinates
            circle.GetComponent<SpriteRenderer>().color = circleColor;//set the sprite renderer to the color 
            selectedKeeper.GetComponent<SelectedKeeper>().unselected.Add(circle);//add the circle(GameObject) to the unselected list in the selectkeeper, do this as you please as long as it ends with two list of gameobjects

            //The circle prefab has a data script on it, which I use to keep the information which needs to be logged
            //Set the initial know values
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

            //This was just used to see the that there was a difference between the two as some values were suspiciously close
            circleData.distanceDifference = circleData.xyYDistanceToBasexyY - circleData.P3ColorDistanceToBase;
        }
    }
}
