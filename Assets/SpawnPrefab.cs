using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    public GameObject BlackBox;
    public GameObject circlePrefab;
    public List<Vector2> coordinates;

    Vector2 baseColour = new Vector2(0.67f, 0.33f); //blue: (0.2f, 0.14f) Green: () Red:() P3redtest:(0.67f, 0.33f)

    // Start is called before the first frame update
    void Start()
    {
        coordinates = new List<Vector2>();
        GameObject circle = Instantiate(circlePrefab, new Vector2(0f,0f), Quaternion.identity);
        circle.GetComponent<SpriteRenderer>().color = BlackBox.GetComponent<ConvertToP3>().Convert(baseColour);
        coordinates.AddRange(BlackBox.GetComponent<CalculateCIE1931xyCoordinates>().CreateCoordinates(baseColour));
        foreach(Vector2 coordinate in coordinates)
        {
            circle = Instantiate(circlePrefab, (coordinate - baseColour)*100, Quaternion.identity);
            circle.GetComponent<SpriteRenderer>().color = BlackBox.GetComponent<ConvertToP3>().Convert(coordinate);
        }
    }
}
