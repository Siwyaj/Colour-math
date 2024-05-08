using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateCIE1931xyCoordinates : MonoBehaviour
{

    List<Vector2> CIE1931xyCoordinates;
    int nDirections = 8;
    int nCircles = 6;
    float circleExpansion=0.05f;

    List<Vector2> CreateCoordinates(Vector2 centerCoordinate)
    {
        CIE1931xyCoordinates = new List<Vector2>();
        for(int circle=1; circle <= nCircles; circle++)
        {
            for(int direction = 1; direction <= nDirections; direction++)
            {

            }
        }


        return CIE1931xyCoordinates;
    }
}
