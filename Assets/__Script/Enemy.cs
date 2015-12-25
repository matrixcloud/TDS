using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity {
    public enum State { Idle, Chasing, Attacking}
    private State currentState;
    private NavMeshAgent agent;
    private Transform target;
    private float attackDistanceThreshold = .5f;
    private float attackInterval = 1f;
    private float nextAttackTime;
    private float myCollisionRadius;
    private float targetCollisionRadius;
    private Material skinMaterial;
    private Color originColor;


	// Use this for initialization
	protected override void Start () {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
	    currentState = State.Chasing;
        target = GameObject.FindGameObjectWithTag("Player").transform;
	    myCollisionRadius = GetComponent<CapsuleCollider>().radius;
	    targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;
	    skinMaterial = GetComponent<Renderer>().material;
	    originColor = skinMaterial.color;

        StartCoroutine(UpdatePath());
    }

    void Update(){
        if (Time.time > nextAttackTime) {
            float sqrDistance2Target = (target.position - transform.position).sqrMagnitude;
            if (sqrDistance2Target < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2)) {
                nextAttackTime = Time.time + attackInterval;
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack() {
        agent.enabled = false;
        currentState = State.Attacking;

        Vector3 originaPos = transform.position;
        Vector3 dir2Target = (target.position - transform.position).normalized;
        Vector3 attackPos = target.position - dir2Target * myCollisionRadius;

        float percent = 0;
        float attackSpeed = 3f;
        skinMaterial.color = Color.red;

        while (percent <= 1) {
            percent += Time.deltaTime*attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent)*4;
            transform.position = Vector3.Lerp(originaPos, attackPos, interpolation);

            yield return null;
        }

        skinMaterial.color = originColor;
        agent.enabled = true;
        currentState = State.Chasing;
    }

    IEnumerator UpdatePath() {
        float refreshRate = .25f;

        while (target != null) {
            if (currentState == State.Chasing) {
                Vector3 dir2Target = (target.position - transform.position).normalized;
                Vector3 targetPos = target.position - dir2Target*(myCollisionRadius + targetCollisionRadius + attackDistanceThreshold/2);
                if (!dead) {
                    agent.SetDestination(targetPos);
                }
            }
            yield return new WaitForSeconds(refreshRate);
        }
        // TODO a bug
        //while (target != null && !dead && currentState == State.Chasing) {
        //    Vector3 targetPos = new Vector3(target.position.x, 0, target.position.z);
        //    agent.SetDestination(targetPos);
        //    yield return new WaitForSeconds(refreshRate);
        //}
    }
}
