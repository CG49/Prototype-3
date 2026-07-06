
using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private readonly float startDelay = 2f;
    private Vector3 spawnPos = new Vector3(-12, 0, -3);

    public GameObject obstaclePrefab;

    void Start()
    {
        StartCoroutine(SpawnLoop());
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
}
