using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walk : MonoBehaviour
{
   public AudioSource WalkAudio;

void Update()
{
if(Input.GetKeyDown(KeyCode.W) )
{
WalkAudio.Play();
}
}
}
