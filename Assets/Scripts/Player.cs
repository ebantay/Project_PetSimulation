using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float moveSpeed = 5.0f;
	public float drag = 0.5f;
	public float terminalRotationSpeed = 25.0f;
	public VirtualJoystick moveJoystick;

	private Rigidbody controller;
	private Transform camTrasform;

	// Use this for initialization
	void Start () {
		controller = GetComponent<Rigidbody>();
		controller.maxAngularVelocity = terminalRotationSpeed;
		controller.drag = drag;

		camTrasform = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 dir = Vector3.zero;

		dir.x = Input.GetAxis ("Horizontal");
		dir.y = Input.GetAxis ("Vertical");

		if (dir.magnitude > 1)
			dir.Normalize ();

		if (moveJoystick.InputDirection != Vector3.zero)
		{
			dir = moveJoystick.InputDirection;
		}

		//Rotation our direction vector with camera
		Vector3 rotatedDir = camTrasform.TransformDirection(dir);
		rotatedDir = new Vector3 (rotatedDir.x, 0, rotatedDir.z);
		rotatedDir = rotatedDir.normalized * dir.magnitude;

		controller.AddForce (rotatedDir * moveSpeed);
	}
}
