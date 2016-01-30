using UnityEngine;
using System.Collections;

public class OutlineEnemy: MonoBehaviour {

	GameObject Test_Obj;
	RaycastHit hit;



	void Update()
	{
		var fwd = transform.TransformDirection(Vector3.forward); 
		Ray temp = new Ray (transform.position, fwd);
		if (Physics.Raycast(temp, out hit))
		{
			if(hit.collider.gameObject.tag == "Enemy") 
			{
				


				hit.transform.GetComponent<EnemyMovement> ().isHighlighted = true;
				//SkinnedMeshRenderer temp2 = hit.collider.gameObject.GetComponentInChildren<SkinnedMeshRenderer> ();
				//temp2.material = highlightMat;

			}

			else
			{
				GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
				for (int i = 0; i < enemies.Length; i++) {
					enemies [i].GetComponentInChildren<EnemyMovement> ().isHighlighted = false;
				}
				//hit.collider.gameObject.renderer.material.shader = Shader.Find("Diffuse");
			}
		}
	}


}
