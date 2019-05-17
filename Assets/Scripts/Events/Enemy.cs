using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class Enemy : MonoBehaviour, IHealth
{
    public Transform target;
    [ProgressBar("Health", 100, ProgressBarColor.Red)]
    public int health = 100;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }

    public void TakeDamage(int damage)
    {
        health -= health;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Heal(int heal)
    {
        health += heal;
    }
}
