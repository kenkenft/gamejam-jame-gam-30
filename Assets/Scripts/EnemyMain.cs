using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain : MonoBehaviour
{
    private float _currentHealth, _maxHealth, _physicalDamagePercentage, _timeDamagePercentage, _speed;
    [SerializeField] private SpriteRenderer _body, _face;
    public SOEnemy EnemyProperties;

    [SerializeField] private Rigidbody2D _rig;
    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private FloatingHealthBar _healthBar;

    [SerializeField] public static SendString PlaySFX;

    void Start()
    {
        SetUp();
    }

    void SetUp()
    {
        ToggleObjectComponents(true);
        

        _currentHealth = EnemyProperties.Health;
        _maxHealth = EnemyProperties.Health;
        _physicalDamagePercentage = EnemyProperties.PhysicalDamagePercentage;
        _timeDamagePercentage = EnemyProperties.TimeDamagePercentage;
        _speed = GameProperties.PlayerSpeed * EnemyProperties.Speed;
        _body.sprite = EnemyProperties.Body;
        _face.sprite = EnemyProperties.Face;
        _collider.radius = _body.sprite.bounds.extents[0];
        
        Vector2 spriteSize = _body.bounds.size;
        Vector3 _maskY = new Vector3(0,spriteSize.y / 2f, 0);
        Vector3 offset = new Vector3(0f, 1, 0f) + _maskY;
        _healthBar.SetOffset(offset);
    }

    public float[] GetDamage()
    {
        float[] damageValues = {_physicalDamagePercentage, _timeDamagePercentage};
        return damageValues;
    }

    public void ReceiveDamage(float damage)
    {
        _currentHealth -= damage;
        Debug.Log($"{gameObject.name} received {damage} damage. {_currentHealth}/ {_maxHealth} left");
        _currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);
        _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);

        if(_currentHealth <= 0f)
        {
            Debug.Log($"{gameObject.name} is dead");
            PlaySFX?.Invoke("EnemyDead");
            ToggleObjectComponents(false);
            // Play death animation;
        }
        else
        {
            PlaySFX?.Invoke("EnemyHit");
        }

    }

    void ToggleObjectComponents(bool state)
    {
        _rig.isKinematic = state;
        _collider.enabled = state;
        _body.enabled = state;
        _face.enabled = state;
        _healthBar.ToggleHealthBar(state);
    }
}
