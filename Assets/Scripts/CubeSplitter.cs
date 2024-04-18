using UnityEngine;

public class CubeSplitter : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private CubeModifier _cubeModifier;
    private int _minNewCubes = 2;
    private int _maxNewCubes = 7;

    public void Split()
    {
        _cubeModifier = GetComponent<CubeModifier>();

        int numberOfCubesToSpawn = Random.Range(_minNewCubes, _maxNewCubes);

        for (int i = 0; i < numberOfCubesToSpawn; i++)
        {
            Cube newCube = Instantiate(_cubePrefab, GetNextPosition(transform.position), Quaternion.identity);

            newCube.SplitChanceChange();
            newCube.ExplotionPowerChange();

            _cubeModifier.ColorChange(newCube);
            _cubeModifier.ScaleChange(newCube);

            Destroy(gameObject);
        }
    }

    private Vector3 GetNextPosition(Vector3 lastPostion)
    {
        float sphereRadius = 5f;

        return lastPostion + Random.insideUnitSphere * sphereRadius + Vector3.up;
    }
}