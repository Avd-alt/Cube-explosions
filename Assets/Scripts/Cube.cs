using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private float _chanceDisintegration = 100f;
    private float _multiplierExplosion = 1f;

    public float MultiplierExplosion => _multiplierExplosion;
    public float ChanceDisintegration => _chanceDisintegration;

    public event Action <Cube> CubeClicked;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnMouseDown()
    {
        CubeClicked?.Invoke(this);
    }

    public bool IsDisintegration()
    {
        float minChance = 0f;
        float maxChance = 100f;

        float randomNumber = Random.Range(minChance, maxChance);

        return randomNumber <= ChanceDisintegration;
    }

    public void Init(float chance, float newMultiplier)
    {
        float divider = 2f;
        float hueMin = 0f;
        float hueMax = 1f;
        float saturationMin = 0f;
        float saturationMax = 1f;
        float alphaMin = 0f;
        float alphaMax = 1f;
        float minChance = 0;
        float maxChance = 100;

        transform.localScale /= divider;
        _chanceDisintegration = Mathf.Clamp(chance, minChance, maxChance);
        _multiplierExplosion = newMultiplier;
        _meshRenderer.material.color = Random.ColorHSV(hueMin, hueMax, saturationMin,saturationMax, alphaMin, alphaMax);
    }
}