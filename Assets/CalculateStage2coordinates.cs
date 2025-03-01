using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using System.Text;

public class CalculateStage2coordinates : MonoBehaviour
{
    List<Vector3> stage2Coordinates = new List<Vector3>();

    Vector3 baseColorClass;
    Vector3[] directions = new Vector3[]
{
    new Vector3(1, 0, 0),    // Right (0°)
    new Vector3(1, 1, 0),    // Top-Right (45°)
    new Vector3(0, 1, 0),    // Up (90°)
    new Vector3(-1, 1, 0),   // Top-Left (135°)
    new Vector3(-1, 0, 0),   // Left (180°)
    new Vector3(-1, -1, 0),  // Bottom-Left (225°)
    new Vector3(0, -1, 0),   // Down (270°)
    new Vector3(1, -1, 0)    // Bottom-Right (315°)
};

    string pathDirection = Application.dataPath + "/ColorMathDirection.csv";
    string pathCurrentCoordinate = Application.dataPath + "/ColorMathCurrentCoordinate.csv";

    List<(Vector3, bool)> direction0 = new List<(Vector3, bool)>();//TRUE=Selected/different, False=not-selected/same
    List<(Vector3, bool)> direction1 = new List<(Vector3, bool)>();//TRUE=Selected/different, False=not-selected/same
    List<(Vector3, bool)> direction2 = new List<(Vector3, bool)>();//TRUE=Selected/different, False=not-selected/same
    List<(Vector3, bool)> direction3 = new List<(Vector3, bool)>();//TRUE=Selected/different, False=not-selected/same
    List<(Vector3, bool)> direction4 = new List<(Vector3, bool)>();//TRUE=Selected/different, False=not-selected/same
    List<(Vector3, bool)> direction5 = new List<(Vector3, bool)>();//TRUE=Selected/different, False=not-selected/same
    List<(Vector3, bool)> direction6 = new List<(Vector3, bool)>();//TRUE=Selected/different, False=not-selected/same
    List<(Vector3, bool)> direction7 = new List<(Vector3, bool)>();//TRUE=Selected/different, False=not-selected/same

    List<List<(Vector3, bool)>>[] directionLists;

    float circleExpansion = 0.0013f;

    

    void CategorizeCoordinate(Vector3 currentCoordinate, bool isSelected)
    {
        if (currentCoordinate == baseColorClass) return;

        // Compute direction vector and normalize it
        Vector3 directionVector = (currentCoordinate - baseColorClass).normalized;
        /*
        //write direction to file for seeing directions
        if (!File.Exists(pathDirection))
        {
            File.Create(pathDirection).Close();
        }
        string content = "\n" + directionVector.x.ToString() + "," + directionVector.y.ToString() + "," + directionVector.z.ToString();
        File.AppendAllText(pathDirection, content);

        if (!File.Exists(pathCurrentCoordinate))
        {
            File.Create(pathCurrentCoordinate).Close();
        }
        content = "\n" + currentCoordinate.x.ToString() + "," + currentCoordinate.y.ToString() + "," + currentCoordinate.z.ToString();
        File.AppendAllText(pathCurrentCoordinate, content);

        Debug.Log("Direction written to file");
        */

        
        // Find the closest direction using dot product
        float directionAngle = Mathf.Atan2(directionVector[0], directionVector[1]);


        if (directionAngle>1.5f && directionAngle<1.6f)
        {
            direction0.Add((currentCoordinate, isSelected));
        }
        else if (directionAngle > 0.7f && directionAngle < 0.8f)
        {
            direction1.Add((currentCoordinate, isSelected));
        }
        else if (directionAngle > -0.1f && directionAngle < 0.1f)
        {
            direction2.Add((currentCoordinate, isSelected));
        }
        else if (directionAngle > -0.8f && directionAngle < -0.7f)
        {
            direction3.Add((currentCoordinate, isSelected));
        }
        else if (directionAngle > -1.6f && directionAngle < -1.5f)
        {
            direction4.Add((currentCoordinate, isSelected));
        }
        else if (directionAngle > -2.4f && directionAngle < -2.3f)
        {
            direction5.Add((currentCoordinate, isSelected));
        }
        else if (directionAngle > 3.1f && directionAngle < 3.2f)
        {
            direction6.Add((currentCoordinate, isSelected));
        }
        else if (directionAngle > 2.3f && directionAngle < 2.4f)
        {
            direction7.Add((currentCoordinate, isSelected));
        }
        else
        {
            Debug.Log("No direction match for: " + currentCoordinate);
        }
    }

    public List<Vector3> Stage2Coordinates(List<Vector3> selected, List<Vector3> unSelected, Vector3 baseColor)//change to whatever is provided
    {
        List<(Vector3, bool)>[] directionLists = new List<(Vector3, bool)>[]
        {
            direction4, direction5, direction6, direction7,
            direction0, direction1, direction2, direction3
        };
        baseColorClass = baseColor;
        


        foreach (Vector3 currentCoordinate in selected)
        {
            CategorizeCoordinate(currentCoordinate, true);
        }
        foreach (Vector3 currentCoordinate in unSelected)
        {
            CategorizeCoordinate(currentCoordinate, false);
        }


        
        

        stage2Coordinates.AddRange(calculateStage2CoordinatesForDirection(direction0, baseColor));
        stage2Coordinates.AddRange(calculateStage2CoordinatesForDirection(direction1, baseColor));
        stage2Coordinates.AddRange(calculateStage2CoordinatesForDirection(direction2, baseColor));
        stage2Coordinates.AddRange(calculateStage2CoordinatesForDirection(direction3, baseColor));
        stage2Coordinates.AddRange(calculateStage2CoordinatesForDirection(direction4, baseColor));
        stage2Coordinates.AddRange(calculateStage2CoordinatesForDirection(direction5, baseColor));
        stage2Coordinates.AddRange(calculateStage2CoordinatesForDirection(direction6, baseColor));
        stage2Coordinates.AddRange(calculateStage2CoordinatesForDirection(direction7, baseColor));

        Debug.Log("CalculateStage2Coordinates ran without problems and created " + stage2Coordinates.Count + " points");
        Random.seed = 210999;
        for (int i = stage2Coordinates.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (stage2Coordinates[i], stage2Coordinates[j]) = (stage2Coordinates[j], stage2Coordinates[i]);
        }
        return stage2Coordinates;
    }

    List<Vector3> calculateStage2CoordinatesForDirection(List<(Vector3, bool)> direction, Vector3 basexyY)
    {

        Vector3 nullVector = new Vector3();
        Vector3 startCoordinate = new Vector3();
        Vector3 endCoordinate = new Vector3();

        List<Vector3> returnCoordinates = new List<Vector3>();
        // Sort the list based on the distance between each Vector3 and basexyY
        List<(Vector3, bool)> ordered = direction.OrderBy(item => Vector3.Distance(item.Item1, basexyY)).ToList();
        Debug.Log("Ordered list count: " + ordered.Count);


        for (int i = 0; i < ordered.Count; i += 2)
        {
            if (i + 1 >= ordered.Count)
                break;
            if (ordered[i].Item2 || ordered[i + 1].Item2)
            {
                if (startCoordinate == nullVector)
                {
                    startCoordinate = ordered[i].Item1;
                    Debug.Log(startCoordinate * 100);
                }
            }
            if (!ordered[i].Item2 || !ordered[i + 1].Item2)
            {
                endCoordinate = ordered[i].Item1;
            }

            /*
            if ((!ordered[i].Item2 && ordered[i + 1].Item2) || (ordered[i].Item2 && !ordered[i + 1].Item2))
            {
                if (startCoordinate == nullVector)
                {
                    startCoordinate = ordered[i].Item1;
                    Debug.Log(startCoordinate * 100);
                }
                endCoordinate = ordered[i].Item1;
            }
            */
        }


        if (startCoordinate == nullVector)//case 2 on paper
        {
            startCoordinate = ordered[0].Item1;
            endCoordinate = ordered[ordered.Count - 1].Item1;
            returnCoordinates.Add(startCoordinate);
            returnCoordinates.Add(Vector3.Lerp(startCoordinate, endCoordinate, (1f / 3f)));
            returnCoordinates.Add(Vector3.Lerp(startCoordinate, endCoordinate, (2f / 3f)));
            returnCoordinates.Add(endCoordinate);
            return returnCoordinates;

        }
        if (endCoordinate == nullVector)//case 3 on paper
        {
            startCoordinate = ordered[0].Item1;
            endCoordinate = ordered[ordered.Count - 1].Item1;
            returnCoordinates.Add(startCoordinate);
            returnCoordinates.Add(Vector3.Lerp(startCoordinate, endCoordinate, (1f / 3f)));
            returnCoordinates.Add(Vector3.Lerp(startCoordinate, endCoordinate, (2f / 3f)));
            returnCoordinates.Add(endCoordinate);
            return returnCoordinates;

        }


        /*
        float distance = Vector3.Distance(endCoordinate, startCoordinate);

        float halfExpansion = circleExpansion / 2;
        float stepsf = distance / halfExpansion;
        int steps = (int) Mathf.RoundToInt(stepsf);
        Debug.Log("stepsf");
        Debug.Log(stepsf);
        Debug.Log("steps");
        Debug.Log(steps);
        */

        Vector3 unitVector = Vector3.Normalize((startCoordinate - basexyY));
        Vector3 stepVector = unitVector * (circleExpansion / 2);//im stuck
        //
        Vector3 point1 = startCoordinate + (stepVector * -1);
        Vector3 point4 = endCoordinate + stepVector;

        if (point1 == point4)//case 1 on paper ie the point i each dicrection is the same.
        {
            returnCoordinates.Add(startCoordinate);
            returnCoordinates.Add(Vector3.Lerp(startCoordinate, endCoordinate, (1f / 3f)));
            returnCoordinates.Add(Vector3.Lerp(startCoordinate, endCoordinate, (2f / 3f)));
            returnCoordinates.Add(endCoordinate);

            return returnCoordinates;
        }
        Vector3 point2 = Vector3.Lerp(point1, point4, (1f / 3f));
        Vector3 point3 = Vector3.Lerp(point1, point4, (2f / 3f));
        //
        /*
        for (int step = -1; step <= steps + 1; step++)
        {

            returnCoordinates.Add(startCoordinate + stepVector * step);
        }
        */
        returnCoordinates.Add(point1);
        returnCoordinates.Add(point2);
        returnCoordinates.Add(point3);
        returnCoordinates.Add(point4);

        return returnCoordinates;
    }
}
