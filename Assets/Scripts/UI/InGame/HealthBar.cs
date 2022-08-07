using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IHealthDisplayer
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image backGroundImage;

    public void ShowActualHealth(int currentHealth, int maxHealth)
    {
        float currentFill = (float)currentHealth / (float)maxHealth;
        healthBar.fillAmount = currentFill;

        if(currentFill <= 0)
        {
            backGroundImage.color = Color.clear;
        }
    }
}
