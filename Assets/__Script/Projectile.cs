using UnityEngine;
using System.Collections;
using System;

public class Projectile : MonoBehaviour {
    public LayerMask collisionMask;
    private float damage = 1f;
    private float speed = 10f;
    private float skinWidth = .5f;

    void Start() {
        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, 1f, collisionMask);
        if (initialCollisions.Length > 0) {
            OnHitObject(initialCollisions[0], transform.position);
        }
    }

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
        if(Physics.Raycast(ray, out hit, moveDistance + skinWidth, collisionMask, QueryTriggerInteraction.Collide)) {
            OnHitObject(hit.collider, hit.point);
        }
    }

    private void OnHitObject(Collider c, Vector3 hitPoint) {
        //Debug.Log(hit.collider.gameObject.name);
        IDamageable damageableObj = c.GetComponent<IDamageable>();
        damageableObj.TakeHit(damage, hitPoint, transform.forward);
        GameObject.Destroy(gameObject);
    }
}
