using UnityEngine;

[RequireComponent(typeof(CubeExploder))]
[RequireComponent(typeof(CubeSplitter))]

public class Cube : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _splitChance;

    private CubeExploder _cubeExploder;
    private CubeSplitter _cubeSplitter;
    private float _minSplitRate = 0f;
    private float _maxSplitRate = 1f;
    private float _doubleValue = 2f;

    private void Awake()
    {
        _cubeExploder = GetComponent<CubeExploder>();
        _cubeSplitter = GetComponent<CubeSplitter>();
    }

    private void OnMouseUpAsButton()
    {
        TrySplit();
    }

    public void Init()
    {
        DecreaseSplitChance();
        IncreaseExplosionPower();
    }

    private void TrySplit()
    {
        float splitRate = Random.Range(_minSplitRate, _maxSplitRate);

        if (splitRate < _splitChance)
        {
            _cubeSplitter.Split();
        }
        else
        {
            _cubeExploder.Explode(_explosionRadius, _explosionForce);
        }
    }

    private void DecreaseSplitChance()
    {
        float halveValue = 0.5f;

        _splitChance *= halveValue;
    }

    private void IncreaseExplosionPower()
    {
        _explosionForce *= _doubleValue;
        _explosionRadius *= _doubleValue;
    }
}