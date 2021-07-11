using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
     AudioSource currentAmbience;
     AudioSource currentMusic;

     [Header("Audio Volumes")]
     [Range(0, 1)] public float ambienceVolume = 1f;
     [Range(0, 1)] public float musicVolume = 1f;
     [Range(0, 1)] public float sfxVolume = 1f;
     public float fadeRate = 0.1f;



     public void RequestAmbienceTrack(AudioClip newAmbience)
     {
        if (newAmbience == null)
        {
            return;
        }
        // If the newAmbience is the SAME as the current Ambience, abort.
        if (newAmbience == currentAmbience.clip)
               return;

          GameObject newAudioObject = CreateTrackSource(newAmbience, ambienceVolume);

          // Swap currentAmbience to new object
          AudioSource oldAmbience = currentAmbience;
          currentAmbience = newAudioObject.GetComponent<AudioSource>();

          // Begin track fading
          oldAmbience.GetComponent<TrackFading>().FadeOut();        // Fade out old
          currentAmbience.GetComponent<TrackFading>().FadeIn();     // Fade in new
     }


     public void RequestMusicTrack(AudioClip newMusic)
     {
        if (newMusic == null)
        {
            return;
        }

        // If the newAmbience is the SAME as the current Ambience, abort.
        if (newMusic == currentMusic.clip)
               return;

          GameObject newAudioObject = CreateTrackSource(newMusic, musicVolume);

          // Swap currentMusic to new object
          AudioSource oldMusic = currentMusic;
          currentMusic = newAudioObject.GetComponent<AudioSource>();

          // Begin track fading
          oldMusic.GetComponent<TrackFading>().FadeOut();        // Fade out old
          currentMusic.GetComponent<TrackFading>().FadeIn();      // Fade in new
     }


     private GameObject CreateTrackSource(AudioClip newClip, float newVolume)
     {
          // Create the object
          GameObject newAudioObject = new GameObject();
          AudioSource newSource;
          TrackFading newFading;
          newAudioObject.name = "ASource_" + newClip.name;

          // Set parent and audio source
          newAudioObject.transform.parent = transform;
          newSource = newAudioObject.AddComponent<AudioSource>();
          newFading = newAudioObject.AddComponent<TrackFading>();

          // Set details
          newSource.loop = true;
          newSource.playOnAwake = false;
          newSource.volume = 0.1f;
          newFading.maxVolume = newVolume;
          newFading.fadeRate = fadeRate;

          // Add new clip
          newSource.clip = newClip;

          // Play the clip
          newSource.Play();

          // Return the new audio source
          return newAudioObject;
     }


     // Create a gameObject with an AudioSource to play an SFX and add it to the audioList[].
     public void PlaySFX(AudioClip sfx)
     {
          AudioSource.PlayClipAtPoint(sfx, Camera.main.transform.position, sfxVolume);
     }

}
