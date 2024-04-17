using System.Collections.Generic;
using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _splitChance;

    [SerializeField] private CubeExplosion _cubePrefab;

    private float _minSplitRate = 0f;
    private float _maxSplitRate = 1f;
    private float _halveValue = 0.5f;

    private int _minNewCubes = 2;
    private int _maxNewCubes = 7;

    private void OnMouseUpAsButton()
    {
        Split();
        Explode();
    }

    private void Split()
    {
        float splitRate = Random.Range(_minSplitRate, _maxSplitRate);

        if (splitRate < _splitChance)
        {
            int numberOfCubesToSpawn = Random.Range(_minNewCubes, _maxNewCubes);

            for (int i = 0; i < numberOfCubesToSpawn; i++)
            {
                CubeExplosion newCube = Instantiate(_cubePrefab);

                ScaleChange(newCube);
                SplitChanceChange(newCube);
                ColorChange(newCube);             
            }
        }
    }

    private void ScaleChange(CubeExplosion cube)
    {
        cube.transform.localScale *= _halveValue;
    }

    private void SplitChanceChange(CubeExplosion cube)
    {
        cube._splitChance *= _halveValue;
    }

    private void ColorChange(CubeExplosion cube)
    {
        Color randomColor = Random.ColorHSV();

        cube.GetComponent<Renderer>().material.color = randomColor;
    }

    private void Explode()
    {
        foreach (Rigidbody explodingObject in GetExplodingObjects())
            explodingObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);

        Destroy(gameObject);
    }

    private List<Rigidbody> GetExplodingObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> cubes = new List<Rigidbody>();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);

        return cubes;
    }
}