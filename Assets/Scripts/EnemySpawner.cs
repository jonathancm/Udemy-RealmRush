using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	// Configurable Parameters
	[SerializeField] int enemiesToSpawn = 5;
	[SerializeField] float secondsBetweenSpawns = 3f;
	[SerializeField] Enemy enemyPrefab = null;

	// State Variables
	int enemyCount = 0;

    void Start()
    {
		if(enemyPrefab)
			StartCoroutine(SpawnEnemies());
		else
			Debug.LogError("Enemy Spawner is missing Enemy Prefab");
    }

	private IEnumerator SpawnEnemies()
	{
		while(enemiesToSpawn >= 1)
		{
			yield return new WaitForSeconds(secondsBetweenSpawns);
			var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
			enemy.transform.SetParent(gameObject.transform);
			enemiesToSpawn--;

			enemyCount++;
		}
	}

	public int GetEnemyCount()
	{
		return enemyCount;
	}
}
