using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private FloatingHealthBar _healthBar;
    [SerializeField] private float _maxHealth = 100f, _currentHealth;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;

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

    void Update()
    {
        transform.rotation = _camera.transform.rotation;
        transform.position = _target.position + _offset; 
    }

    public float GetMaxHealth()
    {
        return _maxHealth;
    }
}
