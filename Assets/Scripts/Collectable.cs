using UnityEngine;

public class Collectable : MonoBehaviour 
{
    [SerializeField] private AudioClip _audioClip;

    public AudioClip PickUpSound { get { return _audioClip; } }
}
