using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform ball; // La bola que la c�mara sigue.
    public Transform flag;
    public float heightAboveBall = 0.5f; // Altura fija sobre la bola.
    public float distanceBehindBall = 1f; // Distancia fija en el eje Z detr�s de la bola.
    public float rotationSpeed = 5f; // Velocidad de rotaci�n en el eje Y.
    public float verticalRotationSpeed = 2f; // Velocidad de rotaci�n vertical (mirar arriba/abajo).
    public float minVelocityToRotate = 0.1f; // Velocidad m�nima de la bola para permitir rotaci�n.
    public float maxVerticalAngle = 10f;// �ngulo m�ximo para rotaci�n vertical (para evitar volcar la c�mara).
    public float proximityToFlag = 5f;
    public float headOffset = 2f; // Ajuste para subir la c�mara y ver m�s all� de la bola.
    public float smoothSpeed = 0.1f;

    private Rigidbody ballRigidbody;
    private float currentRotationAngle = 0f; // �ngulo actual de rotaci�n alrededor de la bola (horizontal).
    private float verticalAngle = 0f; // �ngulo actual de rotaci�n en el eje Y (vertical).
    private bool isCameraFrozen = false; // Indica si la c�mara est� congelada (movimiento detenido).

    private Vector3 lastCameraPosition; // �ltima posici�n de la c�mara mientras la bola se mov�a.
    private Quaternion lastCameraRotation; // �ltima rotaci�n de la c�mara mientras la bola se mov�a.

    void Start()
    {
        if (ball == null)
        {
            Debug.LogError("�Por favor, asigna la bola al script!");
            return;
        }

        // Obtener el Rigidbody de la bola.
        ballRigidbody = ball.GetComponent<Rigidbody>();
        lastCameraPosition = new Vector3(0, heightAboveBall, -distanceBehindBall);
        lastCameraRotation = Quaternion.identity;

        if (ballRigidbody == null)
        {
            Debug.LogError("�La bola debe tener un Rigidbody para este script!");
            return;
        }
    }

    void Update()
    {
        if (ball == null || ballRigidbody == null) return;
        float distanceToFlag = Vector3.Distance(ball.position, flag.position);

        if (distanceToFlag <= proximityToFlag)
        {
            // Posicionar la c�mara en la l�nea recta entre la bola y la bandera.
            Vector3 directionToFlag = (flag.position - ball.position).normalized;
            Vector3 targetPosition = ball.position + directionToFlag * distanceBehindBall+new Vector3(0,heightAboveBall,0);

            // Interpolar suavemente la posici�n de la c�mara hacia el objetivo.
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

            // Orientar la c�mara hacia la bandera.
            transform.LookAt(flag.position);
        }
        else
        {
            // Detectar si la bola est� quieta.

            if (ball.position == new Vector3(0, 0.1f, 0))
            {
               
                transform.position = ball.position + new Vector3(1,1,1);
                transform.LookAt(ball.position);
                lastCameraPosition = transform.position;
                lastCameraRotation = transform.rotation;
            }
            if (ballRigidbody.velocity.magnitude < minVelocityToRotate)
            {





                // Si la bola est� quieta, mantener la posici�n y orientaci�n anteriores.
                if (lastCameraPosition != Vector3.zero && lastCameraRotation != Quaternion.identity)
                {
                    transform.position = lastCameraPosition;
                    transform.rotation = lastCameraRotation;
                }
                else
                {
                    Vector3 offset = new Vector3(0, 0, -distanceBehindBall);

                    // Actualizar la posici�n de la c�mara con la rotaci�n calculada.
                    transform.position = ball.position + offset;
                    transform.LookAt(ball.position);


                }

                // Detectar si el clic est� presionado para congelar la c�mara.
                if (Input.GetMouseButton(0)) // 0 es para el clic izquierdo
                {
                    isCameraFrozen = true; // Congelar c�mara si el clic est� presionado.
                }


                else
                {
                    isCameraFrozen = false; // Descongelar c�mara cuando el clic se suelta.
                }

                if (!isCameraFrozen)
                {
                    // Rotar alrededor de la bola en el eje Y (horizontal).
                    float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
                    currentRotationAngle += mouseX;

                    // Rotar la c�mara en el eje X (vertical), limitando la rotaci�n para evitar volcarse.
                    float mouseY = Input.GetAxis("Mouse Y") * verticalRotationSpeed;
                    verticalAngle -= mouseY;
                    verticalAngle = Mathf.Clamp(verticalAngle, -maxVerticalAngle, maxVerticalAngle); // Limitar el rango de rotaci�n vertical.

                    // Crear la rotaci�n final combinada en los ejes X (vertical) y Y (horizontal).
                    Quaternion rotation = Quaternion.Euler(verticalAngle, currentRotationAngle, 0);
                    Vector3 offset = rotation * new Vector3(1, 1, 1);

                    // Actualizar la posici�n de la c�mara con la rotaci�n calculada.
                    transform.position = lastCameraPosition + offset;
                    transform.LookAt(ball.position);
                }
            }
            else
            {
                // Calcular la direcci�n del movimiento de la bola.
                Vector3 direction = ballRigidbody.velocity.normalized;

                // Calcular la posici�n deseada detr�s de la bola.
                Vector3 targetPosition = ball.position + direction * distanceBehindBall + new Vector3(0, heightAboveBall, 0);

                // Interpolar suavemente la posici�n de la c�mara hacia la posici�n deseada.
                float smoothSpeed = 0.1f;
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

                // Suavizar la rotaci�n hacia la bola.
                Vector3 targetDirection = ball.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed);

                // Guardar la posici�n y orientaci�n actuales.
                lastCameraPosition = transform.position;
                lastCameraRotation = transform.rotation;
            }
        }
    }
}