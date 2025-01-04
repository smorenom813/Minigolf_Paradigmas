using UnityEngine;

public class ResetPlane : MonoBehaviour
{
    public Transform ball; // Referencia a la bola
    public Transform cameraTransform; // Referencia a la c�mara
    private Rigidbody ballRigidbody; // Rigidbody de la bola

    public Vector3 startPosition = new Vector3(0, 0.1f, 0); // Posici�n inicial de la bola
    public Vector3 cameraStartPosition = new Vector3(1, 1f, 1f); // Posici�n inicial de la c�mara
    public Quaternion cameraStartRotation = Quaternion.Euler(10f, 0f, 0f); // Rotaci�n inicial de la c�mara

    void Start()
    {
        if (ball == null || cameraTransform == null)
        {
            Debug.LogError("�Por favor, asigna la bola y la c�mara al script!");
            return;
        }

        ballRigidbody = ball.GetComponent<Rigidbody>();

        if (ballRigidbody == null)
        {
            Debug.LogError("�La bola debe tener un Rigidbody para este script!");
            return;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        // Detectar si el objeto que entra es la bola
        if (other.transform == ball)
        {
            // Detener la bola y moverla a la posici�n inicial
            ballRigidbody.velocity = Vector3.zero;
            ballRigidbody.angularVelocity = Vector3.zero;
            ball.position = startPosition;

            // Mover la c�mara a la posici�n inicial
            cameraTransform.position = cameraStartPosition;
            cameraTransform.rotation = cameraStartRotation;
        }
    }
}
