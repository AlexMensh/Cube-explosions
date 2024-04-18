using UnityEngine;

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

    private void OnMouseUpAsButton()
    {
        _cubeExploder = GetComponent<CubeExploder>();
        _cubeSplitter = GetComponent<CubeSplitter>();

        TrySplit();
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

    public void SplitChanceChange()
    {
        float halveValue = 0.5f;

        _splitChance *= halveValue;
    }

    public void ExplotionPowerChange()
    {
        _explosionForce *= _doubleValue;
        _explosionRadius *= _doubleValue;
    }
}