using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Projectiles;
public abstract class Weapon : MonoBehaviour
{
    [BoxGroup("Stats")] public int damage = 10;
    [BoxGroup("Stats")] public float attackRate = .2f;


    [HideInInspector] public bool canShoot = false;

    private float attackTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        // Count up shoot timer
        attackTimer += Time.deltaTime;
        // If shoot timer reaches shoot rate
        if (attackTimer >= 1f / attackRate)
        {
            // Can shoot!
            canShoot = true;
        }
    }

    public virtual void Attack()
    {
        //reset attack Timer
        attackTimer = 0f;
        // reset can shoot
        canShoot = false;
    }
}
