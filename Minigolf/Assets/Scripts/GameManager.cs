using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI Elements")]
    private Dictionary<string, int> strokesByLevel = new Dictionary<string, int>(); // Diccionario de golpes por nivel.
    private Dictionary<string, float> timeByLevel = new Dictionary<string, float>();

    private bool playWithAI = false; // Indica si se debe activar la IA.

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // No destruir al cambiar de escena.
    }

    public void AddStatsForLevel(string levelName, int strokes, float time)
    {
        if (strokesByLevel.ContainsKey(levelName))
        {
            if (strokesByLevel[levelName] > strokes)
            {
                strokesByLevel[levelName] = strokes;
                timeByLevel[levelName] = time;
            }
            else if (strokesByLevel[levelName] == strokes) {

                if (timeByLevel[levelName] > time)
                {

                    timeByLevel[levelName] = time;
                }
            }
        }
        else
        {
            strokesByLevel[levelName] = strokes;
            timeByLevel[levelName] = time;
        }
    }

    public Dictionary<string, int> GetAllStrokesByLevel()
    {
        return strokesByLevel; // Devolver todos los registros de golpes por nivel.
    }
    public Dictionary<string, float> GetAllTimesByLevel()
    {
        return timeByLevel; // Devolver todos los registros de golpes por nivel.
    }

    public void SetPlayWithAI(bool value)
    {
        playWithAI = value; // Guardar si la IA debe estar activa.
    }

    public bool ShouldPlayWithAI()
    {
        return playWithAI; // Devolver si la IA debe estar activa.
    }
    public void StopAI()
    {
        AIPlayer aiPlayer = FindObjectOfType<AIPlayer>();
        if (aiPlayer != null)
        {
            aiPlayer.StopAI(); // Llamar al método de detener la IA.
            SetPlayWithAI(false); // Desactivar la IA en el GameManager.
            Debug.Log("La IA ha sido detenida desde el GameManager.");
        }
    }
} 