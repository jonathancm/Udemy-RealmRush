using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class WaveDisplay : MonoBehaviour
{
	// Cached References
	EnemySpawner enemySpawner;
	Text waveText;

	void Start()
	{
		enemySpawner = FindObjectOfType<EnemySpawner>();
		waveText = GetComponent<Text>();

		UpdateDisplay();
	}

	void Update()
	{
		UpdateDisplay();
	}

	private void UpdateDisplay()
	{
		if(enemySpawner)
			waveText.text = enemySpawner.GetEnemyCount().ToString();
		else
			waveText.text = "0";
	}
}