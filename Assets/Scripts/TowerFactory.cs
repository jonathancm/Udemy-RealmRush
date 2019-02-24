using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
	// Configurable Parameters
	[SerializeField] Tower towerPrefab = null;
	[SerializeField] int towerPoolSize = 5;

	// State Variables
	Queue<Tower> towerQueue = new Queue<Tower>();

	public void PlaceTower(Waypoint waypoint)
	{
		if(towerPoolSize > 0)
		{
			InstantiateTower(waypoint);
		}
		else
		{
			MoveTower(waypoint);
		}
	}

	private void InstantiateTower(Waypoint baseWaypoint)
	{
		Tower newTower = null;
		Vector3 waypointPos = baseWaypoint.transform.position;

		// Create Tower
		newTower = Instantiate(towerPrefab, waypointPos, Quaternion.identity) as Tower;
		newTower.transform.SetParent(transform);

		// Update Base Waypoint
		newTower.SetBaseWaypoint(baseWaypoint);
		baseWaypoint.isPlaceable = false;

		// Update Queue
		towerPoolSize--;
		towerQueue.Enqueue(newTower);
	}

	private void MoveTower(Waypoint newBaseWaypoint)
	{
		Tower oldTower = towerQueue.Dequeue();

		// Free Previous Base Waypoint
		oldTower.GetBaseWaypoint().isPlaceable = true;
		newBaseWaypoint.isPlaceable = false;

		// Update Base Waypoint
		oldTower.transform.position = newBaseWaypoint.transform.position;
		oldTower.SetBaseWaypoint(newBaseWaypoint);

		towerQueue.Enqueue(oldTower);
	}
}
