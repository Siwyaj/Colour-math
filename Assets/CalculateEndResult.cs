using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CalculateEndResult : MonoBehaviour
{
    List<Vector3> finalMedianCoordinates = new List<Vector3>();

    List<(Vector3, bool)> direction0 = new List<(Vector3, bool)>();//TRUE=Selected/different, False=not-selected/same
    List<(Vector3, bool)> direction1 = new List<(Vector3, bool)>();//TRUE=Selected/different, False=not-selected/same
    List<(Vector3, bool)> direction2 = new List<(Vector3, bool)>();//TRUE=Selected/different, False=not-selected/same
    List<(Vector3, bool)> direction3 = new List<(Vector3, bool)>();//TRUE=Selected/different, False=not-selected/same
    List<(Vector3, bool)> direction4 = new List<(Vector3, bool)>();//TRUE=Selected/different, False=not-selected/same
    List<(Vector3, bool)> direction5 = new List<(Vector3, bool)>();//TRUE=Selected/different, False=not-selected/same
    List<(Vector3, bool)> direction6 = new List<(Vector3, bool)>();//TRUE=Selected/different, False=not-selected/same
    List<(Vector3, bool)> direction7 = new List<(Vector3, bool)>();//TRUE=Selected/different, False=not-selected/same

    public List<Vector3> CalculateEndPoints(List<Vector3> selectedStage2,List<Vector3> notSelectedStage3, Vector3 basePoint)
    {



        foreach (Vector3 currentCoordinate in selectedStage2)
        {
            //Vector3 currentCoordinate = selectedCircle.GetComponent<data>().xyYCoordinate;
            if (currentCoordinate != basePoint)
            {
                float angle = Mathf.Atan2(basePoint[1] - currentCoordinate[1], basePoint[0] - currentCoordinate[0]);
                switch (angle)
                {
                    case > 3.1f and < 3.2f:
                        direction0.Add((currentCoordinate, true));
                        break;
                    case > -2.4f and < -2.3f:
                        direction1.Add((currentCoordinate, true));
                        break;
                    case > -1.6f and < -1.5f:
                        direction2.Add((currentCoordinate, true));
                        break;
                    case > -0.8f and < -0.7f:
                        direction3.Add((currentCoordinate, true));
                        break;
                    case > -0.1f and < 0.1f:
                        direction4.Add((currentCoordinate, true));
                        break;
                    case < 2.4f and > 2.3f:
                        direction5.Add((currentCoordinate, true));
                        break;
                    case < 1.6f and > 1.5f:
                        direction6.Add((currentCoordinate, true));
                        break;
                    case < 0.8f and > 0.7f:
                        direction7.Add((currentCoordinate, true));
                        break;
                    default:
                        break;
                }


            }
        }
        foreach (Vector3 currentCoordinate in notSelectedStage3)
        {
            //Vector3 currentCoordinate = unSelectedCircle.GetComponent<data>().xyYCoordinate;
            if (currentCoordinate != basePoint)
            {
                float angle = Mathf.Atan2(basePoint[1] - currentCoordinate[1], basePoint[0] - currentCoordinate[0]);
                switch (angle)
                {
                    case > 3.1f and < 3.2f:
                        direction0.Add((currentCoordinate, false));
                        break;
                    case > -2.4f and < -2.3f:
                        direction1.Add((currentCoordinate, false));
                        break;
                    case > -1.6f and < -1.5f:
                        direction2.Add((currentCoordinate, false));
                        break;
                    case > -0.8f and < -0.7f:
                        direction3.Add((currentCoordinate, false));
                        break;
                    case > -0.1f and < 0.1f:
                        direction4.Add((currentCoordinate, false));
                        break;
                    case < 2.4f and > 2.3f:
                        direction5.Add((currentCoordinate, false));
                        break;
                    case < 1.6f and > 1.5f:
                        direction6.Add((currentCoordinate, false));
                        break;
                    case < 0.8f and > 0.7f:
                        direction7.Add((currentCoordinate, false));
                        break;
                    default:
                        break;
                }
            }
        }



        finalMedianCoordinates.Add(calculateFinalPointForDirection(direction0, basePoint));
        finalMedianCoordinates.Add(calculateFinalPointForDirection(direction1, basePoint));
        finalMedianCoordinates.Add(calculateFinalPointForDirection(direction2, basePoint));
        finalMedianCoordinates.Add(calculateFinalPointForDirection(direction3, basePoint));
        finalMedianCoordinates.Add(calculateFinalPointForDirection(direction4, basePoint));
        finalMedianCoordinates.Add(calculateFinalPointForDirection(direction5, basePoint));
        finalMedianCoordinates.Add(calculateFinalPointForDirection(direction6, basePoint));
        finalMedianCoordinates.Add(calculateFinalPointForDirection(direction7, basePoint)); 


        return finalMedianCoordinates;
    }


    Vector3 calculateFinalPointForDirection(List<(Vector3, bool)> direction, Vector3 basexyY)
    {
        Vector3 medianPoint = new Vector3();
        Vector3 startpoint = new Vector3();
        Vector3 endPoint = new Vector3();

        List<(Vector3, bool)> ordered = direction.OrderBy(item => Vector3.Distance(item.Item1, basexyY)).ToList();

        for(int i=0; i < ordered.Count; i++)
        {
            if (ordered[i].Item2)
            {
                if (startpoint == new Vector3())//first selected point
                {
                    startpoint = ordered[i].Item1;
                }
            }
            else
            {
                endPoint = ordered[i].Item1;
            }

        }

        medianPoint = Vector3.Lerp(startpoint, endPoint, 0.5f);
       

        return medianPoint;
    }
}
