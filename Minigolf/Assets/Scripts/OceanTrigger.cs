using UnityEngine;

public class ResetPlane : MonoBehaviour
{
    public Transform ball; // Referencia a la bola
    public Transform cameraTransform; // Referencia a la cámara
    private Rigidbody ballRigidbody; // Rigidbody de la bola

    public Vector3 startPosition = new Vector3(0, 0.1f, 0); // Posición inicial de la bola
    public Vector3 cameraStartPosition = new Vector3(1, 1f, 1f); // Posición inicial de la cámara
    public Quaternion cameraStartRotation = Quaternion.Euler(10f, 0f, 0f); // Rotación inicial de la cámara

    void Start()
    {
        if (ball == null || cameraTransform == null)
        {
            Debug.LogError("¡Por favor, asigna la bola y la cámara al script!");
            return;
        }

        ballRigidbody = ball.GetComponent<Rigidbody>();

        if (ballRigidbody == null)
        {
            Debug.LogError("¡La bola debe tener un Rigidbody para este script!");
            return;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        // Detectar si el objeto que entra es la bola
        if (other.transform == ball)
        {
            // Detener la bola y moverla a la posición inicial
            ballRigidbody.velocity = Vector3.zero;
            ballRigidbody.angularVelocity = Vector3.zero;
            ball.position = startPosition;

            // Mover la cámara a la posición inicial
            cameraTransform.position = cameraStartPosition;
            cameraTransform.rotation = cameraStartRotation;
        }
    }
}
