
using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private readonly float startDelay = 2f;
    private Vector3 spawnPos = new Vector3(-19, 0, -3f);

    public GameObject obstaclePrefab;
    private Coroutine spawnCoroutine;

    private void OnEnable()
    {
        PlayerController.OnGameOver += StopSpawning;
    }

    private void OnDisable()
    {
        PlayerController.OnGameOver -= StopSpawning;
    }

    void Start()
    {
        spawnCoroutine  = StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            SpawnObstacle();

            yield return new WaitForSeconds(Random.Range(2.5f, 5.5f));
        }
    }

    void SpawnObstacle()
    {
        Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
    }

    private void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }
}
