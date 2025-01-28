using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc : MonoBehaviour
{
   public AudioSource audio;

void Update()
{
if(Input.GetKey(KeyCode.Mouse0) )
{
audio.Play();
}
}
}
