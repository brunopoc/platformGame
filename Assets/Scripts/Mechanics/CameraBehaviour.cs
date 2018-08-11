using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

    public Transform targetPlayer;

    void Start (){
	
    }
    void Update (){
	    this.gameObject.transform.position = new Vector3(targetPlayer.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    }
}