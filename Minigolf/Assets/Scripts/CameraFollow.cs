using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform ball; // La bola que la c�mara sigue.
    public Transform[] courseAssets; // Array de assets en el orden del recorrido.
    public float heightAboveBall = 3f; // Altura fija sobre la bola.
    public float distanceBehindBall = 6f; // Distancia fija detr�s de la bola.
    public float smoothSpeed = 0.2f; // Suavidad en el movimiento de la c�mara.
    public int scene;

    private int currentAssetIndex = 0; // �ndice del asset actual.

    void Start()
    {
        if (ball == null || courseAssets.Length == 0)
        {
            Debug.LogError("�Por favor, asigna la bola y los assets del mapa al script!");
            return;
        }
    }

    void LateUpdate()

    {
        if (scene == 1)
        {
            if (ball.position == new Vector3(-1.11044767e-12f, 0.0982986167f, -7.36743644e-09f))
            {

                currentAssetIndex = 0;

            }
            if (ball == null || courseAssets.Length == 0) return;

            // Actualizar el siguiente asset basado en la posici�n de la bola.
            UpdateNextAsset();

            // Calcular la direcci�n hacia el siguiente asset.
            Vector3 directionToNextAsset = (courseAssets[currentAssetIndex].position - ball.position).normalized;

            // Calcular la posici�n de la c�mara DETR�S de la bola (en la direcci�n opuesta al siguiente asset).
            Vector3 targetPosition = ball.position + directionToNextAsset * distanceBehindBall + Vector3.up * heightAboveBall;

            // Interpolar suavemente la posici�n de la c�mara hacia la posici�n deseada.
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

            // Hacer que la c�mara mire hacia la bola.
            transform.LookAt(ball.position);
        }

        else if (scene == 2)
        {
            if (ball.position == new Vector3(0, 0.0982986167f, 0))
            {

                currentAssetIndex = 0;

            }
            if (ball == null || courseAssets.Length == 0) return;

            // Actualizar el siguiente asset basado en la posici�n de la bola.
            UpdateNextAsset();

            // Calcular la direcci�n hacia el siguiente asset.
            Vector3 directionToNextAsset = (courseAssets[currentAssetIndex].position - ball.position).normalized;

            // *Posicionar la c�mara detr�s de la bola en la direcci�n contraria al siguiente asset*.
            Vector3 targetPosition = ball.position + directionToNextAsset * (-distanceBehindBall) + Vector3.up * heightAboveBall;

            // Interpolar suavemente la posici�n de la c�mara hacia la posici�n deseada.
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

            // Hacer que la bola mire a la c�mara
            transform.LookAt(ball.position);

        }
    }

    private void UpdateNextAsset()
    {
        // Comprobar la distancia al siguiente asset.
        float distanceToNextAsset = Vector3.Distance(ball.position, courseAssets[currentAssetIndex].position);

        // Si la bola est� cerca del siguiente asset, avanzar al siguiente.
        if (distanceToNextAsset < 1f && currentAssetIndex < courseAssets.Length - 1)
        {
            currentAssetIndex++;
        }
    }
}