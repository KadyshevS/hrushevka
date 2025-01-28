using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse : MonoBehaviour

{
   public class ExampleClass : MonoBehaviour {
    public AudioSource clickAudio;
    void Update() {
        if (Input.GetMouseButtonDown(0))
            Debug.Log("Pressed left click.");
        
        if (Input.GetMouseButtonDown(1))
            Debug.Log("Pressed right click.");
        
        if (Input.GetMouseButtonDown(2))
            Debug.Log("Pressed middle click.");
        
    }
}

}
