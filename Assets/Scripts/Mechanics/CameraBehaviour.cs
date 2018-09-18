using UnityEngine;
using System.Collections;

public class cameraBehaviour : MonoBehaviour {

    public Transform targetPlayer;
    public float groundLevel = 0.553f;
    public float velocity;

    public bool needTranslate;

    void Start (){

	
    }
    void LateUpdate (){

        if (targetPlayer.position.y > 2) { // --- MOVIMENTAR A CAMERA
            if (this.gameObject.transform.position.y < targetPlayer.position.y && needTranslate == true) {
                this.gameObject.transform.Translate(0, velocity, 0);
                this.gameObject.transform.position = new Vector3(targetPlayer.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            } else {
                this.gameObject.transform.position = new Vector3(targetPlayer.position.x, targetPlayer.position.y, this.gameObject.transform.position.z);
                needTranslate = false;
            }
        } else {
            if (this.gameObject.transform.position.y > groundLevel) {
                this.gameObject.transform.Translate(0, -velocity, 0);
                this.gameObject.transform.position = new Vector3(targetPlayer.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            } else {
                this.gameObject.transform.position = new Vector3(targetPlayer.position.x, groundLevel, this.gameObject.transform.position.z);
            }
            needTranslate = true;
        }  
    }
}

