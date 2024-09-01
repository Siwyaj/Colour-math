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

    public Vector3 XYZCoordinate;
    public Vector3 gammeminus;
    public Vector3 centerCoordinatexyY;

    /*
    float[,] sRGBToXYZ = { { 0.4124564f, 0.3575761f, 0.1804375f },
                                   { 0.2126729f, 0.7151522f, 0.0721750f },
                                   { 0.0193339f, 0.1191920f, 0.9503041f } };//Bruce
    */
    float[,] sRGBToXYZ = { { 0.4177f, 0.3468f, 0.1859f },
                                   { 0.2201f, 0.7185f, 0.0609f },
                                   { 0.0182f, 0.1282f, 0.9426f } };//Emilie
    public List<Vector3> CreateCoordinates(Vector3 centerCoordinatesRGB)
    {
        
        gammeminus = GammaMinus(centerCoordinatesRGB);
        //Debug.Log("Gamma reduced(*10)"+gammeminus*10);
        XYZCoordinate = ConvertsRGBToXYZ(gammeminus);
        //Debug.Log("XYZ(*10)" + XYZCoordinate * 10);
        centerCoordinatexyY = ConvertXYZToxyY(XYZCoordinate);
        //Debug.Log("x(*10): " + centerCoordinatexyY[0]*10+ "y(*10): " + centerCoordinatexyY[1]*10 + "Y(*10): " + centerCoordinatexyY[2]*10);


        xyYCoordinates = new List<Vector3>();
        xyYCoordinates.Add(centerCoordinatexyY);
        for (int direction = 0; direction < nDirections; direction++)
        {
            for (int circle = 0; circle < nCircles; circle++)
            {
                float x = Mathf.Cos((2 * Mathf.PI / nDirections) * direction) * (circle * circleExpansion + startExpansion) + centerCoordinatexyY[0];
                float y = Mathf.Sin((2 * Mathf.PI / nDirections) * direction) * (circle * circleExpansion + startExpansion) + centerCoordinatexyY[1];
                Vector3 currentCoordinate = new Vector3(x, y, centerCoordinatexyY[2]);

                xyYCoordinates.Add(currentCoordinate);
            }
        }
        xyYCoordinates.AddRange(xyYCoordinates);
        return xyYCoordinates;
    }

    Vector3 GammaMinus(Vector3 sRGB)
    {
        Vector3 gammaMinus = new Vector3();
        for (int i = 0; i < 3; i++)
        {
            if (sRGB[i] <= 0.04045f)
            {
                gammaMinus[i] = sRGB[i] / 12.92f;
            }
            else
            {
                Debug.Log("else case ran"+ Mathf.Pow((sRGB[i] + 0.055f) / 1.055f, 2.4f));
                gammaMinus[i]=Mathf.Pow((sRGB[i]+0.055f)/1.055f, 2.4f);

            }
        }
        return gammaMinus;
    }
    Vector3 ConvertsRGBToXYZ(Vector3 sRGB)
    {
        
        float X = sRGB[0] * sRGBToXYZ[0, 0] + sRGB[1] * sRGBToXYZ[0, 1] + sRGB[2] * sRGBToXYZ[0, 2];
        float Y = sRGB[0] * sRGBToXYZ[1, 0] + sRGB[1] * sRGBToXYZ[1, 1] + sRGB[2] * sRGBToXYZ[1, 2];
        float Z = sRGB[0] * sRGBToXYZ[2, 0] + sRGB[1] * sRGBToXYZ[2, 1] + sRGB[2] * sRGBToXYZ[2, 2];
        Vector3 XYZ = new Vector3(X,Y,Z);
        return XYZ;
    }

    Vector3 ConvertXYZToxyY(Vector3 XYZCoordinate)
    {
        float x = XYZCoordinate[0] / (XYZCoordinate[0] + XYZCoordinate[1] + XYZCoordinate[2]);
        float y = XYZCoordinate[1] / (XYZCoordinate[0] + XYZCoordinate[1] + XYZCoordinate[2]);
        float Y = XYZCoordinate[1];

        return new Vector3(x,y,Y);
    }
}
