using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform_controller : raycast_controller{
    
    public Vector3 move;
    public LayerMask passengerMask;

    public override void Start(){
        base.Start();
    }

    void Update(){

        UpdateRaycastOrigins();

        Vector3 velocity = move * Time.deltaTime;

        MovePassengers(velocity);
        transform.Translate(velocity);
    }

    void MovePassengers(Vector3 velocity){

        HashSet<Transform> movePassengers = new HashSet<Transform>();
        float directionX = Mathf.Sign(velocity.x);
        float directionY = Mathf.Sign(velocity.y);

        if (velocity.y != 0) {
			float rayLength = Mathf.Abs (velocity.y) + skinWidth;
			
			for (int i = 0; i < verticalRayCount; i ++) {
				Vector2 rayOrigin = (directionY == -1)?raycastOrigins.bottomLeft:raycastOrigins.topLeft;
				rayOrigin += Vector2.right * (verticalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, passengerMask);

                Debug.DrawRay(rayOrigin, Vector2.up * rayLength,Color.red);

				if (hit) {
					if (!movePassengers.Contains(hit.transform)) {
						movePassengers.Add(hit.transform);
						float pushX = (directionY == 1)?velocity.x:0;
						float pushY = velocity.y - (hit.distance - skinWidth) * directionY;

						hit.transform.Translate(new Vector3(pushX,pushY));
					}
				}
			}
		}
    }
}
