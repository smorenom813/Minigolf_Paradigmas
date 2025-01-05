using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    public Rigidbody ball; // Rigidbody de la bola.
    public float forceMultiplier = 10f; // Fuerza del golpe.
    public float forceMultiplierChasing = 1f; // Fuerza del golpe.
    public float waitTimeBetweenShots = 2f; // Tiempo entre golpes.
    public Transform[] courseAssets; // Array de puntos del recorrido (assets).
    public Transform hole; // Transform del hoyo.
    public Transform cylinder; // Cilindro del hoyo (con Collider).

    private int currentAssetIndex = 1; // Índice del asset actual.
    private bool isWaiting = false; // Evita múltiples golpes mientras la bola se mueve.
    private bool isPlaying = false; // Indica si la IA está activa.
    private bool aimingForHole = false; // Saber si la IA ya está apuntando al cilindro.

    private void Start()
    {
        if (!GameManager.Instance.ShouldPlayWithAI())
        {
            Debug.Log("La IA está desactivada. Eliminando la bola de la IA.");
            Destroy(ball.gameObject);
            return;
        }

        Debug.Log("La IA está activada.");
    }

    private void Update()
    {
        if (!isPlaying || isWaiting)
        {
            return; // No hacer nada si la IA está desactivada o en espera.
        }

        if (currentAssetIndex >= courseAssets.Length || aimingForHole)
        {
            // Golpear hacia el cilindro del hoyo cuando se llegue al último asset.
            StartCoroutine(PlayShot(cylinder.position)); // Golpear hacia el cilindro.
            aimingForHole = true;
        }
        else
        {
            StartCoroutine(PlayShot(courseAssets[currentAssetIndex].position)); // Golpea hacia el siguiente asset.
            currentAssetIndex++; // Pasar al siguiente asset después del golpe.
        }
    }

    private System.Collections.IEnumerator PlayShot(Vector3 targetPosition)
    {
        isWaiting = true;

        // Calcular la dirección hacia el objetivo.
        Vector3 direction = (targetPosition - ball.position).normalized;

        // Detener la bola antes de aplicar la fuerza.
        ball.velocity = Vector3.zero;

        // Golpear la bola hacia el objetivo.
        if (!aimingForHole)
        { ball.AddForce(direction * forceMultiplier, ForceMode.Impulse); }
        else
        {
            ball.AddForce(direction * forceMultiplierChasing, ForceMode.Impulse);
        }
        Debug.Log($"La IA ha realizado un golpe hacia {(targetPosition == cylinder.position ? "el cilindro del hoyo" : $"el asset {currentAssetIndex}")}.");

        yield return new WaitForSeconds(waitTimeBetweenShots); // Esperar antes de permitir otro golpe.

        isWaiting = false;
    }

    public void StartAI()
    {
        isPlaying = true; // Activar la IA.
        Debug.Log("IA activada y lista para jugar.");
    }

    public void StopAI()
    {
        isPlaying = false; // Detener la IA.
        ball.velocity = Vector3.zero; // Detener la bola.
        Destroy(ball.gameObject);
        Debug.Log("IA desactivada.");
    }
}
