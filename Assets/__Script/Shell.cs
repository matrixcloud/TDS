using UnityEngine;
using System.Collections;

public class Shell : MonoBehaviour {
    public Rigidbody mRigidbody;
    public float minForce;
    public float maxForce;
    private float lifeTime = 4f;
    private float fadeTime = 2f;

	// Use this for initialization
	void Start () {
	    float force = Random.Range(minForce, maxForce);
        mRigidbody.AddForce(transform.forward * force);
        mRigidbody.AddTorque(Random.insideUnitSphere * force);
	    StartCoroutine(Fade());
	}

    IEnumerator Fade() {
        yield return new WaitForSeconds(lifeTime);

        float percent = 0;
        float fadeSpeed = 1/fadeTime;
        Material mat = GetComponent<Renderer>().material;
        Color initialColor = mat.color;

        while (percent < 1) {
            percent += Time.deltaTime*fadeSpeed;
            mat.color = Color.Lerp(initialColor, Color.clear, percent);
            yield return null;
        }
        Destroy(gameObject);
    }
}
