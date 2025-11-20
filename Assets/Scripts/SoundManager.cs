using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioResource;
    [SerializeField] private List<AudioClip> _soundsList = new List<AudioClip>();

    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }

    public void PlaySound(string soundName)
    {
        AudioClip sound = _soundsList.Find(x => x.name == soundName);

        if (sound != null)
        {
            _audioResource.PlayOneShot(sound);
        }
    }
}
