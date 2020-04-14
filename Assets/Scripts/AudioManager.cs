using System;
using System.Collections;
using UnityEngine;
namespace Chess.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;
        public static AudioManager instance;
        public Sound[] theme_sounds;
        void Awake()
        {
            if (instance == null)
                instance = this;
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
            foreach (Sound s in sounds)
            {
                init_sound(s);
            }
            foreach (var s in theme_sounds)
            {
                init_sound(s);
            }
        }
        private void init_sound(Sound s)
        {
          
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                // s.source.pitch = s.pitch;
                s.source.volume = s.volume;
                s.source.loop = s.loop;
            
        }
        private void Start()
        {
            StartCoroutine(theme());
            
        }
        IEnumerator theme()
        {
            foreach (var item in theme_sounds)
            {
                item.source.PlayOneShot(item.clip);
                yield return new WaitForSeconds(item.clip.length);
            }
        }
        public void Play(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s==null)
            {
                Debug.LogWarningFormat("NO SUCH sound with name {0}", name);
                return;
            }

            s.source.PlayOneShot(s.clip);
        }
    }
}