using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Sprite heart;
    [SerializeField] private UnityEngine.UI.Image[] healthDisplay;

    public void UpdateHealthDisplay(int currentHealth)
    {
        for (int i = 0; i < healthDisplay.Length; i++)
        {
            if (i < currentHealth)
            {
                healthDisplay[i].enabled = true; // Shows heart
            }
            else
            {
                healthDisplay[i].enabled = false; // Hides heart
            }
        }
    }
}
