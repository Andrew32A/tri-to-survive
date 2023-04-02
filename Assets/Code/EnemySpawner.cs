using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject ChaserEnemyPrefab;
    public GameObject GunnerEnemyPrefab;
    public GameObject warningPrefab;
    
    public float chaserInterval;
    public float gunnerInterval;
    
    void Start()
    {
        StartCoroutine(SpawnEnemyWithWarning(chaserInterval, ChaserEnemyPrefab));
        StartCoroutine(SpawnEnemyWithWarning(gunnerInterval, GunnerEnemyPrefab));
    }
    
    private IEnumerator SpawnEnemyWithWarning(float interval, GameObject enemy)
    {
        // wait for enemy interval
        yield return new WaitForSeconds(interval);

        // randomize spawn position
        Vector3 spawnPosition = new Vector3(Random.Range(-5f, 0.5f), Random.Range(-6f, 6f), 0);
        
        // spawn warning sprite at the same position as the enemy
        GameObject warning = Instantiate(warningPrefab, spawnPosition, Quaternion.identity);
        
        yield return new WaitForSeconds(2f);
        
        // destroy warning sprite
        Destroy(warning);
        
        // spawn enemy
        GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
        
        // loop again
        StartCoroutine(SpawnEnemyWithWarning(interval, enemy));
    }
}
