using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHourGlass : MonoBehaviour
{
    [SerializeField] private Slider[] _sands;
    [SerializeField] private HourGlassInverted _isHourGlassInverted;

    void OnEnable()
    {
        PlayerAge.HourGlassFlipped = InvertHourGlass;
        PlayerAge.HourGlassUpdated = UpdateHourGlass;
    }

    void OnDisable()
    {
        PlayerAge.HourGlassFlipped = null;
        PlayerAge.HourGlassUpdated = null;
    }
    
    void Setup()
    {
        _isHourGlassInverted = HourGlassInverted.No;
    }

    void UpdateHourGlass(float remainingTimePercentage)
    {
        _sands[0 + (int)_isHourGlassInverted].value = remainingTimePercentage;
        _sands[1 - (int)_isHourGlassInverted].value = 1f - remainingTimePercentage;
    }

    void InvertHourGlass()
    {
        if(_isHourGlassInverted == HourGlassInverted.No)
            _isHourGlassInverted = HourGlassInverted.Yes;
        else
            _isHourGlassInverted = HourGlassInverted.No;
        Debug.Log($"Hourglass flipped! Is hour glass inverted: {_isHourGlassInverted}");
    }
}
