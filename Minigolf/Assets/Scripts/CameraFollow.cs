using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform ball; // La bola que la cámara sigue.
    public Transform[] courseAssets; // Array de assets en el orden del recorrido.
    public float heightAboveBall = 3f; // Altura fija sobre la bola.
    public float distanceBehindBall = 6f; // Distancia fija detrás de la bola.
    public float smoothSpeed = 0.2f; // Suavidad en el movimiento de la cámara.
    public int scene;

    private int currentAssetIndex = 0; // Índice del asset actual.

    void Start()
    {
        if (ball == null || courseAssets.Length == 0)
        {
            Debug.LogError("¡Por favor, asigna la bola y los assets del mapa al script!");
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

            // Actualizar el siguiente asset basado en la posición de la bola.
            UpdateNextAsset();

            // Calcular la dirección hacia el siguiente asset.
            Vector3 directionToNextAsset = (courseAssets[currentAssetIndex].position - ball.position).normalized;

            // Calcular la posición de la cámara DETRÁS de la bola (en la dirección opuesta al siguiente asset).
            Vector3 targetPosition = ball.position + directionToNextAsset * distanceBehindBall + Vector3.up * heightAboveBall;

            // Interpolar suavemente la posición de la cámara hacia la posición deseada.
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

            // Hacer que la cámara mire hacia la bola.
            transform.LookAt(ball.position);
        }

        else if (scene == 2)
        {
            if (ball.position == new Vector3(0, 0.0982986167f, 0))
            {

                currentAssetIndex = 0;

            }
            if (ball == null || courseAssets.Length == 0) return;

            // Actualizar el siguiente asset basado en la posición de la bola.
            UpdateNextAsset();

            // Calcular la dirección hacia el siguiente asset.
            Vector3 directionToNextAsset = (courseAssets[currentAssetIndex].position - ball.position).normalized;

            // *Posicionar la cámara detrás de la bola en la dirección contraria al siguiente asset*.
            Vector3 targetPosition = ball.position + directionToNextAsset * (-distanceBehindBall) + Vector3.up * heightAboveBall;

            // Interpolar suavemente la posición de la cámara hacia la posición deseada.
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

            // Hacer que la bola mire a la cámara
            transform.LookAt(ball.position);

        }
    }

    private void UpdateNextAsset()
    {
        // Comprobar la distancia al siguiente asset.
        float distanceToNextAsset = Vector3.Distance(ball.position, courseAssets[currentAssetIndex].position);

        // Si la bola está cerca del siguiente asset, avanzar al siguiente.
        if (distanceToNextAsset < 1f && currentAssetIndex < courseAssets.Length - 1)
        {
            currentAssetIndex++;
        }
    }
}