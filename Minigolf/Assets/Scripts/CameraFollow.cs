using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform ball; // La bola que la cámara sigue.
    public Transform flag;
    public float heightAboveBall = 0.5f; // Altura fija sobre la bola.
    public float distanceBehindBall = 1f; // Distancia fija en el eje Z detrás de la bola.
    public float rotationSpeed = 5f; // Velocidad de rotación en el eje Y.
    public float verticalRotationSpeed = 2f; // Velocidad de rotación vertical (mirar arriba/abajo).
    public float minVelocityToRotate = 0.1f; // Velocidad mínima de la bola para permitir rotación.
    public float maxVerticalAngle = 10f;// Ángulo máximo para rotación vertical (para evitar volcar la cámara).
    public float proximityToFlag = 5f;
    public float headOffset = 2f; // Ajuste para subir la cámara y ver más allá de la bola.
    public float smoothSpeed = 0.1f;

    private Rigidbody ballRigidbody;
    private float currentRotationAngle = 0f; // Ángulo actual de rotación alrededor de la bola (horizontal).
    private float verticalAngle = 0f; // Ángulo actual de rotación en el eje Y (vertical).
    private bool isCameraFrozen = false; // Indica si la cámara está congelada (movimiento detenido).

    private Vector3 lastCameraPosition; // Última posición de la cámara mientras la bola se movía.
    private Quaternion lastCameraRotation; // Última rotación de la cámara mientras la bola se movía.

    void Start()
    {
        if (ball == null)
        {
            Debug.LogError("¡Por favor, asigna la bola al script!");
            return;
        }

        // Obtener el Rigidbody de la bola.
        ballRigidbody = ball.GetComponent<Rigidbody>();
        lastCameraPosition = new Vector3(0, heightAboveBall, -distanceBehindBall);
        lastCameraRotation = Quaternion.identity;

        if (ballRigidbody == null)
        {
            Debug.LogError("¡La bola debe tener un Rigidbody para este script!");
            return;
        }
    }

    void Update()
    {
        if (ball == null || ballRigidbody == null) return;
        float distanceToFlag = Vector3.Distance(ball.position, flag.position);

        if (distanceToFlag <= proximityToFlag)
        {
            // Posicionar la cámara en la línea recta entre la bola y la bandera.
            Vector3 directionToFlag = (flag.position - ball.position).normalized;
            Vector3 targetPosition = ball.position + directionToFlag * distanceBehindBall+new Vector3(0,heightAboveBall,0);

            // Interpolar suavemente la posición de la cámara hacia el objetivo.
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

            // Orientar la cámara hacia la bandera.
            transform.LookAt(flag.position);
        }
        else
        {
            // Detectar si la bola está quieta.

            if (ball.position == new Vector3(0, 0.1f, 0))
            {
               
                transform.position = ball.position + new Vector3(1,1,1);
                transform.LookAt(ball.position);
                lastCameraPosition = transform.position;
                lastCameraRotation = transform.rotation;
            }
            if (ballRigidbody.velocity.magnitude < minVelocityToRotate)
            {





                // Si la bola está quieta, mantener la posición y orientación anteriores.
                if (lastCameraPosition != Vector3.zero && lastCameraRotation != Quaternion.identity)
                {
                    transform.position = lastCameraPosition;
                    transform.rotation = lastCameraRotation;
                }
                else
                {
                    Vector3 offset = new Vector3(0, 0, -distanceBehindBall);

                    // Actualizar la posición de la cámara con la rotación calculada.
                    transform.position = ball.position + offset;
                    transform.LookAt(ball.position);


                }

                // Detectar si el clic está presionado para congelar la cámara.
                if (Input.GetMouseButton(0)) // 0 es para el clic izquierdo
                {
                    isCameraFrozen = true; // Congelar cámara si el clic está presionado.
                }


                else
                {
                    isCameraFrozen = false; // Descongelar cámara cuando el clic se suelta.
                }

                if (!isCameraFrozen)
                {
                    // Rotar alrededor de la bola en el eje Y (horizontal).
                    float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
                    currentRotationAngle += mouseX;

                    // Rotar la cámara en el eje X (vertical), limitando la rotación para evitar volcarse.
                    float mouseY = Input.GetAxis("Mouse Y") * verticalRotationSpeed;
                    verticalAngle -= mouseY;
                    verticalAngle = Mathf.Clamp(verticalAngle, -maxVerticalAngle, maxVerticalAngle); // Limitar el rango de rotación vertical.

                    // Crear la rotación final combinada en los ejes X (vertical) y Y (horizontal).
                    Quaternion rotation = Quaternion.Euler(verticalAngle, currentRotationAngle, 0);
                    Vector3 offset = rotation * new Vector3(1, 1, 1);

                    // Actualizar la posición de la cámara con la rotación calculada.
                    transform.position = lastCameraPosition + offset;
                    transform.LookAt(ball.position);
                }
            }
            else
            {
                // Calcular la dirección del movimiento de la bola.
                Vector3 direction = ballRigidbody.velocity.normalized;

                // Calcular la posición deseada detrás de la bola.
                Vector3 targetPosition = ball.position + direction * distanceBehindBall + new Vector3(0, heightAboveBall, 0);

                // Interpolar suavemente la posición de la cámara hacia la posición deseada.
                float smoothSpeed = 0.1f;
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

                // Suavizar la rotación hacia la bola.
                Vector3 targetDirection = ball.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed);

                // Guardar la posición y orientación actuales.
                lastCameraPosition = transform.position;
                lastCameraRotation = transform.rotation;
            }
        }
    }
}