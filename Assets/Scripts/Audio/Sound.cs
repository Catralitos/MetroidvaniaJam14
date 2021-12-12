using UnityEngine;

namespace Audio
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume = 1f;
        [Range(0.1f, 3f)]
        public float pitch = 1f;
        public bool loop;

        [HideInInspector]
        public AudioSource source;

        public void SetSource(AudioSource source)
        {
            source.clip = clip;

            source.volume = volume;
            source.pitch = pitch ;
            source.loop = loop;
            this.source = source;
        }

        public void Play()
        {
            //Debug.Log(source.clip);
            source.Play();
        }

        public void PlayScheduled(double time)
        {
            source.PlayScheduled(time);
        }

        public void Stop()
        {
            source.Stop();
        }

        public bool IsPlaying()
        {
            return source.isPlaying;
        }
    }
}

