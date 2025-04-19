using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource source;
    // Start is called before the first frame update
    private void Awake(){
        source = GetComponent<AudioSource>();
    }
    public void PlaySound(AudioClip _sound){
        source.PlayOneShot(_sound);
    }
}
