using System;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private RandomGenerator _randomGenerator;

    private float _minCubesSpawn = 2f;
    private float _maxCubesSpawn = 6f;

    public event Action <List<Rigidbody>, Cube> CubeSpawned;

    public void TryCreateCube(Cube mainCube)
    {
        if (mainCube.IsDisintegration())
        {
            float randomCountSpawn = _randomGenerator.GetRandomChance(_minCubesSpawn, _maxCubesSpawn);

            Quaternion rotationCube = _randomGenerator.GetRandomRotation();

            List<Rigidbody> cubeList = new();

            for (int i = 0; i < randomCountSpawn; i++)
            {
                Cube newCube = Instantiate(mainCube, mainCube.transform.position, rotationCube);
                newCube.PrepareGeneration(mainCube.NumberDecays);
                cubeList.Add(newCube.GetComponent<Rigidbody>());
            }

            CubeSpawned?.Invoke(cubeList, mainCube);
        }

        Destroy(mainCube.gameObject);
    }
}