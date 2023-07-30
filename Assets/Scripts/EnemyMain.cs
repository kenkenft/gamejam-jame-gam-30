using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain : MonoBehaviour
{
    private float _health, _physicalDamagePercentage, _timeDamagePercentage, _speed;
    [SerializeField] private SpriteRenderer _body, _face;
    public SOEnemy EnemyProperties;

    void Start()
    {
        SetUp();
    }
    
    void SetUp()
    {
        _health = EnemyProperties.Health;
        _physicalDamagePercentage = EnemyProperties.PhysicalDamagePercentage;
        _timeDamagePercentage = EnemyProperties.TimeDamagePercentage;
        _speed = GameProperties.PlayerSpeed * EnemyProperties.Speed;
        _body.sprite = EnemyProperties.Body;
        _face.sprite = EnemyProperties.Face;
    }
}
