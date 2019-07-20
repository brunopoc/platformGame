using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform_controller : raycast_controller{
    
    public Vector3 move;

    public override void Start(){
        base.Start();
    }

    // Update is called once per frame
    void Update(){
        Vector3 velocity = move * Time.deltaTime;
        transform.Translate(velocity);
    }
}
