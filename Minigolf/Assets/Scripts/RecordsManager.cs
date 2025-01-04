using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecordsUI : MonoBehaviour
{
    public TMP_Text recordsText; // Texto UI para mostrar los records.


    private void Start()
    {
        ShowRecords(); // Mostrar los records al cargar la escena.
    }

    public void ShowRecords()
    {
        if (recordsText == null)
        {
            Debug.LogWarning("No se ha asignado el Text para mostrar los records.");
            return;
        }

        recordsText.text = "";

        // Obtener los registros del GameManager.
        Dictionary<string, int> records = GameManager.Instance.GetAllStrokesByLevel();
        Debug.Log(records["Level 1"]);

        Dictionary<string, float> times = GameManager.Instance.GetAllTimesByLevel();
        // Mostrar cada nivel y su puntuación.
        foreach (var entry in records)
        {
            recordsText.text += $"{entry.Key}: {entry.Value} golpes en {times[entry.Key]} segundos\n\n" ;
        }
    }

}