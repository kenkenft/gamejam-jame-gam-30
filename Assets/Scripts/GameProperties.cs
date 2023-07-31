using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnSomeEvent();
public delegate void SendInt(int value);
public delegate void SendFloat(float value);
public delegate void SendString(string value);
public delegate bool ReturnBool();
public delegate int ReturnInt();
public enum AgeState
{
    Young, Middle, Old, Dead 
};

public enum HourGlassInverted
{
    No, Yes
}
public class GameProperties
{
    public static float PlayerSpeed;
    public static WaitForSecondsRealtime timerDelay = new WaitForSecondsRealtime(0.5f);

    // public enum PlayerAge
    // {
    //     Young, Middle, Old, Dead 
    // };
}
