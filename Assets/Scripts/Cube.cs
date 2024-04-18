using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _splitChance;

    [SerializeField] private Cube _cubePrefab;

    private float _minSplitRate = 0f;
    private float _maxSplitRate = 1f;
    private float _halveValue = 0.5f;
    private float _doubleValue = 2f;

    private int _minNewCubes = 2;
    private int _maxNewCubes = 7;

    private void OnMouseUpAsButton()
    {
        TrySplit();
    }

    private void TrySplit()
    {
        float splitRate = Random.Range(_minSplitRate, _maxSplitRate);

        if (splitRate < _splitChance)
        {
            int numberOfCubesToSpawn = Random.Range(_minNewCubes, _maxNewCubes);

            for (int i = 0; i < numberOfCubesToSpawn; i++)
            {
                Cube newCube = Instantiate(_cubePrefab, GetNextPosition(transform.position), Quaternion.identity);

                ScaleChange(newCube);
                SplitChanceChange(newCube);
                ColorChange(newCube);
                ExplotionPowerChange(newCube);

                Destroy(gameObject);
            }
        }
        else
        {
            Explode();
        }
    }

    private void ScaleChange(Cube cube)
    {
        cube.transform.localScale *= _halveValue;
    }

    private void SplitChanceChange(Cube cube)
    {
        cube._splitChance *= _halveValue;
    }

    private void ExplotionPowerChange(Cube cube)
    {
        cube._explosionForce *= _doubleValue;
        cube._explosionRadius *= _doubleValue;
    }

    private void ColorChange(Cube cube)
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

    private Vector3 GetNextPosition(Vector3 lastPostion)
    {
        float sphereRadius = 5f;

        return lastPostion + Random.insideUnitSphere * sphereRadius + Vector3.up;
    }
}