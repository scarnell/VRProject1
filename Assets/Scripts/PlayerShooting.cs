using UnityEngine;
//using UnitySampleAssets.CrossPlatformInput;

namespace CompleteProject
{
    public class PlayerShooting : MonoBehaviour
    {
        public int damagePerShot = 20;                  // The damage inflicted by each bullet.
        public float timeBetweenBullets = 0.15f;        // The time between each shot.
        public float range = 100f;                      // The distance the gun can fire.
		CardboardAudioSource gunAudio;

        float timer;                                    // A timer to determine when to fire.
        Ray shootRay;                                   // A ray from the gun end forwards.
        RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
        int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
        /*ParticleSystem gunParticles;                    // Reference to the particle system.
                               // Reference to the line renderer.
                                   // Reference to the audio source.
        Light gunLight;                                 // Reference to the light component.
*/
		//public Light faceLight;								// Duh
        float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.


		GameObject Fireball;
		GameObject WandTip;
		//LineRenderer gunLine;    
		Animator anim;

		GameObject enemyInSight;



        void Awake ()
        {
            // Create a layer mask for the Shootable layer.
            shootableMask = LayerMask.GetMask ("Shootable");

            // Set up the references.
            /*gunParticles = GetComponentInChildren<ParticleSystem> ();
			gunLine = GetComponentInChildren<LineRenderer> ();
            
            gunLight = GetComponentInChildren<Light> (); */

			Fireball = gameObject.transform.FindChild("WandTip").FindChild("Fireball").gameObject;
			Fireball.SetActive (false);
			WandTip = gameObject.transform.FindChild("WandTip").gameObject;
			gunAudio = GetComponent<CardboardAudioSource> ();
			//faceLight = GetComponentInChildren<Light> ();

			anim = GetComponent<Animator> ();

			//gunLine = gameObject.transform.FindChild("WandTip").gameObject.GetComponent<LineRenderer>();
        }


        void Update ()
        {
            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

#if !MOBILE_INPUT
            // If the Fire1 button is being press and it's time to fire...
			if((Input.GetButton("Fire1") | Cardboard.SDK.CardboardTriggered)  && timer >= timeBetweenBullets && Time.timeScale != 0)
            {
                // ... shoot the gun.
				//UnityEngine.Debug.Log("Shot!");
                Shoot ();
            }
#else
            // If there is input on the shoot direction stick and it's time to fire...
            if ((CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0) && timer >= timeBetweenBullets)
            {
                // ... shoot the gun
                Shoot();
            }
#endif
            // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
            /*if(timer >= timeBetweenBullets * effectsDisplayTime)
            {
                // ... disable the effects.
                DisableEffects ();
            }*/
        }


        public void DisableEffects ()
        {
            // Disable the line renderer and the light.
            //gunLine.enabled = false;
			//faceLight.enabled = false;
            //gunLight.enabled = false;

			Fireball.SetActive (false);
        }


        void Shoot ()
        {

			anim.SetTrigger ("IsSpellCast");
            // Reset the timer.
            timer = 0f;


        }

		public void ShootFire(){

			Fireball.SetActive (false);
			Fireball.SetActive (true);

			gunAudio.Play ();

			// Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
			shootRay.origin = WandTip.transform.position;
			shootRay.direction = WandTip.transform.forward;

			//Debug.DrawRay (shootRay.origin, shootRay.direction, Color.green, 50f, false);


			// Perform the raycast against gameobjects on the shootable layer and if it hits something...
			if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
			{


				EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
				//Debug.Log (enemyHealth);

				// If the EnemyHealth component exist...
				if(enemyHealth != null){


					Debug.Log ("enemy is hit!");
					// ... the enemy should take damage.
					enemyHealth.TakeDamage (damagePerShot, shootHit.point);
				}
				// Set the second position of the line renderer to the point the raycast hit.
				//gunLine.SetPosition (1, shootHit.point);
			}else{
				Debug.Log ("no enemy hit!");
				// ... set the second position of the line renderer to the fullest extent of the gun's range.
				//gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
			}
		}

	
    }
}