using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;

    private float _minCubesSpawn = 2f;
    private float _maxCubesSpawn = 6f;

    public event Action <List<Rigidbody>, Transform> CubeSpawned;
    public event Action<Cube> CubeDestroyed;

    private void OnEnable()
    {
        _cube.CubeClicked += TryCreateCube;
    }

    private void OnDisable()
    {
        _cube.CubeClicked -= TryCreateCube;
    }

    private void TryCreateCube(Cube mainCube)
    {
        float numberIncreaseMultiplier = 1f;
        float chanceDivider = 2f;
        float newChanceDisintegration = mainCube.ChanceDisintegration / chanceDivider;
        float newMultiplierExplode = mainCube.MultiplierExplosion + numberIncreaseMultiplier;

        if (mainCube.IsDisintegration())
        {
            float randomCountSpawn = Random.Range(_minCubesSpawn, _maxCubesSpawn);

            Quaternion rotationCube = Random.rotation;

            List<Rigidbody> cubes = new();

            for (int i = 0; i < randomCountSpawn; i++)
            {
                Cube newCube = Instantiate(mainCube, mainCube.transform.position, rotationCube);
                newCube.Init(newChanceDisintegration, newMultiplierExplode);
                newCube.CubeClicked += TryCreateCube;

                Rigidbody cubeRigidbody;

                if(newCube.TryGetComponent<Rigidbody>(out cubeRigidbody))
                {
                    cubes.Add(cubeRigidbody);
                }
            }

            CubeSpawned?.Invoke(cubes, mainCube.transform);
        }
        else
        {
            CubeDestroyed?.Invoke(mainCube);
        }

        mainCube.CubeClicked -= TryCreateCube;

        Destroy(mainCube.gameObject);
    }
}