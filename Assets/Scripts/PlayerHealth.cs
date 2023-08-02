using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private FloatingHealthBar _healthBar;
    [SerializeField] private float _maxHealth = 100f, _currentHealth, _lastTimeHit = 0f;
    [SerializeField] public static SendString PlaySFX;
    [HideInInspector] public static OnSomeEvent TriggerEndGame;
    [HideInInspector] public static OnSomeEvent PlayerDied;

    void OnEnable()
    {
        UIManager.StartGameSetUp += SetUp;
    }

    void Disable()
    {
        UIManager.StartGameSetUp -= SetUp;
    }
    
    // void Start()
    // {
    //     SetUp();
    // }

    void SetUp()
    {
        _currentHealth = _maxHealth;
        _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);
        _lastTimeHit = Time.time;
    }

    public void ModifyHealth(float amount)
    {
        if(amount > 0f || (amount < 0f && Time.time -_lastTimeHit > 3f))
        {    
            if(amount < 0f)
            {    
                _lastTimeHit = Time.time;
                PlaySFX?.Invoke("PlayerHit");
            }
            _currentHealth += amount;    // If amount is positive, then it is healing; if amount is negative, then it is damage
            _currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);
            _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);
        }
        // Debug.Log($"amount: {amount}");

        if(_currentHealth <= 0f)
        {
            // Debug.Log("Player is dead");
            PlaySFX?.Invoke("GameOver");
            PlayerDied?.Invoke();
            TriggerEndGame?.Invoke();
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
