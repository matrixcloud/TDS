using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    private float speed = 10f;

    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);	
	}
}
