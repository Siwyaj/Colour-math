using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    public GameObject blackBox;
    public GameObject circlePrefab;
    public List<Vector3> coordinates;
    public GameObject selectedKeeper;
    public GameObject background;


    Vector3 baseColorxyY;

    Vector3 baseColor = new Vector3(210f/255f, 121f/255f, 117f/255f);  
    void Start()
    {
        //Debug.Log(210 / 255);
        //Debug.Log("Vector given start: " + baseColor/255f);

        (coordinates, baseColorxyY) = blackBox.GetComponent<CalculatexyYCoordinates>().CreateCoordinates(baseColor);
        Color BaseConvertedNew = blackBox.GetComponent<ConvertToP3>().convertBasesRGBToP3(baseColor);
        background.GetComponent<SpriteRenderer>().color = Color.black;
        Color BaseConvertedfull = blackBox.GetComponent<ConvertToP3>().Convert(baseColorxyY);
        Debug.Log("Difference x" + Mathf.Abs(BaseConvertedfull[0] - BaseConvertedNew[0]));
        Debug.Log("Difference y" + Mathf.Abs(BaseConvertedfull[1] - BaseConvertedNew[1]));
        Debug.Log("Difference z" + Mathf.Abs(BaseConvertedfull[2] - BaseConvertedNew[2]));
        //Error: We are converting xyY to P3 and sRGB as xyY to P3,

        //coordinates.AddRange();

        
        foreach (Vector3 coordinate in coordinates)
        {
            GameObject circle = Instantiate(circlePrefab, (coordinate - baseColorxyY) *500, Quaternion.identity);//Instantiate the circle prefab, i do this at the coordinates of the circle for the test do this according to your game
            Color circleColor = blackBox.GetComponent<ConvertToP3>().Convert(coordinate); //Get the color based on the coordinates
            circle.GetComponent<SpriteRenderer>().color = circleColor;//set the sprite renderer to the color 
            selectedKeeper.GetComponent<SelectedKeeper>().unselected.Add(coordinate);//add the circle(GameObject) to the unselected list in the selectkeeper, do this as you please as long as it ends with two list of gameobjects
            selectedKeeper.GetComponent<SelectedKeeper>().unselectedGameobjects.Add(circle);
            //The circle prefab has a data script on it, which I use to keep the information which needs to be logged
            //Set the initial know values
            data circleData = circle.GetComponent<data>();//gets the data scipt, notice type = data as this is name of data script
            circleData.xyYCoordinate = coordinate; //circle data set xyY
            circleData.P3Color = circleColor; //circle data set P3 color 

            //CalculatexyYDistance takes two argument, the base xyY coordinate the the current coordinate
            //The result is saved in the data script
            circleData.xyYDistanceToBasexyY = blackBox.GetComponent<CalculateDistances>().CalculatexyYDistance(baseColorxyY, coordinate);

            //CalculateP3Distance also takes two arguments, the base color and this color
            //In this script, the color was saved in a variable earlier, we just have to convert the base color via ConvertToP3 script
            //The result is saved in the data script
            circleData.P3ColorDistanceToBase = blackBox.GetComponent<CalculateDistances>().CalculateP3Distance(BaseConvertedNew, circleColor);

        
        }
    }
}
