﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour {
    private NavMeshAgent agent;
    private Transform target;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(UpdatePath());
    }
	
    IEnumerator UpdatePath() {
        float refreshRate = .25f;

        while(target != null) {
            agent.SetDestination(target.position);
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
