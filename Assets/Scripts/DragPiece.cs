using UnityEngine;
using System.Collections;

public class DragPiece : MonoBehaviour
{

	private bool dragging = false;
	private float distance;

	void OnMouseDown ()
	{
		distance = Vector3.Distance (transform.position, Camera.main.transform.position);
		dragging = true;
	}

	void OnMouseUp ()
	{
		dragging = false;
	}

	void Update ()
	{
		if (dragging) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			Vector3 rayPoint = ray.GetPoint (distance);
			transform.position = rayPoint;
		}
	}
}
