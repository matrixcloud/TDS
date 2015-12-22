using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    private Rigidbody mRigidbody;
    private Vector3 velocity;

	// Use this for initialization
	void Start () {
        mRigidbody = GetComponent<Rigidbody>();
	}

    public void Move(Vector3 velocity) {
        this.velocity = velocity;
    }

    void FixedUpdate() {
        mRigidbody.MovePosition(mRigidbody.position + velocity * Time.fixedDeltaTime);
    }

    public void LookAt(Vector3 point) {
        Vector3 heightCorrectedPt = new Vector3(point.x, transform.position.y, point.z);
        transform.LookAt(heightCorrectedPt);
    }
}
