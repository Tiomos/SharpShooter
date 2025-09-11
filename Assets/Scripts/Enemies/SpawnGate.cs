using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnGate : MonoBehaviour
{
    [SerializeField] GameObject robotPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float spawnInterval = 5f;

    PlayerHealth player;

    void Awake()
    {
        player = FindFirstObjectByType<PlayerHealth>();
    }

    void Start()
    {
        StartCoroutine(spawnRobot());
    }

    IEnumerator spawnRobot()
    {
        while (player)
        {
            Instantiate(robotPrefab, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
