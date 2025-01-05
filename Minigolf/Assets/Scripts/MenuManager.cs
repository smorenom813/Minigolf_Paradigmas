using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Toggle aiToggle; // Casilla de verificación para jugar con IA.

    private void Start()
    {
        aiToggle.isOn = false; // Desmarcar el Toggle al cargar el menú.
    }

    public void OnToggleValueChanged()
    {
        bool isOn = aiToggle.isOn;
        GameManager.Instance.SetPlayWithAI(isOn); // Notificar al GameManager si la IA está activada o no.
        Debug.Log($"Método OnToggleValueChanged llamado. Valor: {isOn}");
    }
}
