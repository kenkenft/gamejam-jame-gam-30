using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private FloatingHealthBar _healthBar;
    [SerializeField] private float maxHealth = 100f, currentHealth;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;

    void SetUp()
    {
        currentHealth = maxHealth;
        _healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    void ModifyHealth(float amount)
    {
        currentHealth += amount;    // If amount is positive, then it is healing; if amount is negative, then it is damage
        _healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    void Update()
    {
        transform.rotation = _camera.transform.rotation;
        transform.position = _target.position + _offset; 
    }
}
