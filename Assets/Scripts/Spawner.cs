using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private Transform _spawnPosition;

    public void Spawn()
    {
        Instantiate(_objectToSpawn, _spawnPosition.position, Quaternion.identity);
    }
}
