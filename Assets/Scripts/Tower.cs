using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Tower : MonoBehaviour
{
	// Configurable Parameters
	[SerializeField] Transform objectToPan = null;
	[SerializeField] ParticleSystem projectileParticle = null;
	[SerializeField] float attackRange = 20f;

	// Cached References
	EnemySpawner enemySpawner;
	Waypoint baseWaypoint;

	// State Variable
	Quaternion initialDirection;
	Transform targetEnemy;
	

	private void Start()
	{
		enemySpawner = FindObjectOfType<EnemySpawner>();
		initialDirection = objectToPan.transform.localRotation;
	}

	void Update()
    {
		SetTargetEnemy();
		AimTurretAtTarget();
		FireAtTarget();
    }

	private void SetTargetEnemy()
	{
		var sceneEnemies = FindObjectsOfType<Enemy>();
		if(sceneEnemies.Length == 0)
			return;

		Transform closestEnemy = sceneEnemies[0].transform;
		foreach(Enemy testEnemy in sceneEnemies)
		{
			closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
		}
		
		if(isTargetInRange(closestEnemy.position))
			targetEnemy = closestEnemy;
		else
			targetEnemy = null;
	}

	private Transform GetClosest(Transform enemyA, Transform enemyB)
	{
		float distToA = Vector3.Distance(enemyA.position, transform.position);
		float distToB = Vector3.Distance(enemyB.position, transform.position);

		if(distToA <= distToB)
			return enemyA;
		else
			return enemyB;
	}

	private bool isTargetInRange(Vector3 targetPosition)
	{
		float distanceToTarget;
		distanceToTarget = Vector3.Distance(targetPosition, transform.position);

		if(distanceToTarget <= attackRange)
			return true;
		else
			return false;
	}

	private void AimTurretAtTarget()
	{
		if(targetEnemy)
			objectToPan.LookAt(targetEnemy);
		else
			objectToPan.transform.localRotation = initialDirection;
	}

	private void FireAtTarget()
	{
		if(targetEnemy)
			Shoot(true);
		else
			Shoot(false);
	}

	private void Shoot(bool enableShooting)
	{
		var emission = projectileParticle.emission;
		emission.enabled = enableShooting;
	}

	public void SetBaseWaypoint(Waypoint waypoint)
	{
		baseWaypoint = waypoint;
	}

	public Waypoint GetBaseWaypoint()
	{
		return baseWaypoint;
	}
}
