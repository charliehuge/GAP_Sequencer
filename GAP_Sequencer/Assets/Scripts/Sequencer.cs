using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A step sequencer
/// </summary>
public class Sequencer : Ticker
{
	/// <summary>
	/// Information about a step in the sequencer
	/// </summary>
	[Serializable]
	public class Step
	{
		public bool Active;
		[Range(1, 7)]
		public int ScaleTone;
		[Range(-1, 8)]
		public int Octave;
	}

	// The "Ticker" we want to listen to (a Clock or another Sequencer)
	[SerializeField] private Ticker _ticker;

	// The KeyController to use for translating scale tones to note numbers
	[SerializeField] private KeyController _keyController;

	// The list of steps in this Sequencer
	[SerializeField] private List<Step> _steps;

	// The current tick.
	private int _currentTick = 0;

	/// <summary>
	/// Subscribe to the ticker
	/// </summary>
	private void OnEnable()
	{
		if (_ticker != null)
		{
			_ticker.Ticked += HandleTicked;
		}
	}

	/// <summary>
	/// Unsubscribe from the ticker
	/// </summary>
	private void OnDisable()
	{
		if (_ticker != null)
		{
			_ticker.Ticked -= HandleTicked;
		}
	}

	/// <summary>
	/// Responds to tick events from the ticker.
	/// </summary>
	/// <param name="tickTime">Tick time.</param>
	/// <param name="midiNoteNumber">Midi note number.</param>
	public void HandleTicked(double tickTime, int midiNoteNumber)
	{
		int numSteps = _steps.Count;

		// if there are no steps, don't do anything
		if (numSteps == 0)
		{
			return;
		}

		Step step = _steps[_currentTick];

		// if the current step is active, send a tick through
		if (step.Active)
		{
			if (_keyController != null)
			{
				midiNoteNumber = _keyController.GetMIDINote(step.ScaleTone, 
															step.Octave);
			}
			DoTick(tickTime, midiNoteNumber);
		}

		// increment and wrap the tick counter
		_currentTick = (_currentTick + 1) % numSteps;
	}
}
