using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] private EnemyMain _enemyMain;

    public float[] GetDamage()
    {
        return _enemyMain.GetDamage();
    }

    public void ReceiveDamage(float damage)
    {
        _enemyMain.ReceiveDamage(damage);
    }
}
