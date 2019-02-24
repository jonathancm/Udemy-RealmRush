using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	[SerializeField] int attackDamage = 1;
	//[SerializeField] float waitPeriod = 1f;
	[SerializeField] float movementSpeed = 1f;
	[SerializeField] GameObject goalParticles = null;

	// Cached References
	List<Waypoint> path;
	PlayerBase playerBase;

	// State Variable
	int waypointIndex = 0;
	Waypoint currentWaypoint = null;
	bool isEndReached = false;

	void Start()
	{
		playerBase = FindObjectOfType<PlayerBase>();

		Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
		path = pathfinder.GetPath();
		currentWaypoint = path[waypointIndex];
	}

	//private IEnumerator FollowPathDiscrete(List<Waypoint> path)
	//{
	//	foreach(Waypoint waypoint in path)
	//	{
	//		transform.position = waypoint.transform.position;
	//		yield return new WaitForSeconds(waitPeriod);
	//	}
	//}

	private void Update()
	{
		FollowPathAnalog();
	}

	private void FollowPathAnalog()
	{
		if(isEndReached)
		{
			ExplodeEnemy();
		}
		else
		{
			SelectNextGoal();
			MoveAnalog();
		}
	}

	private void SelectNextGoal()
	{
		Vector3 currentPos;
		Vector3 goalPos;
		float distToWaypoint;

		currentPos = transform.position;
		goalPos = currentWaypoint.transform.position;

		distToWaypoint = Vector3.Distance(goalPos, currentPos);
		if(distToWaypoint <= Mathf.Epsilon)
		{
			waypointIndex++;
			if(waypointIndex < path.Count)
			{
				currentWaypoint = path[waypointIndex];
			}
			else
			{
				isEndReached = true;
			}
		}
	}

	private void MoveAnalog()
	{
		Vector3 currentPos = transform.position;
		Vector3 goalPos = currentWaypoint.transform.position;

		currentPos = Vector3.MoveTowards(currentPos, goalPos, movementSpeed * Time.deltaTime);
		transform.position = currentPos;
	}

	private void ExplodeEnemy()
	{
		playerBase.TakeDamage(attackDamage);

		GameObject vfxInstance = Instantiate(goalParticles, gameObject.transform.position, Quaternion.identity);
		ParticleSystem particles = vfxInstance.GetComponent<ParticleSystem>();
		Destroy(vfxInstance, particles.main.duration);

		Destroy(gameObject);
	}
}
