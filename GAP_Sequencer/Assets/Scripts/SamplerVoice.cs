using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SamplerVoice : MonoBehaviour
{
	private AudioSource _audioSource;

	public void Play(AudioClip audioClip, float pitch, double startTime)
	{
		_audioSource.clip = audioClip;
		_audioSource.pitch = pitch;
		_audioSource.PlayScheduled(startTime);
	}

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}
}
