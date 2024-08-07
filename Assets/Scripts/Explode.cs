using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    private CubeSpawner _spawnerCubes;
    private float _upwardsModifier = 1f;

    private void Awake()
    {
        _spawnerCubes = GetComponent<CubeSpawner>();
    }

    private void OnEnable()
    {
        _spawnerCubes.CubeSpawned += ExplodeSpawnedCubes;
        _spawnerCubes.CubeDestroyed += ExplodeDestroyedCube;
    }

    private void OnDisable()
    {
        _spawnerCubes.CubeSpawned -= ExplodeSpawnedCubes;
        _spawnerCubes.CubeDestroyed -= ExplodeDestroyedCube;
    }

    private void ExplodeSpawnedCubes(List<Rigidbody> spawnedCubes, Transform mainCube)
    {
        float explosionForce = 10f;
        float explosionRadius = 10f;

        foreach (Rigidbody explodableCube in spawnedCubes)
        {
            explodableCube.AddExplosionForce(explosionForce, mainCube.transform.position, explosionRadius, _upwardsModifier, ForceMode.Impulse);
        }
    }

    private void ExplodeDestroyedCube(Cube mainCube)
    {
        float radiusExplousion = 5f * mainCube.MultiplierExplosion;
        float explosionForce = 10f * mainCube.MultiplierExplosion;

        foreach (Rigidbody cube in GetCubesForExplode(mainCube.transform,radiusExplousion))
        {
            cube.AddExplosionForce(explosionForce, mainCube.transform.position, radiusExplousion, _upwardsModifier, ForceMode.Impulse);
        }
    }

    private List<Rigidbody> GetCubesForExplode(Transform mainCube, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(mainCube.position, radius);

        List<Rigidbody> cubesRigidbodyes = new();

        if (hitColliders.Length > 0)
        {
            foreach (Collider hitCube in hitColliders)
            {
                Rigidbody cubeRigidbody = hitCube.attachedRigidbody;

                if (cubeRigidbody != null)
                {
                    cubesRigidbodyes.Add(cubeRigidbody);
                }
            }
        }

        return cubesRigidbodyes;
    }
}