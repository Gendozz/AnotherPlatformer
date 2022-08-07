using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource soundsSource;

    [SerializeField] private AudioSource musicSource;

    [SerializeField] private AudioClip[] musicThemes;

    [SerializeField]
    private float lowPitchRange = 0.95f;

    [SerializeField]
    private float highPitchRange = 1.05f;

    public static SoundManager SharedInstance = null;

    private void Awake()
    {
        if (SharedInstance == null)
        {
            SoundManager.SharedInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(PlayMusicClipsAround());
    }

    private IEnumerator PlayMusicClipsAround()
    {
        yield return null;

        for (int i = 0; i < musicThemes.Length; i++)
        {
            musicSource.clip = musicThemes[i];

            musicSource.Play();

            while (musicSource.isPlaying)
            {
                yield return null;
            }
            if (i == musicThemes.Length) i = 0;
        }
    }

    public void Play(AudioClip clip)
    {
        soundsSource.clip = clip;
        soundsSource.Play();
    }

    public void Play(AudioClip clip, bool isRandomPitch)
    {
        if (isRandomPitch)
        {
            soundsSource.pitch = Random.Range(lowPitchRange, highPitchRange);
        }
        soundsSource.clip = clip;
        soundsSource.Play();
    }

    // Play a single clip through the music source.
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void ChangeMusicPitch(float pitch)
    {
        musicSource.pitch = pitch;
    }

    // Play a random clip from an array, and randomize the pitch slightly.
    public void RandomSoundEffect(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        soundsSource.pitch = randomPitch;
        soundsSource.clip = clips[randomIndex];
        soundsSource.Play();
    }
}
