using UnityEngine;
using System.Collections;

public class PlayHermioneAndRon : MonoBehaviour {

	public AudioClip[] sayings;
	CardboardAudioSource source;
	int  randomNumberClip; 
	float randomNumberTimer;

	// Use this for initialization
	void Start () {
		source = GetComponent<CardboardAudioSource> ();
		InvokeRepeating ("setAudioClip", 15f, randomNumberTimer);
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void setAudioClip(){
		randomNumberTimer = (float) Random.Range (15, 30);
		randomNumberClip = Random.Range (0, sayings.Length);

		source.clip = sayings [randomNumberClip];
		source.Play ();
	}
}
