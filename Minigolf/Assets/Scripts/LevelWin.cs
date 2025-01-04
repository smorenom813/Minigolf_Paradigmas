using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;// Para mostrar el mensaje en UI

public class CylinderController : MonoBehaviour
{
    public int score = 0;  // Puntuación
    public Vector3 respawnPosition;  // Nueva posición a la que se moverá la bola

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball"))  // Asegúrate de que el cilindro tiene el tag "Cylinder"
        {

            ScoreManager.Instance.AddPoint();
            Tracker.Instance.EndHole();
            Thread.Sleep(2000);
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            other.transform.position = respawnPosition;// Establecer la posición de la bola
           
        }
    }

}
