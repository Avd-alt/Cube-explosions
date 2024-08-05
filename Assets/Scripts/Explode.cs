using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    private CubeSpawner _spawnerCubes;

    private void Awake()
    {
        _spawnerCubes = GetComponent<CubeSpawner>();
    }

    private void OnEnable()
    {
        _spawnerCubes.CubeSpawned += ExplodeSpawnedCubes;
    }

    private void OnDisable()
    {
        _spawnerCubes.CubeSpawned -= ExplodeSpawnedCubes;
    }

    private void ExplodeSpawnedCubes(List<Rigidbody> spawnedCubes, Cube mainCube)
    {
        float upwardsModifier = 1f;
        float explosionForce = 10f;
        float explosionRadius = 10f;

        foreach (Rigidbody explodableCube in spawnedCubes)
        {
            explodableCube.AddExplosionForce(explosionForce, mainCube.transform.position, explosionRadius, upwardsModifier, ForceMode.Impulse);
        }
    }
}