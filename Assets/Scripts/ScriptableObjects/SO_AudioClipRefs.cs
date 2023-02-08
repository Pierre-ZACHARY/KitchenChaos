using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipRefs", menuName = "ScriptableObjects/AudioClipRefs", order = 1)]
public class SO_AudioClipRefs : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] deliverySuccess;
    public AudioClip[] deliveryFail;
    public AudioClip[] footstep;
    public AudioClip[] objectDrop;
    public AudioClip[] objectPickup;
    public AudioClip[] trash;
    public AudioClip[] warning;
    public AudioClip stoveSizzle;
}
