using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    public float GetRandomChance(float minChance, float maxChance)
    {
        return Random.Range(minChance, maxChance);
    }

    public Color GetRandomColor(float hueMin, float hueMax, float saturationMin, float saturationMax, float alphaMin, float alphaMax)
    {
        return Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, alphaMin, alphaMax);
    }

    public Quaternion GetRandomRotation()
    {
        return Random.rotation;
    }
}