using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public AudioSource source;
	public Vector3 minScale;
	public Vector3 maxScale;
	public Voice voice;

	public float loudnessSensibility = 100f;
	public float threshold = 0.1f;

	// Update is called once per frame
	void Update () {
		float loudness = voice.GetLoudnessFromMicrophone () * loudnessSensibility;

		if (loudness < threshold)
			loudness = 0f;

		transform.localScale = Vector3.Lerp (minScale, maxScale, loudness);
		
	}
}
