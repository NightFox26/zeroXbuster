using UnityEngine;

public class AudioManager : MonoBehaviour
{	
	public AudioSource VoicesSource;
	public AudioSource EffectsSource;
	public AudioSource MusicSource;
	public float LowPitchRange = .95f;
	public float HighPitchRange = 1.05f;

	public static AudioManager Instance = null;
	
	// Initialize the singleton instance.
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
	}

	public void Play(AudioClip clip)
	{
		EffectsSource.clip = clip;
		EffectsSource.Play();
	}
	public void PlayVoice(AudioClip clip)
	{
		VoicesSource.clip = clip;
		VoicesSource.Play();
	}

	public void PlayMusic(AudioClip clip)
	{
		MusicSource.clip = clip;
		MusicSource.Play();
	}

	public void RandomSoundEffect(params AudioClip[] clips)
	{
		int randomIndex = Random.Range(0, clips.Length);
		float randomPitch = Random.Range(LowPitchRange, HighPitchRange);

		EffectsSource.pitch = randomPitch;
		EffectsSource.clip = clips[randomIndex];
		EffectsSource.Play();
	}
	
}