using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private MeshRenderer _meshRenderer;
    private float _chanceDisintegration = 100f;

    public float ChanceDisintegration => _chanceDisintegration;

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

        float randomNumber = Random.Range(minChance, maxChance);

        return randomNumber <= ChanceDisintegration;
    }

    public void PrepareGeneration()
    {
        float divider = 2f;
        float hueMin = 0f;
        float hueMax = 1f;
        float saturationMin = 0f;
        float saturationMax = 1f;
        float alphaMin = 0f;
        float alphaMax = 1f;

        transform.localScale /= divider;
        _meshRenderer.material.color = Random.ColorHSV(hueMin, hueMax, saturationMin,saturationMax, alphaMin, alphaMax);
    }

    public void SetSplitChance(float chance)
    {
        float minChance = 0;
        float maxChance = 100;

        _chanceDisintegration = Mathf.Clamp(chance, minChance, maxChance);
    }
}