using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CylinderController : MonoBehaviour
{
    public int score = 0;  // Puntuación
    public Vector3 respawnPosition;  // Nueva posición a la que se moverá la bola
    public TMP_Text winText; // Texto UI para mostrar quién ganó.

    private bool gameEnded = false; // Para evitar múltiples mensajes.

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("ball")) // Si es la bola del jugador.
        {
            if (!gameEnded & GameManager.Instance.ShouldPlayWithAI())
            {
                ShowWinMessage("¡Has ganado a la IA!");
                gameEnded = true;
                // Notificar al GameManager para detener la IA.
                GameManager.Instance.StopAI();
            }
            ScoreManager.Instance.AddPoint();
            Tracker.Instance.EndHole();
            RespawnBall("ball");
        }
        else if (other.CompareTag("AIBall") & GameManager.Instance.ShouldPlayWithAI())// Si es la bola de la IA.
        {
            if (!gameEnded)
            {
                ShowWinMessage("¡Ha ganado la IA!");
                gameEnded = true;
                GameManager.Instance.StopAI();
            }
        }
    }

    private void RespawnBall(string ballTag)
    {
        GameObject ball = GameObject.FindGameObjectWithTag(ballTag);
        if (ball == null) return;

        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero; // Detener la bola.

        ball.transform.position = respawnPosition; // Reposicionar la bola.
    }

    private void ShowWinMessage(string message)
    {
        winText.gameObject.SetActive(true); // Mostrar el texto en pantalla.
        winText.text = message;
        Debug.Log(message);

        // Iniciar la corutina para ocultar el mensaje después de 10 segundos.
        StartCoroutine(HideWinMessageAfterDelay(10f));
    }

    private System.Collections.IEnumerator HideWinMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Esperar el tiempo indicado.
        winText.gameObject.SetActive(false); // Ocultar el texto después de 10 segundos.
        Debug.Log("El mensaje de victoria se ha ocultado.");
    }

}
