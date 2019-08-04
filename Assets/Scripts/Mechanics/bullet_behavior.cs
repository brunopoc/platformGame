using UnityEngine;
using System.Collections;

public class bullet_behavior : MonoBehaviour
{
    
    public Vector3 velocity;
    
    void Update()
    {
        Move();
    }
    
    public void Move()
    {
        transform.Translate(velocity.x * Time.deltaTime, 0, 0);
        Destroy(gameObject, .4f); 
    }
}