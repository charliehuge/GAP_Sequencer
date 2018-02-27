using System;
using UnityEngine;

public class KeyController : MonoBehaviour 
{
	public enum Note { C = 0, Db, D, Eb, E, F, Gb, G, Ab, A, Bb, B }
	public enum ScaleMode { Ionian = 0, Dorian, Phrygian, Lydian, Mixolydian, Aeolian, Locrian }

	public Note RootNote;
	public ScaleMode Mode;

	private static int[] INTERVALS = { 1, 2, 2, 1, 2, 2, 2 };

	/// <summary>
	/// Converts a scale tone and octave to a note in the current key
	/// </summary>
	/// <returns>MIDI note</returns>
	/// <param name="scaleTone">Scale tone, range (1, 7)</param>
	/// <param name="octave">Octave, range (-1, 10 or so)</param>
	public int GetMIDINote(int scaleTone, int octave)
	{
		// scaleTone is range (1,7) for readability
		// but range (0,6) is easier to work with
		scaleTone--;

		// wrap scale tone and shift octaves
		while (scaleTone < 0)
		{
			octave--;
			scaleTone += 7;
		}
		while (scaleTone >= 7)
		{
			octave++;
			scaleTone -= 7;
		}

		// C4 = middle C, so MIDI note 0 is C-1.
		// we don't want to go any lower than that
		octave = Mathf.Max(octave, -1);
		// shift to minimum of 0 for easy math
		octave++;

		// add semitones for each step through the scale,
		// using the interval key above
		int semitones = 0;
		while (scaleTone > 0)
		{
			int idx = (scaleTone + (int)Mode) % 7;
			semitones += INTERVALS[idx];
			scaleTone--;
		}
			
		return octave * 12 + semitones + (int)RootNote;
	}
}
