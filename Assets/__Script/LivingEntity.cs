using UnityEngine;
using System.Collections;
using System;

public class LivingEntity : MonoBehaviour, IDamageable {
    public float startingHealth;
    protected float health;
    protected bool dead = false;
    public event System.Action OnDeath;

    protected virtual void Start() {
        health = startingHealth;
    }

    public void TakeHit(float damage, RaycastHit hit) {
        health -= damage;
        if(health <= 0 && !dead) {
            Die();
        }
    }

    public void Die() {
        dead = true;
        if(OnDeath != null) {
            OnDeath();
        }
        Destroy(gameObject);
    }
}
