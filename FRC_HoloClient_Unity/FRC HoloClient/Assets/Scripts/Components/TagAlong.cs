using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagAlong : MonoBehaviour {

	public bool TagAlongEnabled = true;
	public float HeightPosition = 0;
	public float PositionOffset = 1.5f;
	private Transform CameraTransform = null;

	// Use this for initialization
	void Start () {
		CameraTransform = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (TagAlongEnabled)
		{
			float angle = CameraTransform.rotation.eulerAngles.y * (Mathf.PI / 180);

			Vector3 newPos = CameraTransform.position + new Vector3((PositionOffset * Mathf.Sin(angle)), HeightPosition, (PositionOffset * Mathf.Cos(angle)));

			gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, CameraTransform.rotation.eulerAngles.y, 0));
			gameObject.transform.position = newPos;
		}
	}

	private void OnDrawGizmos()
	{
		// Draw a yellow sphere at the transform's position
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, 0.1f);
	}
}
