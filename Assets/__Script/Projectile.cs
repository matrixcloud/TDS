using UnityEngine;
using System.Collections;
using System;

public class Projectile : MonoBehaviour {
    public LayerMask collisionMask;
    private float speed = 10f;


    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);	
	}

    private void CheckCollisions(float moveDistance) {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide)) {
            OnHitObject(hit);
        }
    }

    private void OnHitObject(RaycastHit hit) {
        Debug.Log(hit.collider.gameObject.name);
        GameObject.Destroy(gameObject);
    }
}
