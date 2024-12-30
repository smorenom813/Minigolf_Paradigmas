using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;// Para mostrar el mensaje en UI

public class CylinderController : MonoBehaviour
{
    public int score = 0;  // Puntuaci�n
    public TMP_Text scoreText;  // Texto UI para mostrar la puntuaci�n
    public TMP_Text messageText;  // Texto UI para mostrar el mensaje cuando toque el cilindro

    public Vector3 respawnPosition;  // Nueva posici�n a la que se mover� la bola

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball"))  // Aseg�rate de que el cilindro tiene el tag "Cylinder"
        {
            Debug.Log("�Trigger activado!");

            score++;  // Sumar un punto
            scoreText.text = "Puntuaci�n: " + score;  // Actualizar el texto de la puntuaci�n

            messageText.text = "�Has ganado un punto!";  // Mostrar mensaje
            Invoke("ClearMessage", 2f);  // Eliminar el mensaje despu�s de 2 segundos

            // Mover la bola a la nueva posici�n


            Thread.Sleep(2000);
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            other.transform.position = respawnPosition;// Establecer la posici�n de la bola
           
        }
    }

    private void ClearMessage()
    {
        messageText.text = "";  // Eliminar el mensaje
    }
}
