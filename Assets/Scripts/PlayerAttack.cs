using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Vector2 _currentDirection = new Vector2(1f, 1f), _attackArea;
    [SerializeField] private float _attackDistance = 0f; 
    private ContactFilter2D _dummy;

    
    public void CallAbility()
    {
         Punch();
    }
    
    void Punch()
    {
        List<RaycastHit2D> listOfHitObjects = new List<RaycastHit2D>();
        int results = Physics2D.BoxCast(gameObject.transform.position, new Vector2(1f,1f), 0f, _currentDirection, _dummy.NoFilter(), listOfHitObjects, 2f);
            
        if(results > 0) 
        {    
            foreach(RaycastHit2D hit in listOfHitObjects)
            {
                if(hit.collider.gameObject.tag == "Enemy")
                {
                    hit.collider.gameObject.GetComponent<EnemyCollision>().ReceiveDamage(5f);
                }
            }
            // ToDo call method that applies damage to hit targets
        }
    }

    public void SetAbilities(int ageState)
    {
        Debug.Log($"New abilities set for ageState: {(AgeState)ageState}");
        //
        //
    }
}
