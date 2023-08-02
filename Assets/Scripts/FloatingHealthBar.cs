using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Camera _camera;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;

    public void SetUp()
    {
        // _camera = FindObjectOfType<Camera>();
        _camera = Camera.main;
        _canvas = GetComponentInParent<Canvas>();
        _canvas.worldCamera = _camera;
    }

    void Update()
    {
        if(gameObject.activeSelf)
        {
            transform.rotation = _camera.transform.rotation;
            transform.position = _target.position + _offset;
        }
    }


    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        _slider.value = currentHealth/maxHealth;
    }

    public void ToggleHealthBar(bool state)
    {
        gameObject.SetActive(state);
    }

    public void SetOffset(Vector3 offset)
    {
        _offset = offset;
    }
}
