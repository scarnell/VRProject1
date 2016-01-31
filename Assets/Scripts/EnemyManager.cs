using UnityEngine;
using CompleteProject;

public class EnemyManager : MonoBehaviour
{
	public GameObject[] spawnedEnemies;
    
    public GameObject enemy;
    public float spawnTime = 5f;
	public float spawnTimeMid = 5f;
    public Transform[] spawnPoints;

	int spawnCount = 0;


    void Start ()
    {
        Invoke ("Spawn", 3);

    }

	void Update(){
	}

    void Spawn ()
    {
		if (ScoreManager.score < ScoreManager.endScore) {
			int spawnPointIndex = Random.Range (0, spawnPoints.Length);
			spawnTime = (float)Random.Range (10, 12);

			GameObject instance = (GameObject)Instantiate (enemy, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);


			spawnCount++;

			Invoke ("Spawn", spawnTime);
		}


    }
}
