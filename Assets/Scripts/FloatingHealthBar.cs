using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        _slider.value = currentHealth/maxHealth;
    }
}
