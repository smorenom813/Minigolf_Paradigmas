using UnityEngine;

public class FlagController : MonoBehaviour
{
    public Transform ball; // La bola que se mueve.
    public Transform flag; // La bandera que se levanta.
    public Transform hole; // El hoyo.
    public float raiseHeight = 0.25f; // La altura en la que se levanta la bandera.
    public float raiseSpeed = 1f; // La velocidad a la que se levanta la bandera.
    public float triggerDistance = 1f; // La distancia a la que la bandera comienza a elevarse.

    private Vector3 originalFlagPosition; // Posici�n original de la bandera.

    void Start()
    {
        // Guardar la posici�n original de la bandera.
        originalFlagPosition = flag.position;
    }

    void Update()
    {
        if (ball == null || flag == null || hole == null) return;

        // Calcular la distancia entre la bola y el hoyo.
        float distanceToHole = Vector3.Distance(ball.position, hole.position);

        // Si la bola est� dentro de la distancia para activar el levantamiento
        if (distanceToHole < triggerDistance)
        {
            // Levantar la bandera de forma gradual, pero sin teletransportar.
            Vector3 targetPosition = originalFlagPosition + new Vector3(0, raiseHeight, 0);
            flag.position = Vector3.Lerp(flag.position, targetPosition, raiseSpeed * Time.deltaTime);
        }
        else
        {
            // Si la bola est� fuera de la distancia, devolver la bandera a su posici�n original.
            flag.position = Vector3.Lerp(flag.position, originalFlagPosition, raiseSpeed * Time.deltaTime);
        }
    }
}
