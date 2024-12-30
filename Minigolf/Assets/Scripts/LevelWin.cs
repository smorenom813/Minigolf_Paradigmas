using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;// Para mostrar el mensaje en UI

public class CylinderController : MonoBehaviour
{
    public int score = 0;  // Puntuación
    public TMP_Text scoreText;  // Texto UI para mostrar la puntuación
    public TMP_Text messageText;  // Texto UI para mostrar el mensaje cuando toque el cilindro

    public Vector3 respawnPosition;  // Nueva posición a la que se moverá la bola

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball"))  // Asegúrate de que el cilindro tiene el tag "Cylinder"
        {
            Debug.Log("¡Trigger activado!");

            score++;  // Sumar un punto
            scoreText.text = "Puntuación: " + score;  // Actualizar el texto de la puntuación

            messageText.text = "¡Has ganado un punto!";  // Mostrar mensaje
            Invoke("ClearMessage", 2f);  // Eliminar el mensaje después de 2 segundos

            // Mover la bola a la nueva posición


            Thread.Sleep(2000);
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            other.transform.position = respawnPosition;// Establecer la posición de la bola
           
        }
    }

    private void ClearMessage()
    {
        messageText.text = "";  // Eliminar el mensaje
    }
}
