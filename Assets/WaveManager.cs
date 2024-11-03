using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class WaveManager : NetworkBehaviour
{
    [SerializeField] int timeUntilFirstWave = 30;
    [SerializeField] GameObject spawnPoints;
    [SerializeField] GameObject enemyPrefab;

    private void WaveManager_OnGameStart(Character c)
    {

        StartCoroutine(DelayedSpawnEnemies());
        
    }

    private IEnumerator DelayedSpawnEnemies()
    {
        // Add a small delay to ensure player initialization is complete before spawning enemies
        yield return new WaitForSeconds(1f);

        SpawnEnemiesServerRpc();
        
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnEnemiesServerRpc()
    {
        for (int i = 0; i < 12; i++)
        {
            int r = Random.Range(0, spawnPoints.transform.childCount);
            GameObject enemy = Instantiate(enemyPrefab, spawnPoints.transform.GetChild(r).position, Quaternion.identity);
            enemy.GetComponent<NetworkObject>().Spawn(true);
        }

    }

    private void OnEnable()
    {
        Character.OnCharacterSpawn += WaveManager_OnGameStart;
    }

    private void OnDisable()
    {
        Character.OnCharacterSpawn -= WaveManager_OnGameStart;
    }
}
