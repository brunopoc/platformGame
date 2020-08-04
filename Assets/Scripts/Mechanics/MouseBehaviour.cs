using UnityEngine;
using System.Collections;

public class MouseBehaviour : MonoBehaviour
{

    public GameObject position;
    Vector2 newMousePosition;

    void Start()
    {
        position.transform.position = Input.mousePosition;
    }

    void Update()
    {
        Vector3 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        newMousePosition.x = ray.x;
        newMousePosition.y = ray.y;
        position.transform.position = newMousePosition;
    }
}