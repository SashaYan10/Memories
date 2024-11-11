using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowVolume : MonoBehaviour
{
    public Button muteButton;
    public AudioSource notMute;
    public float muteDuration = 5f;

    private void Start()
    {
        muteButton.onClick.AddListener(Mute);
    }
    public void Mute()
    {
        StartCoroutine(MuteAll());
    }

    private IEnumerator MuteAll()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource != notMute)
            {
                audioSource.volume = 0;
            }
        }

        yield return new WaitForSeconds(muteDuration);

        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource != notMute)
            {
                audioSource.volume = 1;
            }
        }
    }
}
