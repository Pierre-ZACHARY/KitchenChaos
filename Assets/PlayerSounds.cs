using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private SO_AudioClipRefs audioClipRefs;
    
    public void PlayFootstepSound()
    {
        AudioClip clip = audioClipRefs.footstep[Random.Range(0, audioClipRefs.footstep.Length)];
        Debug.Log("PlayFootstepSound : " + clip.name);
        AudioSource.PlayClipAtPoint(clip, transform.position, 1.0f);
    }
}
