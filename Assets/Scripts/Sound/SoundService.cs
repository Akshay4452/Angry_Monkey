using System;
using UnityEngine;

namespace ServiceLocator.Sound
{
    public class SoundService : MonoBehaviour
    {
        [SerializeField] private SoundScriptableObject soundScriptableObject;
        [SerializeField] private AudioSource audioEffects;
        [SerializeField] private AudioSource backgroundMusic;

        public static SoundService Instance { get { return instance; } }

        private static SoundService instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            } else
            {
                Destroy(this.gameObject);
                Debug.LogError("Singleton of SoundService is trying to create second instance\n");
            }
        }

        private void Start()
        {
            PlaybackgroundMusic(SoundType.BackgroundMusic, true);
        }

        public void PlaySoundEffects(SoundType soundType, bool loopSound = false)
        {
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                audioEffects.loop = loopSound;
                audioEffects.clip = clip;
                audioEffects.PlayOneShot(clip);
            }
            else
                Debug.LogError("No Audio Clip selected.");
        }

        private void PlaybackgroundMusic(SoundType soundType, bool loopSound = false)
        {
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                backgroundMusic.loop = loopSound;
                backgroundMusic.clip = clip;
                backgroundMusic.Play();
            }
            else
                Debug.LogError("No Audio Clip selected.");
        }

        private AudioClip GetSoundClip(SoundType soundType)
        {
            Sounds sound = Array.Find(soundScriptableObject.audioList, item => item.soundType == soundType);
            if (sound.audio != null)
                return sound.audio;
            return null;
        }
    }
}