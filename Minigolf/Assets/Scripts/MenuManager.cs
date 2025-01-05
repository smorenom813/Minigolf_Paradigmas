using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Toggle aiToggle; // Casilla de verificaci�n para jugar con IA.

    private void Start()
    {
        aiToggle.isOn = false; // Desmarcar el Toggle al cargar el men�.
    }

    public void OnToggleValueChanged()
    {
        bool isOn = aiToggle.isOn;
        GameManager.Instance.SetPlayWithAI(isOn); // Notificar al GameManager si la IA est� activada o no.
        Debug.Log($"M�todo OnToggleValueChanged llamado. Valor: {isOn}");
    }
}
