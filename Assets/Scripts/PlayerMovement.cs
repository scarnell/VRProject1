using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	GameObject[] enemies;
	bool isMoving = false;
	public float movementSpeed;
	Vector3 newLocation;
	float yStart;
	GameObject checkPoint;

	Autowalk aw;

	// Use this for initialization
	void Start () {
		yStart = transform.position.y;
		checkPoint = GameObject.Find("Checkpoint1");
		aw = GetComponent<Autowalk> ();
		aw.enabled = false;
	
	}

	// Update is called once per frame
	void Update () {
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		/*SetIsMoving ();

		if (isMoving) {
			newLocation = checkPoint.transform.position;
			newLocation.y = yStart;
			transform.position = Vector3.MoveTowards(transform.position, checkPoint.transform.position, movementSpeed*Time.deltaTime);
		}
		*/

		if (enemies.Length == 0) {
			aw.enabled = true;
		} else {
			aw.enabled = false;
		}

		
	}

	void SetIsMoving(){
		if (enemies.Length == 0 && (Cardboard.SDK.Triggered | Input.GetButton ("Fire1"))) {
			isMoving = !isMoving;
		}
	}


}
