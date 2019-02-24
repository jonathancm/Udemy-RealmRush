using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
	// Cached References
	Waypoint waypoint;

	private void Awake()
	{
		waypoint = GetComponent<Waypoint>();
	}

	void Update()
	{
		SnapToGrid();
		UpdateLabel();
	}

	private void SnapToGrid()
	{
		int gridSize = waypoint.GetGridSize();
		transform.position = new Vector3(waypoint.GetGridPos().x, 0f, waypoint.GetGridPos().y) * gridSize;
	}

	private void UpdateLabel()
	{
		TextMesh textMesh = GetComponentInChildren<TextMesh>();

		string labelName = (waypoint.GetGridPos().x) + "," + (waypoint.GetGridPos().y);
		textMesh.text = labelName;
		//gameObject.name = "Cube " + textMesh.text;
	}
}
