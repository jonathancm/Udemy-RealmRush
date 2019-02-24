using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Enemy : MonoBehaviour
{
	[SerializeField] int hitPoints = 10;
	[SerializeField] ParticleSystem hitParticles = null;
	[SerializeField] GameObject deathParticles = null;

	private void OnParticleCollision(GameObject other)
	{
		ProcessHit();
		if(hitPoints <= 0)
		{
			KillEnemy();
		}
	}

	private void ProcessHit()
	{
		hitPoints--;
		hitParticles.Play();
	}

	private void KillEnemy()
	{
		GameObject vfxInstance = Instantiate(deathParticles, gameObject.transform.position, Quaternion.identity);
		ParticleSystem particles = vfxInstance.GetComponent<ParticleSystem>();
		Destroy(vfxInstance, particles.main.duration);

		Destroy(gameObject);
	}
}
