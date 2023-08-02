using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain : MonoBehaviour
{
    private int _pointValue;
    private float _currentHealth, _maxHealth, _physicalDamagePercentage, _timeDamagePercentage, _speed;
    [SerializeField] private SpriteRenderer _body, _face;
    public SOEnemy EnemyProperties;
    [SerializeField] private Transform _player; 

    [SerializeField] private Rigidbody2D _rig;
    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private FloatingHealthBar _healthBar;
    private Vector2 _moveDirection;

    [SerializeField] public static SendString PlaySFX;
    [SerializeField] public static SendInt EnemyDied;
    [SerializeField] public static SendVector3 ItemDropped;

    // void Start()
    // {
    //     SetUp();
    // }

    

    public void SetUp()
    {
        ToggleObjectComponents(true);
        

        _currentHealth = EnemyProperties.Health;
        _maxHealth = EnemyProperties.Health;
        _physicalDamagePercentage = EnemyProperties.PhysicalDamagePercentage;
        _timeDamagePercentage = EnemyProperties.TimeDamagePercentage;
        _speed = 9f *  EnemyProperties.Speed;  //GameProperties.PlayerSpeed was 0, therefore enemySpeed was being set to zero.
        _body.sprite = EnemyProperties.Body;
        _face.sprite = EnemyProperties.Face;
        _collider.radius = _body.sprite.bounds.extents[0];
        _pointValue = EnemyProperties.PointValue;
        
        Vector2 spriteSize = _body.bounds.size;
        Vector3 _maskY = new Vector3(0,spriteSize.y / 2f, 0);
        Vector3 offset = new Vector3(0f, 1, 0f) + _maskY;
        _healthBar.SetOffset(offset);

        _player = FindObjectOfType<PlayerMain>().gameObject.transform;
        _rig = GetComponent<Rigidbody2D>();
        _healthBar.SetUp();
        _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);
    }

    void Update()
    {
        if(_currentHealth > 0f)
        {
            Vector2 direction = (_player.position - transform.position).normalized;
            _moveDirection = direction;
            _rig.velocity = new Vector2(_moveDirection.x * _speed, _moveDirection.y *_speed);
            // Debug.Log($"Move Direction: {_moveDirection}.Velocity: {_rig.velocity}");
        }
    }

    public float[] GetDamage()
    {
        float[] damageValues = {_physicalDamagePercentage, _timeDamagePercentage};
        return damageValues;
    }

    public void ReceiveDamage(float damage)
    {
        _currentHealth -= damage;
        // Debug.Log($"{gameObject.name} received {damage} damage. {_currentHealth}/ {_maxHealth} left");
        _currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);
        _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);

        if(_currentHealth <= 0f)
        {
            // Debug.Log($"{gameObject.name} is dead");
            EnemyDied?.Invoke(_pointValue);
            PlaySFX?.Invoke("EnemyDead");
            SpawnItem();
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
        // _rig.isKinematic = state;
        _collider.enabled = state;
        _body.enabled = state;
        _face.enabled = state;
        _healthBar.ToggleHealthBar(state);
    }

    void SpawnItem()
    {
        int randomInt = Random.Range(1,100);
        if(randomInt > 80)
            ItemDropped?.Invoke(gameObject.transform.position);
    }
}
