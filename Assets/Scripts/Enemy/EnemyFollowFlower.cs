using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowFlower : MonoBehaviour
{
    //enemy stats
    public float speed;
    public float attackRange;
    public int damage;
    public float attackCooldown = 1f;
    private float nextFireTime;
    //reference the flower
    private Transform flower;
    // Start is called before the first frame update
    void Start()
    {
        flower = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //check distance of flower
        float distanceFromPlayer = Vector2.Distance(flower.position, transform.position);
        //to move towards the flower until within attack range.
        if (distanceFromPlayer > attackRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, flower.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= attackRange && nextFireTime < Time.time)
        {
            //attack once in range, then wait for attack cooldown
            GameManager.instance.AddHealth(damage);
            nextFireTime = Time.time + attackCooldown;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
