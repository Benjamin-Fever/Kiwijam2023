using System.Collections;

using UnityEngine;

public class AttackDetect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        EnemyHealth health = collision.gameObject.GetComponent<EnemyHealth>();
        health.currentHealth--;
        if (health.currentHealth <= 0)
            Destroy(collision.gameObject);
    }
}
