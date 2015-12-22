using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float moveSpeed = 5f;
    private PlayerController controller;
    Camera viewCamera;

	// Use this for initialization
	void Start () {
        controller = GetComponent<PlayerController>();
        viewCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;
        if(groundPlane.Raycast(ray, out rayDistance)) {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            controller.LookAt(point);
        }
    }
}
