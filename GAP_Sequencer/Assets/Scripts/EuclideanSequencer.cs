using System.Collections.Generic;
using UnityEngine;

public class EuclideanSequencer : Ticker 
{
	[Range(1, 16)] public int steps = 16;
	[Range(1, 16)] public int triggers = 4;

	private int _currentStep;

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
		if (IsStepOn(_currentStep, steps, triggers))
		{
			DoTick(tickTime, midiNoteNumber, volume);
		}

		_currentStep = (_currentStep + 1) % steps;
	}

	private static bool IsStepOn(int step, int numSteps, int numTriggers)
	{
		return (step * numTriggers) % numSteps < numTriggers;
	}
}
