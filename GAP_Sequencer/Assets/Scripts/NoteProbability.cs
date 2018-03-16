using UnityEngine;

public class NoteProbability : Ticker 
{
	[Range(0.0f, 1.0f)] public float probability;

	// The "Ticker" we want to listen to (a Clock or another Sequencer)
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

	public void HandleTicked(double tickTime, int midiNoteNumber)
	{
		// roll the dice to see if we should play this note
		float rand = UnityEngine.Random.value;
		if (rand < probability)
		{
			DoTick(tickTime, midiNoteNumber);
		}
	}
}
