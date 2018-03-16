using UnityEngine;

public class MusicController : MonoBehaviour 
{
	// game-modifiable fields
	public float playerDistance;
	public bool playerSpotted;

	// references to things we want to control
	[SerializeField] private Clock _clock;
	[SerializeField] private KeyController _keyController;
	[SerializeField] private Sequencer _excitedSequence;

	// mapping settings
	[SerializeField] private float _playerDistanceMin = 1.0f;
	[SerializeField] private float _playerDistanceMax = 10.0f;
	[SerializeField] private float _tempoMin = 60.0f;
	[SerializeField] private float _tempoMax = 130.0f;
	[SerializeField] private KeyController.ScaleMode _safeScaleMode;
	[SerializeField] private KeyController.ScaleMode _spottedScaleMode;

	private bool _lastPlayerSpotted;

	private void Update()
	{
		// map the player distance to tempo
		// we'll assume the settings are reasonable,
		// but in the wild you really should clean the input
		float amount = (playerDistance - _playerDistanceMin) 
			/ (_playerDistanceMax - _playerDistanceMin);
		float tempo = Mathf.Lerp(_tempoMax, _tempoMin, amount);
		_clock.SetTempo(tempo);

		// if the player's "spotted" state changed...
		if (playerSpotted != _lastPlayerSpotted)
		{
			// ... update the key
			_keyController.Mode = playerSpotted 
				? _spottedScaleMode 
				: _safeScaleMode;

			// ...also toggle the "excited" sequence
			_excitedSequence.suspended = !playerSpotted;

			_lastPlayerSpotted = playerSpotted;
		}
	}
}
