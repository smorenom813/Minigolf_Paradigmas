using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.Instance.ShouldPlayWithAI()) // Verificar si se debe activar la IA.
        {
            AIPlayer aiPlayer = FindObjectOfType<AIPlayer>();
            if (aiPlayer != null)
            {
                aiPlayer.StartAI(); // Activar la IA.
            }
        }
    }
}

