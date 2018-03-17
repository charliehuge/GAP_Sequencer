using UnityEngine;

public class Sampler : MonoBehaviour
{
	// Helper to convert MIDI note number to pitch scalar
	public static float MidiNoteToPitch(int midiNote)
	{
		int semitoneOffset = midiNote - 60; // offset from MIDI note C4
		return Mathf.Pow(2.0f, semitoneOffset / 12.0f);
	}

	[SerializeField] private Ticker _ticker;
	[SerializeField] private AudioClip _audioClip;
	[SerializeField, Range(1, 8)] private int _numVoices = 2;
	[SerializeField] private SamplerVoice _samplerVoicePrefab;

	private SamplerVoice[] _samplerVoices;
	private int _nextVoiceIndex;

	private void Awake()
	{
		_samplerVoices = new SamplerVoice[_numVoices];

		for (int i = 0; i < _numVoices; ++i)
		{
			SamplerVoice samplerVoice = Instantiate(_samplerVoicePrefab);
			samplerVoice.transform.parent = transform;
			samplerVoice.transform.localPosition = Vector3.zero;
			_samplerVoices[i] = samplerVoice;
		}
	}

	private void OnEnable()
	{
		if (_ticker != null)
		{
			_ticker.Ticked += HandleTicked;
		}
	}

	private void OnDisable()
	{
		if (_ticker != null)
		{
			_ticker.Ticked -= HandleTicked;
		}
	}

	private void HandleTicked(double tickTime, int midiNoteNumber, float volume)
	{
		float pitch = MidiNoteToPitch(midiNoteNumber);
		_samplerVoices[_nextVoiceIndex].Play(_audioClip, pitch, tickTime, volume);

		_nextVoiceIndex = (_nextVoiceIndex + 1) % _samplerVoices.Length;
	}
}
