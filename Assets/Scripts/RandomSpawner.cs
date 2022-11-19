using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject cubePrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-249, 250), 5, Random.Range(-249, 250));
            Instantiate(cubePrefab, randomSpawnPosition, Quaternion.identity);
        }
    }
}
