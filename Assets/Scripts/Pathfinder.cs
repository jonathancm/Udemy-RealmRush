using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
	// Configurable Parameters
	[SerializeField] Waypoint startWaypoint = null;
	[SerializeField] Waypoint endWaypoint = null;

	// State Variables
	Dictionary<Vector2Int, Waypoint> waypointGrid = new Dictionary<Vector2Int, Waypoint>();
	Queue<Waypoint> searchQeue = new Queue<Waypoint>();
	bool isSearching = true;
	Waypoint searchCenter;
	List<Waypoint> path = new List<Waypoint>();

	Vector2Int[] directions = {
		Vector2Int.up,
		Vector2Int.right,
		Vector2Int.down,
		Vector2Int.left
	};

	private void Start()
	{
		GetPath();
	}

	public List<Waypoint> GetPath()
	{
		if(path.Count == 0)
		{
			CalculatePath();
		}

		return path;
	}

	private void CalculatePath()
	{
		LoadBlocks();
		//ColorStartAndEnd();
		Pathfind_BreadthFirstSearch();
		CreatePath();
	}

	private void LoadBlocks()
	{
		var waypoints = FindObjectsOfType<Waypoint>();
		foreach(Waypoint waypoint in waypoints)
		{
			var gridPos = waypoint.GetGridPos();

			if(waypointGrid.ContainsKey(gridPos))
			{
				Debug.LogWarning("Skipping overlapping block " + waypoint);
			}
			else
			{
				waypointGrid.Add(waypoint.GetGridPos(), waypoint);
			}
		}
	}

	//private void ColorStartAndEnd()
	//{
	//	if(startWaypoint)
	//		startWaypoint.SetTopColor(Color.green);
	//	else
	//		Debug.LogError("Pathfinder is missing Starting Waypoint");

	//	if(endWaypoint)
	//		endWaypoint.SetTopColor(Color.red);
	//	else
	//		Debug.LogError("Pathfinder is missing Ending Waypoint");
	//}

	private void Pathfind_BreadthFirstSearch()
	{
		searchQeue.Enqueue(startWaypoint);

		while(searchQeue.Count > 0 && isSearching)
		{
			searchCenter = searchQeue.Dequeue();
			searchCenter.isExplored = true;

			HaltIfEndFound();
			ExploreNeighbours();
		}
	}

	private void HaltIfEndFound()
	{
		if(searchCenter == endWaypoint)
		{
			isSearching = false;
		}
	}

	private void ExploreNeighbours()
	{
		if(!isSearching)
			return;

		foreach(Vector2Int direction in directions)
		{
			Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;
			if(waypointGrid.ContainsKey(neighbourCoordinates))
			{
				QueueNewNeighbour(neighbourCoordinates);
			}
		}
	}

	private void QueueNewNeighbour(Vector2Int neighbourCoordinates)
	{
		Waypoint neighbour = waypointGrid[neighbourCoordinates];

		if(neighbour.isExplored || searchQeue.Contains(neighbour))
		{
			// Do nothing
		}
		else
		{
			neighbour.exploredFrom = searchCenter;
			searchQeue.Enqueue(neighbour);
		}
	}

	private void CreatePath()
	{
		Waypoint previous;

		if(endWaypoint)
			SetAsPath(endWaypoint);
		else
			Debug.LogError("Pathfinder is missing Ending Waypoint");

		previous = endWaypoint.exploredFrom;
		while(previous != startWaypoint)
		{
			SetAsPath(previous);
			previous = previous.exploredFrom;
		}
		SetAsPath(startWaypoint);
		path.Reverse();
	}

	private void SetAsPath(Waypoint waypoint)
	{
		path.Add(waypoint);
		waypoint.isPlaceable = false;
	}
}
