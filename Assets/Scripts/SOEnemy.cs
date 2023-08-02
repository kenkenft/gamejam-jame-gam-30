using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class SOEnemy : ScriptableObject
{
    public string Name;
    public float Health, PhysicalDamagePercentage, TimeDamagePercentage, Speed;
    public Sprite Body, Face;
    public bool CanDamageTime;
    public int PointValue; 
    
}
