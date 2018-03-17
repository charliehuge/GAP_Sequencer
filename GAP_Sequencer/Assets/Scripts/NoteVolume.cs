using UnityEngine;

public class NoteVolume : Ticker 
{
	public enum Mode
	{
		Set,
		Multiply,
		Add
	}

	[Range(0.0f, 1.0f)] public float volume;
	public Mode mode;

	// The "Ticker" we want to listen to
	[SerializeField] private Ticker _ticker;

	/// Subscribe to the ticker
	private void OnEnable()
	{
		if (_ticker != null)
		{
			_ticker.Ticked += HandleTicked;
		}
	}

	/// Unsubscribe from the ticker
	private void OnDisable()
	{
		if (_ticker != null)
		{
			_ticker.Ticked -= HandleTicked;
		}
	}

	public void HandleTicked(double tickTime, int midiNoteNumber, float volume)
	{
		float newVolume = 0.0f;
		switch (mode)
		{
		case Mode.Set:
			newVolume = this.volume;
			break;
		case Mode.Multiply:
			newVolume = this.volume * volume;
			break;
		case Mode.Add:
			newVolume = this.volume + volume;
			break;
		}

		DoTick(tickTime, midiNoteNumber, newVolume);
	}
}
