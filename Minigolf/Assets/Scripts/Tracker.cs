using UnityEngine;
using TMPro;

public class Tracker : MonoBehaviour
{
    public static Tracker Instance { get; private set; }

    public TMP_Text strokeText; // Texto para los golpes.
    public TMP_Text timeText; // Texto para mostrar el tiempo.
    public TMP_Text scoreText; // Texto para mostrar el puntaje.

    private int currentStrokes = 0; // Golpes actuales del nivel.
    public string levelName; // Nombre del nivel (para identificar los registros).

    private float startTime; // Tiempo inicial del nivel.
    private float elapsedTime; // Tiempo transcurrido.
    private bool isTracking = false; // Controla si el temporizador está en marcha.

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        StartTracking(); // Iniciar el temporizador al inicio del nivel.
        UpdateScoreText(); // Mostrar el puntaje actual al inicio.
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0)) // Cuando se suelta el clic izquierdo del ratón.
        {
            AddStroke(); // Añadir un golpe.
        }

        if (isTracking)
        {
            // Calcular el tiempo transcurrido.
            elapsedTime = Time.time - startTime;
            timeText.text = $"Time: {elapsedTime:F2}"; // Mostrar el tiempo en la UI.
        }
    }

    private void StartTracking()
    {
        startTime = Time.time; // Registrar el tiempo de inicio.
        isTracking = true; // Activar el temporizador.
        elapsedTime = 0f; // Reiniciar el tiempo transcurrido.
        Debug.Log($"Temporizador iniciado en {levelName}");
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    private void AddStroke()
    {
        currentStrokes++;
        strokeText.text = $"Strokes: {currentStrokes}";
    }

    public void EndHole()
    {
        // Enviar los golpes al GameManager al finalizar el hoyo.
        GameManager.Instance.AddStatsForLevel(levelName, currentStrokes, elapsedTime);
        currentStrokes = 0; // Reiniciar para el siguiente hoyo.
        isTracking = false; // Detener el temporizador.
        Debug.Log($"Tiempo total en {levelName}: {elapsedTime:F2} segundos");

        // Actualizar el puntaje y reiniciar textos en pantalla.
        UpdateScoreText();
        strokeText.text = "Strokes: 0";
        timeText.text = "Time: 0.00";
        StartTracking(); // Iniciar el temporizador de nuevo.
    }

    private void UpdateScoreText()
    {
        if (ScoreManager.Instance != null)
        {
            int currentScore = ScoreManager.Instance.GetCurrentScore(); // Obtener la puntuación del ScoreManager.
            scoreText.text = $"Score: {currentScore}"; // Mostrar la puntuación en la UI.
        }
    }
}
