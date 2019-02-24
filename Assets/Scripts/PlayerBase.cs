using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
	[SerializeField] int startingHitPoints = 10;

	// State variables
	int currentHitPoints = 1;

	private void Start()
	{
		currentHitPoints = startingHitPoints;
	}

	public int GetHitPoints()
	{
		return currentHitPoints;
	}

	public void TakeDamage(int damageAmount)
	{
		currentHitPoints -= damageAmount;
		if(currentHitPoints <= 0)
		{
			currentHitPoints = 0;
			// Lose Game
		}
		else
		{
			// PlayHitVFX();
		}
	}
}
