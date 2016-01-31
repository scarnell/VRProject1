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
		InvokeRepeating ("setAudioClip", 10f, 10f);
	
	}
	
	// Update is called once per frame
	void Update () {
		//randomNumberTimer = (float) Random.Range (10, );
	}

	void setAudioClip(){
		
		randomNumberClip = Random.Range (0, sayings.Length);

		source.clip = sayings [randomNumberClip];
		source.Play ();
	}
}
