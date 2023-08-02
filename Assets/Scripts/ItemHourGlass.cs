using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHourGlass : MonoBehaviour
{
    float _spawnTime, _currentTime;
    [SerializeField] private SpriteRenderer _itemSpriteRenderer;
    public int DespawnTimerCurrent = 0, DespawnTimerMax = 3;

    // void Start()
    // {
    //     SetUpCoin();
    // }
    
    public void SetUpItem()
    {
        _itemSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        DespawnTimerCurrent = 0;
    }

    public void ToggleSprite(bool state)
    {
        _itemSpriteRenderer.enabled = state;
    }

    public void UpdateDespawnTimer()
    {
        if(DespawnTimerCurrent < DespawnTimerMax)
        {    
            DespawnTimerCurrent++;
            Color tempColor = _itemSpriteRenderer.color;
            tempColor.a = 1f - ((float)DespawnTimerCurrent/(float)DespawnTimerMax);
            _itemSpriteRenderer.color = tempColor;
        }
        else
            this.gameObject.SetActive(false);
    }

    public void PickedUp()
    {
        this.gameObject.SetActive(false);
    }
}
