using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private FloatingHealthBar _healthBar;
    [SerializeField] private float _maxHealth = 100f, _currentHealth;

    void Start()
    {
        SetUp();
    }

    void SetUp()
    {
        _currentHealth = _maxHealth;
        _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);
    }

    public void ModifyHealth(float amount)
    {
        Debug.Log($"amount: {amount}");
        _currentHealth += amount;    // If amount is positive, then it is healing; if amount is negative, then it is damage
        _currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);
        _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);

        if(_currentHealth <= 0f)
        {
            Debug.Log("Player is dead");
            // ToDo Trigger endgame
        }
    }


    public float GetMaxHealth()
    {
        return _maxHealth;
    }

    public void SetOffset(Vector3 offset)
    {
        _healthBar.SetOffset(offset);
    }
}
