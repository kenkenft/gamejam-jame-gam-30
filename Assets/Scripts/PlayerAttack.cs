using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    bool m_HitDetect;
    RaycastHit2D m_Hit;
    [SerializeField] private Vector2 _currentDirection = new Vector2(1f, 1f), _currentAttackArea;
    [SerializeField] private float _currentAttackDistance = 0f, _currentAttackDamage = 0f; 
    private List<Vector2> _attackAreas = new List<Vector2>
    {
        new Vector2(1f,1f), new Vector2(2f,2f), new Vector2(5f,5f), new Vector2(1f,1f)
    };

    private List<float> _attackDistances = new List<float>
    {
        2f, 3f, 4f, 1f
    };

    private List<float> _attackDamageValues = new List<float>
    {
        5f, 7f, 20f, 1f
    };
    private ContactFilter2D _dummy;

    [HideInInspector] public static OnSomeEvent SupportAbilityCalled;

    
    public void Attack(Vector2 direction)
    {
        _currentDirection = direction;
        List<RaycastHit2D> listOfHitObjects = new List<RaycastHit2D>();
        int results = Physics2D.BoxCast(gameObject.transform.position, _currentAttackArea, 0f, 
                                        _currentDirection, _dummy.NoFilter(), listOfHitObjects, 
                                        _currentAttackDistance);
            
        if(results > 0) 
        {    
            m_HitDetect = true;
            foreach(RaycastHit2D hit in listOfHitObjects)
            {
                if(hit.collider.gameObject.tag == "Enemy")
                {
                    m_Hit = hit;    
                    hit.collider.gameObject.GetComponent<EnemyCollision>().ReceiveDamage(_currentAttackDamage);
                }
            }
            // ToDo call method that applies damage to hit targets
        }
        else
            m_HitDetect = false;
    }

    public void SetAbilities(int ageState)
    {
        Debug.Log($"New abilities set for ageState: {(AgeState)ageState}");
        // ToDo Set attacks sprite
        _currentAttackArea = _attackAreas[ageState];
        _currentAttackDamage = _attackDamageValues[ageState];
        _currentAttackDistance = _attackDistances[ageState];
        // SupportAbilityCalled = _supportAbilities[ageState];
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (m_HitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, transform.forward * m_Hit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + transform.forward * m_Hit.distance, transform.localScale);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, transform.forward * _currentAttackDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + transform.forward * _currentAttackDistance, transform.localScale);
        }
    }
}
