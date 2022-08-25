using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControllerCursor : MonoBehaviour
{
    [Flags]
    public enum MouseEventFlags
    {
        LeftDown = 0x00000002,
        LeftUp = 0x00000004,
        MiddleDown = 0x00000020,
        MiddleUp = 0x00000040,
        Move = 0x00000001,
        Absolute = 0x00008000,
        RightDown = 0x00000008,
        RightUp = 0x00000010
    }
    
    [SerializeField] private ArduinoTest arduino;
    private Vector2Int mousePosition;
    private bool _buttonIsPressed;

    // Start is called before the first frame update
    void Start()
    {
        mousePosition = new Vector2Int(500, -500);
        
    }

    // Update is called once per frame
    void Update()
    {
        setMousePosition();
        if (arduino.inputs.b1 && !_buttonIsPressed)
        {
            mouse_event((int)MouseEventFlags.LeftDown, mousePosition.x, mousePosition.y, 0, 0);
            _buttonIsPressed = true;
            Debug.Log("button has been pressed");
        }
        else if(!arduino.inputs.b1 &&_buttonIsPressed)
        {
            mouse_event((int)MouseEventFlags.LeftUp, mousePosition.x, mousePosition.y, 0, 0);
            _buttonIsPressed = false;
        }
    }
    
    private void setMousePosition()
    {
        var input = arduino.inputs.stickL * 10;
        mousePosition += Vector2Int.FloorToInt(input);
        SetCursorPos(mousePosition.x, -mousePosition.y);
    }
    
    [DllImport("user32.dll")]
    private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);
}
