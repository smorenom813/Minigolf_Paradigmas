using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; } // Instancia �nica del singleton.

    private int score = 0; // Puntuaci�n actual.

    void Awake()
    {
        // Asegurar que solo exista una instancia del ScoreManager.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Elimina instancias duplicadas.
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // No destruir este objeto al cambiar de escena.
    }

    public void AddPoint()
    {
        score++;
        Debug.Log("Puntuaci�n actual: " + score);
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
        Debug.Log("Puntuaci�n reiniciada.");
    }
}