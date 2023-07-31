using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHourGlass : MonoBehaviour
{
    [SerializeField] private Slider[] _sands;
    [SerializeField] private HourGlassInverted _isHourGlassInverted;
    [SerializeField] private Canvas _hourGlass;
    [SerializeField] private Image[] _sandsImages;

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

    void start()
    {
        Setup();
    }
    
    void Setup()
    {
        _isHourGlassInverted = HourGlassInverted.No;
        _hourGlass.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        SetFillDirections();
    }

    void UpdateHourGlass(float remainingTimePercentage)
    {
        _sands[0 + (int)_isHourGlassInverted].value = remainingTimePercentage;
        _sands[1 - (int)_isHourGlassInverted].value = 1f - remainingTimePercentage;
    }

    void InvertHourGlass()
    {
        StartCoroutine("RotateHourGlass");

        if(_isHourGlassInverted == HourGlassInverted.No)
        {    
            _hourGlass.transform.eulerAngles = new Vector3(0f,0f,0f);
            _isHourGlassInverted = HourGlassInverted.Yes;
        }
        else
        {
            _hourGlass.transform.eulerAngles = new Vector3(0f,0f,180f);
            _isHourGlassInverted = HourGlassInverted.No;
        }

        SetFillDirections();        
        Debug.Log($"Hourglass flipped! Is hour glass inverted: {_isHourGlassInverted}");
    }

    public void SetFillDirections()
    {
        _sands[0 + (int)_isHourGlassInverted].SetDirection(Slider.Direction.TopToBottom, false);
        _sands[1 - (int)_isHourGlassInverted].SetDirection(Slider.Direction.BottomToTop, false);
        _sandsImages[0 + (int)_isHourGlassInverted].fillOrigin = (int)Image.OriginVertical.Top;
        _sandsImages[1 - (int)_isHourGlassInverted].fillOrigin = (int)Image.OriginVertical.Bottom;
    }

    public IEnumerator RotateHourGlass()
    {
        float startTime = Time.time, currentTime = Time.time;
        while(currentTime - startTime < 0.5f)
        {
            yield return null;
            // if(!CheckIsPaused.Invoke())
            // {
                currentTime = Time.time;
                _hourGlass.transform.RotateAround(_hourGlass.transform.position, Vector3.forward,  359 * Time.deltaTime);
            // }
        }
        StopCoroutine("RotateHourGlass");
        Debug.Log("RotateHourGlass coroutine stopped");
        yield return null;
    }
}
