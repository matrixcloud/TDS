using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
    public Transform muzzle;
    public Projectile projectile;
    public float msBetweenShots = 100f;
    public float muzzleVelocity = 35f;
    public Transform shell;
    public Transform shellEjection;
    private float nextShotTime;
    private MuzzleFlash muzzleFlash;

    void Start() {
        muzzleFlash = GetComponent<MuzzleFlash>();
    }

    public void Shoot() {
        if(Time.time > nextShotTime) {
            nextShotTime = Time.time + msBetweenShots / 1000f;
            Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;
            newProjectile.SetSpeed(muzzleVelocity);

            Instantiate(shell, shellEjection.position, shellEjection.rotation);
            muzzleFlash.Activate();
        }
    }
}
