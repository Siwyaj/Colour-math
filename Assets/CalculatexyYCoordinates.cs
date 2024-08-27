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
    float circleExpansion = 0.0013f;

    float[,] sRGBToxyY = { { 0.8225f, 0.1774f, 0.0000f },
                                   { 0.0332f, 0.9669f, 0.0000f },
                                   { 0.0171f, 0.0724f, 0.9108f } };//endavid

    public List<Vector3> CreateCoordinates(Vector3 centerCoordinate)
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
        xyYCoordinates.AddRange(xyYCoordinates);
        return xyYCoordinates;
    }
}
