using System.Collections;

using UnityEngine;

public class AttackDetect : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("adsdfasd");
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("adasd");
        EnemyHealth health = collision.gameObject.GetComponent<EnemyHealth>();
        health.currentHealth--;
        if (health.currentHealth <= 0)
            Destroy(collision.gameObject);
    }
}
