using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using scrips.GameMechanics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class DiceMovement : MonoBehaviour
{
    [SerializeField] private ArduinoTest arduino;
    
    public InputAction movementActionX;
    public InputAction movementActionY;
    public InputAction resetLevelAction;
    public float speed = 300;

    public bool diceMoving = false;

    private bool _leftButtonPressed;

    private void OnEnable()
    {
        movementActionX.Enable();
        movementActionY.Enable();
        resetLevelAction.Enable();
    }

    private void OnDisable()
    {
        movementActionX.Disable();
        movementActionY.Disable();
        resetLevelAction.Disable();
    }

    private void Start()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.x, transform.localScale.z);
        var numberManager = GetComponent<DiceNumberManager>();
        movementActionX.performed += context =>
        {
            var value = context.ReadValue<float>();
            StartCoroutine(Roll(new Vector3(Convert.ToInt32(value), 0, 0)));
        };
        movementActionY.performed += context =>
        {
            var value = context.ReadValue<float>();
            StartCoroutine(Roll(new Vector3( 0,0, -Convert.ToInt32(value))));
        };
        resetLevelAction.performed += _ => resetLevel();
    }
    
    private void Update()
    {
        var arduinoInput = arduino.inputs.stickR;
        if (Math.Abs(arduinoInput.x)> Math.Abs(arduinoInput.y))
        {
            StartCoroutine(Roll(new Vector3(Convert.ToInt32(arduinoInput.x), 0, 0)));
        }
        else if (Math.Abs(arduinoInput.x)< Math.Abs(arduinoInput.y))
        {
            StartCoroutine(Roll(new Vector3(0, 0, Convert.ToInt32(arduinoInput.y))));
        }

        if (arduino.inputs.l && !_leftButtonPressed)
        {
            _leftButtonPressed = true;
            resetLevel();
        }
        if(arduino.inputs.l == false && _leftButtonPressed)
        {
            _leftButtonPressed = false;
        }
    }

    private void resetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        arduino.ClosePort();
    }

    private IEnumerator Roll(Vector3 movementVector)
    {
        float rotation = (int)(Camera.main.gameObject.transform.eulerAngles.y - 35);
        float temp = rotation / 90;
        rotation = Mathf.RoundToInt(temp) * 90;
        
        movementVector = Quaternion.Euler(0, rotation, 0) * movementVector;
        var ray = new Ray(transform.position, movementVector);
        
        if (!diceMoving)
        {
            if (Physics.Raycast(ray, out var hit, transform.localScale.x))
            {
                if (hit.collider.CompareTag("Wall"))
                {
                    yield break;
                }

                if (hit.collider.CompareTag("Dice"))
                {
                    if (Physics.Raycast(hit.collider.transform.position,movementVector, out var h, transform.localScale.x))
                    {
                        if (h.collider.CompareTag("Wall"))
                        {
                            yield break;
                        }
                    }
                    if (Physics.Raycast(hit.collider.transform.position + movementVector, Vector3.down, out hit ,1f, LayerMask.NameToLayer("Player")))
                    {
                        if (!hit.collider.CompareTag("floor"))
                        {
                            yield break;
                        }
                    }
                    else
                    {
                        yield break;
                    }
                }
            }
            if (Physics.Raycast(transform.position + movementVector, Vector3.down, out hit ,1f, LayerMask.NameToLayer("Player")))
            {
                if (!hit.collider.CompareTag("floor"))
                {
                    yield break;
                }
            }
            else
            {
                yield break;
            }
            diceMoving = true;
            SoundManager.Instance.DiceSfx();
            float remainingAngle = 90;
            Vector3 rotationCenter = transform.position + ( movementVector/2 + Vector3.down/2)* transform.localScale.x;
            Vector3 rotationAxis = Vector3.Cross(Vector3.up, movementVector);
            while (remainingAngle > 0) {
                float rotationAngle = Mathf.Min (Time.deltaTime * speed, remainingAngle);
                transform.RotateAround(rotationCenter, rotationAxis, rotationAngle);
                remainingAngle -= rotationAngle;
                yield return null;
            }
            diceMoving = false;
        }
    }
}
