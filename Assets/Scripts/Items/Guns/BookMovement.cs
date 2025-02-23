using UnityEngine;

public class BookMovement : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float frequency = 1f;   

    private Vector3 start_position;

    void Start()
    {
        start_position = transform.localPosition;
    }

    void Update()
    {
        float y_offset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.localPosition = start_position + new Vector3(0, y_offset, 0);
        transform.Rotate(0, 0, 50 * Time.deltaTime);
    }
}