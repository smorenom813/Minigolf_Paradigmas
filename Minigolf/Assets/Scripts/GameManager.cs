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
} 