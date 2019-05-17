using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Projectiles;

public class Gun : Weapon
{
    public int maxReserve = 500, maxClip = 20;
    public float spread = 2f, recoil = 1f, range = 10f;
    [BoxGroup("References")] public Transform shotOrigin;
    [BoxGroup("References")] public GameObject projectilePrefab;

    private int currentReserve = 0, currentClip = 0;

    private CameraLook camLook;

    public void Awake()
    {
        camLook = FindObjectOfType<CameraLook>();
    }

    public void Reload()
    {
        // Task:
        // - Ammo Math
        // - Auto-Reload when firing
        if (currentReserve > 0)
        {
            if (currentClip >= 0)
            {
                currentReserve += currentClip;
                currentClip = 0;

                if (currentReserve >= maxClip)
                {
                    currentReserve -= maxClip - currentClip;

                    currentClip = maxClip;
                }
                else if (currentReserve < maxClip)
                {
                    currentClip = currentReserve;
                    currentReserve -= currentReserve;
                }
            }
        }
    }

    public override void Attack()
    {
        // Reset timer & canShoot to false
        currentClip--;
        // Auto-Reload
        if (currentClip == 0)
        {
            Reload();
        }
        // Get some values
        Camera attachedCamera = Camera.main; // Note (Manny): Pass the reference into weapon somehow
        Transform camTransform = attachedCamera.transform; // Shortening Camera's Transform to 'camTransform'
        Vector3 lineOrigin = shotOrigin.position; // Where the bullet line starts
        Vector3 direction = camTransform.forward; // Forward direction of camera
                                                  // Spawn Bullet
        GameObject clone = Instantiate(projectilePrefab, camTransform.position, camTransform.rotation);
        Projectile projectile = clone.GetComponent<Projectile>();
        projectile.damage += damage;
        projectile.Fire(lineOrigin, direction);

        // Apply Weapon Recoil
        Vector3 euler = Vector3.up * 2f;
        // Randomize the pitch
        euler.x = Random.Range(-1f, 1f);
        // Apply offset to camera using weapon recoil
        camLook.SetTargetOffset(euler * recoil);

        // Call Weapon's attack
        base.Attack();
    }
}
