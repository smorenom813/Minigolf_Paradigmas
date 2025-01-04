using UnityEngine;

public class GolfClubPendulum : MonoBehaviour
{
    public float speed = 2f; // Velocidad de oscilaci�n
    public float angle = 45f; // �ngulo m�ximo de oscilaci�n

    private float initialAngle;
    private float time;

    void Start()
    {
        initialAngle = transform.rotation.eulerAngles.z;
        time = 0f;
    }

    void Update()
    {
        time += Time.deltaTime;
        float zRotation = initialAngle + Mathf.Sin(time * speed) * angle;
        Debug.Log("Z Rotation: " + zRotation); // Muestra los �ngulos calculados.
        transform.rotation = Quaternion.Euler(0, 0, zRotation);
    }
}

