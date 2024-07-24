using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatexyYCoordinates : MonoBehaviour
{
    //Initial values
    public List<Vector3> xyYCoordinates;
    List<int> directions;
    int nDirections = 8;
    int nCircles = 6;
    float startExpansion = 0.0005f;

    public List<Vector3> CreateCoordinates(Vector3 centerCoordinate, float circleExpansion = 0.0013f)
    {
        xyYCoordinates = new List<Vector3>();
        directions = new List<int>();
        xyYCoordinates.Add(centerCoordinate);
        for (int direction = 0; direction < nDirections; direction++)
        {
            for (int circle = 0; circle < nCircles; circle++)
            {
                float x = Mathf.Cos((2 * Mathf.PI / nDirections) * direction) * (circle * circleExpansion + startExpansion) + centerCoordinate[0];
                float y = Mathf.Sin((2 * Mathf.PI / nDirections) * direction) * (circle * circleExpansion + startExpansion) + centerCoordinate[1];
                Vector3 currentCoordinate = new Vector3(x, y, centerCoordinate[2]);

                xyYCoordinates.Add(currentCoordinate);
                directions.Add(direction);
            }
        }
        return xyYCoordinates;
    }
}
