using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource[] audioSources;

    void Start()
    {
        audioSources = Object.FindObjectsByType<AudioSource>(FindObjectsSortMode.InstanceID);
    }
}