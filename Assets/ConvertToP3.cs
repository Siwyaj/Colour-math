using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertToP3 : MonoBehaviour
{
    Color P3Colour;


    public Color Convert(Vector2 coordinate, float luminance = 0.3f)
    {
        Vector3 XYZ = ConvertxyYToXYZ(coordinate, luminance);



        return P3Colour;
    }



    Vector3 ConvertxyYToXYZ(Vector2 xy, float Y)
    {
        if (xy[1] == 0)
        {
            return new Vector3(0, 0, 0);
        }
        float X = (xy[0] * Y) / xy[1];
        float Z = ((1 - xy[0] - xy[1]) * Y) / xy[1];
        Vector3 XYZ = new Vector3(X, Y, Z);
        return XYZ;
    }
}
