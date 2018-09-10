using UnityEngine;
using System.Collections;

public class fadeInOutBehaviour : MonoBehaviour {
    public Color colorStart;
    public Color colorEnd;
    public Renderer renderoption;
    public float duration = 1.0f;
    public float t = 1.0f;
    public bool  playfadeIn = false;
    public bool  playfadeOut = false;
    public scenesManager ScenesManager;

    void Start (){
	    colorStart = renderoption.material.color;
        colorEnd = new Color(colorStart.r, colorStart.g, colorStart.b, 0);
        ScenesManager = GameObject.Find("SceneManager").GetComponent<scenesManager>();
        ScenesManager.loadCurrentFade();
    }

    void Update (){
 	    if(playfadeIn == true){
            StartCoroutine(fadeIn());
            playfadeIn = false;
 	    }
 	    if(playfadeOut == true){
            StartCoroutine(fadeOut());
            playfadeOut = false;
 	    }
    }

    IEnumerator fadeIn (){
        for (t = 0; t < duration; t+= Time.deltaTime){
	        renderoption.material.color = Color.Lerp(colorStart, colorEnd, t/duration);
	        yield return 0;
	    }
    }

    IEnumerator fadeOut (){
	    for(t = 0; t < duration; t+= Time.deltaTime){
	        renderoption.material.color = Color.Lerp(colorEnd, colorStart, t/duration);
            yield return 0;
	    }
    }
}