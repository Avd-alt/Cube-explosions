using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : MonoBehaviour
{
    private float _minCubesSpawn = 2f;
    private float _maxCubesSpawn = 6f;

    public event Action <List<Rigidbody>, Cube> CubeSpawned;

    public void TryCreateCube(Cube mainCube)
    {
        float chanceDivider = 2f;
        float newChanceDisintegration = mainCube.ChanceDisintegration / chanceDivider;

        if (mainCube.IsDisintegration())
        {
            float randomCountSpawn = Random.Range(_minCubesSpawn, _maxCubesSpawn);

            Quaternion rotationCube = Random.rotation;

            List<Rigidbody> cubes = new();

            for (int i = 0; i < randomCountSpawn; i++)
            {
                Cube newCube = Instantiate(mainCube, mainCube.transform.position, rotationCube);
                newCube.PrepareGeneration();
                newCube.SetSplitChance(newChanceDisintegration);
                cubes.Add(newCube.GetComponent<Rigidbody>());
            }

            CubeSpawned?.Invoke(cubes, mainCube);
        }

        Destroy(mainCube.gameObject);
    }
}