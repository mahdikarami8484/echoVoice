using UnityEngine;
using System.Collections;

public class Voice : MonoBehaviour
{
	private int sampleWindow = 64;
	private AudioClip microphoneClip;
	private AudioSource audio;

	public int maxTimeRecord = 10;

	void Start()
	{
		MicrophoneToAudioClip ();
		audio = GetComponent<AudioSource> ();
	}

	void Update()
	{
		/*
		if(microphoneClip != null)
		{
			print(GetLoudnessFromMicrophone ());
		}
		*/
	}
		
	IEnumerator WaitForRecording()
	{
		// Wait for the maximum recording length
		yield return new WaitForSeconds(maxTimeRecord);
		audio.clip = microphoneClip;
		audio.Play ();
		print ("Playing..");
		GetComponent<SpriteRenderer> ().color = Color.red;
		yield return new WaitForSeconds(microphoneClip.length);
		MicrophoneToAudioClip();
	}

	public void MicrophoneToAudioClip()
	{
		print ("Listening..");
		GetComponent<SpriteRenderer> ().color = Color.green;
		// Get the first microphone in device list
		string microphoneName = Microphone.devices [0];
		microphoneClip = Microphone.Start (microphoneName, false, maxTimeRecord, AudioSettings.outputSampleRate);
		StartCoroutine (WaitForRecording ());
	}



	public float GetLoudnessFromMicrophone()
	{
		return GetLoudnessFromAudioClip (Microphone.GetPosition(Microphone.devices[0]), microphoneClip);
	}

	public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip)
	{
		if (clip == null)
			return 0;
		
		int startPosition = clipPosition - sampleWindow;

		if (startPosition < 0)
			return 0;

		float[] waveData = new float[sampleWindow];
		try{
			clip.GetData (waveData, startPosition);
		}catch{
		}
		float totalLoudness = 0;

		for (int i = 0; i < sampleWindow; i++) 
		{
			totalLoudness += Mathf.Abs (waveData [i]);
		}

		return totalLoudness / sampleWindow;
	}

}
