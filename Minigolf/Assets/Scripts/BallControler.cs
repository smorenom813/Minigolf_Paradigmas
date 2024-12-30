using UnityEngine;

public class GolfBallController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 dragStartPoint;
    private Vector3 dragEndPoint;
    public float forceMultiplier = 10f; // Ajusta la fuerza seg�n tus necesidades.
    public float maxForce = 20f; // Fuerza m�xima opcional.
    public float minVelocityToStop = 0.1f; // Velocidad m�nima antes de detener la bola.

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Detectar el inicio del arrastre
        if (Input.GetMouseButtonDown(0)) // Al hacer clic o tocar
        {
            dragStartPoint = GetWorldPoint(Input.mousePosition);
        }

        // Detectar el final del arrastre
        if (Input.GetMouseButtonUp(0)) // Al soltar el clic o dedo
        {
            dragEndPoint = GetWorldPoint(Input.mousePosition);
            LaunchBall();
        }

        // Detener la bola si su velocidad es muy peque�a
        if (rb.velocity.magnitude < minVelocityToStop && rb.velocity != Vector3.zero)
        {
            StopBall();
        }
    }

    private Vector3 GetWorldPoint(Vector3 screenPoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    private void LaunchBall()
    {
        Vector3 force = dragStartPoint - dragEndPoint; // La direcci�n se invierte para lanzar hacia donde arrastras.
        force = Vector3.ClampMagnitude(force, maxForce); // Limitar la fuerza m�xima.
        rb.AddForce(force * forceMultiplier, ForceMode.Impulse);
    }

    private void StopBall()
    {
        rb.velocity = Vector3.zero; // Detiene completamente el movimiento.
        rb.angularVelocity = Vector3.zero; // Detiene la rotaci�n.
    }
}

