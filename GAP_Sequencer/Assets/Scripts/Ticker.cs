using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A thing that sends out ticks.
/// </summary>
public abstract class Ticker : MonoBehaviour 
{
	public delegate void TickHandler(double tickTime, int midiNoteNumber, float volume);

	public event TickHandler Ticked;

	protected void DoTick(double tickTime, int midiNoteNumber = 60, float volume = 1.0f)
	{
		if (Ticked != null)
		{
			Ticked(tickTime, midiNoteNumber, volume);
		}
	}
}
