using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //variables to set spawn rate, and who spawns.
    [SerializeField] private float spawnRate = 5f;

    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private bool canSpawn = true;

    //varriables to make spawwner follow player to move with screen.
    public float minDistance;
    public float speed = 2f;
    public Transform target;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    void Update()
    {
        transform.position = new Vector3(target.position.x + 5, target.position.y + 5, 0);
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (true)
        {
            yield return wait;
            int rand = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[rand];

            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, minDistance);
    }
}
