using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject ChaserEnemyPrefab;
    public GameObject GunnerEnemyPrefab;

    public float chaserInterval;
    public float gunnerInterval;

    void Start() {
        StartCoroutine(spawnEnemy(chaserInterval, ChaserEnemyPrefab));
        StartCoroutine(spawnEnemy(gunnerInterval, GunnerEnemyPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy) {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 0.5f), Random.Range(-6f, 6f), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
