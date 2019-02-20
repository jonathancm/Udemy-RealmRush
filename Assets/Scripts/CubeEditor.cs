using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class CubeEditor : MonoBehaviour
{
	// Configurable Parameters
	[Range(1f, 20f)][SerializeField] float gridSize = 10f;

	TextMesh textMesh;

	void Update()
	{
		Vector3 snapPosition;
		snapPosition.x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize;
		snapPosition.y = Mathf.RoundToInt(transform.position.y / gridSize) * gridSize;
		snapPosition.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;

		transform.position = snapPosition;

		textMesh = GetComponentInChildren<TextMesh>();
		textMesh.text = (snapPosition.x/gridSize) + "," + (snapPosition.z/gridSize);
	}
}
