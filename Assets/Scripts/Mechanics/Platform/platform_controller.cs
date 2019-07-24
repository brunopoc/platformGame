using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform_controller : raycast_controller{

    public LayerMask passengerMask;

    public Vector3[] localWaypoints;
    Vector3[] globalWaypoints;

    public float speed;
    public bool cyclic;
    int fromWaypointIndex;
    float percentBetweenWaypoints;

    List<PassengerMovement> passengerMovements;
    Dictionary<Transform, controller2D> passengerDictionary = new Dictionary<Transform, controller2D>();

    public override void Start() {
        base.Start();

        globalWaypoints = new Vector3[localWaypoints.Length];
        for (int i = 0; i < localWaypoints.Length; i++){
            globalWaypoints[i] = localWaypoints[i] + transform.position;
        }
    }

    void Update() {

        UpdateRaycastOrigins();

        Vector3 velocity = CalculatePlatformMovement();

        CalculatePassengerMovement(velocity);
        MovePassengers (true);
        transform.Translate(velocity);
        MovePassengers (false);
    }

    Vector3 CalculatePlatformMovement(){
        fromWaypointIndex %= globalWaypoints.Length;
        int toWaypointIndex = (fromWaypointIndex + 1) % globalWaypoints.Length;
        float distanceBetweenWaypoints = Vector3.Distance(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex]);
        percentBetweenWaypoints += Time.deltaTime * speed / distanceBetweenWaypoints;
        
        Vector3 newPos = Vector3.Lerp(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex], percentBetweenWaypoints);
        
        if(percentBetweenWaypoints >= 1){
            percentBetweenWaypoints = 0;
            fromWaypointIndex ++;
            if(!cyclic){
                if(fromWaypointIndex >= globalWaypoints.Length - 1){
                    fromWaypointIndex = 0;
                    System.Array.Reverse(globalWaypoints);
                }
            }
        }

        return newPos - transform.position;
    }

    void MovePassengers(bool beforeMovePlatform) {
        foreach(PassengerMovement passenger in passengerMovements){
            if(!passengerDictionary.ContainsKey(passenger.transform)){
                passengerDictionary.Add(passenger.transform, passenger.transform.GetComponent<controller2D>());
            }
            if(passenger.moveBeforePlatform == beforeMovePlatform){
                passengerDictionary[passenger.transform].Move(passenger.velocity, passenger.standingOnPlatform);
            }
        }
    }

    void CalculatePassengerMovement(Vector3 velocity) {
        HashSet<Transform> movePassengers = new HashSet<Transform>();
        passengerMovements = new List<PassengerMovement>();
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
                        passengerMovements.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), directionY == 1, true));
						
					}
				}
			}
		}

        if(velocity.x != 0) {
            float rayLength  = Mathf.Abs(velocity.x) + skinWidth;
            for(int i = 0; i < horizontalRayCount; i++){
                Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
                rayOrigin += Vector2.up * (horizontalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, passengerMask); 
                if (hit) {
                    if (!movePassengers.Contains(hit.transform)) {
                        movePassengers.Add(hit.transform);
                        float pushX = velocity.x - (hit.distance - skinWidth) * directionX;
                        float pushY = -skinWidth;

                        passengerMovements.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), false, true));
                    }
                }
            }
        }

        if(directionY == -1 || velocity.y == 0 && velocity.x != 0) {
            float rayLength = skinWidth * 2;
			
			for (int i = 0; i < verticalRayCount; i ++) {
				Vector2 rayOrigin = raycastOrigins.topLeft + Vector2.right * (verticalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, passengerMask);

                Debug.DrawRay(rayOrigin, Vector2.up * rayLength,Color.red);

				if (hit) {
					if (!movePassengers.Contains(hit.transform)) {
						movePassengers.Add(hit.transform);
						float pushX = velocity.x;
						float pushY = velocity.y;

						passengerMovements.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), true, false));
					}
				}
			}
        }
    }

    struct PassengerMovement {
        public Transform transform;
        public Vector3 velocity;
        public bool standingOnPlatform;
        public bool moveBeforePlatform;

        public PassengerMovement (Transform _transform, Vector3 _velocity, bool _standingOnPlatform, bool _moveBeforePlatform){
            transform = _transform;
            velocity = _velocity;
            standingOnPlatform = _standingOnPlatform;
            moveBeforePlatform = _moveBeforePlatform;
        }
    }

    void OnDrawGizmos() {
        if(localWaypoints != null){
            Gizmos.color = Color.red;
            float size = .1f;
            for (int i = 0; i < localWaypoints.Length; i++){
                Vector3 globalWaypointPos = (Application.isPlaying)? globalWaypoints[i] : localWaypoints[i] + transform.position;
                Gizmos.DrawLine(globalWaypointPos - Vector3.up * size, globalWaypointPos + Vector3.up * size);
                Gizmos.DrawLine(globalWaypointPos - Vector3.left * size, globalWaypointPos + Vector3.left * size);
            }
        }
    }
}
