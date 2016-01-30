using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	Transform player;               // Reference to the player's position.
	//PlayerHealth playerHealth;      // Reference to the player's health.
	//EnemyHealth enemyHealth;        // Reference to this enemy's health.
	NavMeshAgent nav;               // Reference to the nav mesh agent.


	SkinnedMeshRenderer rend;
	public Material highlightMat;
	Material orig;
	public bool isHighlighted;

	void Awake ()
	{
		// Set up the references.
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		//playerHealth = player.GetComponent <PlayerHealth> ();
		//enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent <NavMeshAgent> ();

		rend = GetComponentInChildren<SkinnedMeshRenderer> ();
		orig = GetComponentInChildren<SkinnedMeshRenderer> ().material;
	}


	void Update ()
	{
		// If the enemy and the player have health left...
		//enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0
		if(true)
		{
			// ... set the destination of the nav mesh agent to the player.
			nav.SetDestination (player.position);
		}
		// Otherwise...
		else
		{
			// ... disable the nav mesh agent.
			nav.enabled = false;
		}

		Pick ();


	}

	public void Pick()
	{
		if (isHighlighted) {
			rend.material = highlightMat;
		} else {
			rend.material = orig;
		}
	}

}