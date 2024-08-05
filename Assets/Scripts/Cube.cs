using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private RandomGenerator _randomGenerator;
    [SerializeField] private CubeSpawner _cubeSpawner;

    private MeshRenderer _meshRenderer;
    private float _chanceDisintegration = 100f;

    public float NumberDecays { get; private set; } = 1;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnMouseDown()
    {
        _cubeSpawner.TryCreateCube(this);
    }

    public bool IsDisintegration()
    {
        float minChance = 0f;
        float maxChance = 100f;

        float randomNumber = _randomGenerator.GetRandomChance(minChance, maxChance);

        if (randomNumber <= _chanceDisintegration / NumberDecays)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PrepareGeneration(float numberDecays)
    {
        float multiplier = 2f;
        float hueMin = 0f;
        float hueMax = 1f;
        float saturationMin = 0f;
        float saturationMax = 1f;
        float alphaMin = 0f;
        float alphaMax = 1f;

        NumberDecays = numberDecays * multiplier;
        gameObject.transform.localScale /= NumberDecays;
        _meshRenderer.material.color = _randomGenerator.GetRandomColor(hueMin, hueMax, saturationMin,saturationMax, alphaMin, alphaMax);
    }
}