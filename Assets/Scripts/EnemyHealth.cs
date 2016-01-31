using UnityEngine;

namespace CompleteProject
{
    public class EnemyHealth : MonoBehaviour
    {
        public int startingHealth;            // The amount of health the enemy starts the game with.
        public int currentHealth;                   // The current health the enemy has.
        public float sinkSpeed = 2.5f;              // The speed at which the enemy sinks through the floor when dead.
        public float scoreValue = 10;                 // The amount added to the player's score when the enemy dies.
        public AudioClip deathClip;                 // The sound to play when the enemy dies.


        Animator anim;                              // Reference to the animator.
		CardboardAudioSource enemyAudio;                     // Reference to the audio source.
        ParticleSystem hitParticles;                // Reference to the particle system that plays when the enemy is damaged.
        CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
        bool isDead;                                // Whether the enemy is dead.
        bool isSinking;                             // Whether the enemy has started sinking through the floor.

		float lastDistance = 10000000000;
		float currDistance;
		public float distanceStep;
		GameObject player;
		public bool allowEnemyGrowth = false;
		bool isGrowing = false;

        void Awake ()
        {
            // Setting up the references.
            anim = GetComponent <Animator> ();
			enemyAudio = GetComponent <CardboardAudioSource> ();
			hitParticles = gameObject.transform.FindChild("HitParticles").GetComponent <ParticleSystem> ();
            capsuleCollider = GetComponent <CapsuleCollider> ();

            // Setting the current health when the enemy first spawns.
            currentHealth = startingHealth;
			player = GameObject.FindGameObjectWithTag ("Player").gameObject;
			if (allowEnemyGrowth) {
				int temp = Random.Range (0, 2);
				if (temp == 0) {
					isGrowing = true;
					currentHealth = 28;
				} else {
					isGrowing = false;
				}

			}
        }


        void Update ()
        {
            // If the enemy should be sinking...
            if(isSinking)
            {
                // ... move the enemy down by the sinkSpeed per second.
                transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
            }

			currDistance = Vector3.Distance (transform.position, player.transform.position);
			if (currDistance < lastDistance) {
				if (isGrowing & !isSinking) {
					scoreValue += 0.02f;
					transform.localScale *= 1.0025f;
				}
				
				scoreValue += 0.01f;
				lastDistance = currDistance;
			}
        }


        public void TakeDamage (int amount, Vector3 hitPoint)
        {
			
            // If the enemy is dead...
            if(isDead)
                // ... no need to take damage so exit the function.
                return;

            // Play the hurt sound effect.
            enemyAudio.Play ();

            // Reduce the current health by the amount of damage sustained.
            currentHealth -= amount;
            
            // Set the position of the particle system to where the hit was sustained.
            hitParticles.transform.position = hitPoint;

            // And play the particles.
            hitParticles.Play();

            // If the current health is less than or equal to zero...
            if(currentHealth <= 0)
            {
                // ... the enemy is dead.
                Death ();
            }
        }


        void Death ()
        {
            // The enemy is dead.
            isDead = true;

            // Turn the collider into a trigger so shots can pass through it.
            capsuleCollider.isTrigger = true;

            // Tell the animator that the enemy is dead.
            anim.SetTrigger ("Dead");

            // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
            enemyAudio.clip = deathClip;
            enemyAudio.Play ();
        }


        public void StartSinking ()
        {
            // Find and disable the Nav Mesh Agent.
            GetComponent <NavMeshAgent> ().enabled = false;

            // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
            GetComponent <Rigidbody> ().isKinematic = true;

            // The enemy should no sink.
            isSinking = true;

            // Increase the score by the enemy's score value.
			ScoreManager.score += Mathf.RoundToInt(scoreValue);

            // After 2 seconds destory the enemy.
            Destroy (gameObject, 2f);
        }
    }
}