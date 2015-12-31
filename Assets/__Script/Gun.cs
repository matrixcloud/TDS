using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
    public enum FireMode { Auto,Burst,Single}
    public FireMode fireMode;
    public Transform[] muzzles;
    public Projectile projectile;
    public float msBetweenShots = 100f;
    public float muzzleVelocity = 35f;
    public Transform shell;
    public Transform shellEjection;
    public int burstCount;
    private float nextShotTime;
    private MuzzleFlash muzzleFlash;
    private bool triggerReleasedSinceLastShot;
    private int shotsRemaingInBurst;

    void Start() {
        muzzleFlash = GetComponent<MuzzleFlash>();
        shotsRemaingInBurst = burstCount;
    }

    private void Shoot() {
        if(Time.time > nextShotTime) {

            if(fireMode == FireMode.Burst) {
                if(shotsRemaingInBurst == 0) {
                    return;
                }
                shotsRemaingInBurst--;
            }else if(fireMode == FireMode.Single) {
                if (!triggerReleasedSinceLastShot) {
                    return;
                }
            }


            for (int i = 0; i < muzzles.Length; i++) {
                nextShotTime = Time.time + msBetweenShots / 1000f;
                Projectile newProjectile = Instantiate(projectile, muzzles[i].position, muzzles[i].rotation) as Projectile;
                newProjectile.SetSpeed(muzzleVelocity);
            }
            Instantiate(shell, shellEjection.position, shellEjection.rotation);
            muzzleFlash.Activate();
        }
    }

    public void OnTriggerHold() {
        Shoot();
        triggerReleasedSinceLastShot = false;
    }

    public void OnTriggerRelease() {
        triggerReleasedSinceLastShot = true;
        shotsRemaingInBurst = burstCount;
    }
}
