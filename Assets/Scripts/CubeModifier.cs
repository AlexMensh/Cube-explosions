using UnityEngine;

public class CubeModifier : MonoBehaviour
{
    private float _halveValue = 0.5f;

    public void ColorChange(Cube cube)
    {
        Color randomColor = Random.ColorHSV();

        cube.GetComponent<Renderer>().material.color = randomColor;
    }

    public void ScaleChange(Cube cube)
    {
        cube.transform.localScale *= _halveValue;
    }
}