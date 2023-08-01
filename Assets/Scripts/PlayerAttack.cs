using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Vector2 _currentDirection = new Vector2(1f, 1f);
    private ContactFilter2D _dummy;

    
    public void CallAbility()
    {
         Punch();
    }
    
    void Punch()
    {
        List<RaycastHit2D> listOfHitObjects = new List<RaycastHit2D>();
        // int results = Physics2D.BoxCast(gameObject.transform.position, new Vector2(1f,1f), 0f, currentDirection, ContactFilter2D.NoFilter, listOfHitObjects, 0.5f);
        int results = Physics2D.BoxCast(gameObject.transform.position, new Vector2(1f,1f), 0f, _currentDirection, _dummy.NoFilter(), listOfHitObjects, 2f);
        // ToDo created a ContactFilter2D that only returns a collision if object is on Enemy layer. At the moment, it's hitting the player
            // Or maybe just filter by tag
            
        if (results > 0) 
        {    
            foreach(RaycastHit2D hit in listOfHitObjects)
                Debug.Log(hit.collider.gameObject.name);
            // ToDo call method that applies damage to hit targets
        }
    }
}
