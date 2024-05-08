using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    public GameObject BlackBox;
    public GameObject circlePrefab;
    public List<Vector2> coordinates;

    // Start is called before the first frame update
    void Start()
    {
        coordinates = new List<Vector2>();
        coordinates.AddRange(BlackBox.GetComponent<CalculateCIE1931xyCoordinates>().CreateCoordinates(new Vector2(0f,0f)));
        foreach(Vector2 coordinate in coordinates)
        {
            GameObject circle = Instantiate(circlePrefab, coordinate,Quaternion.identity);
        }
    }
}
