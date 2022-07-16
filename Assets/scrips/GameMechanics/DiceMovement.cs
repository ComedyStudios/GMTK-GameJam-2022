using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using scrips.GameMechanics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class DiceMovement : MonoBehaviour
{
    public InputAction movementActionX;
    public InputAction movementActionY;
    public float speed = 300;

    private bool _isMoving = false;

    private void OnEnable()
    {
        movementActionX.Enable();
        movementActionY.Enable();
    }

    private void OnDisable()
    {
        movementActionX.Disable();
        movementActionY.Disable();
    }

    private void Start()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.x, transform.localScale.z);
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
    }

    private void Update()
    {
        if (!_isMoving)
        {
            Debug.Log(DiceNumberManager.Instance.GetNumber());
        }
    }

    private IEnumerator Roll(Vector3 movementVector)
    {
        var ray = new Ray(transform.position, movementVector);
        RaycastHit hit;
        
        if (!_isMoving)
        {
            if (Physics.Raycast(ray, out hit, transform.localScale.x))
            {
                if (hit.collider.CompareTag("Wall"))
                {
                    yield break;
                }
            }
            _isMoving = true;
            float remainingAngle = 90;
            Vector3 rotationCenter = transform.position + ( movementVector/2 + Vector3.down/2)* transform.localScale.x;
            Vector3 rotationAxis = Vector3.Cross(Vector3.up, movementVector);
            while (remainingAngle > 0) {
                float rotationAngle = Mathf.Min (Time.deltaTime * speed, remainingAngle);
                transform.RotateAround(rotationCenter, rotationAxis, rotationAngle);
                remainingAngle -= rotationAngle;
                yield return null;
            }
            _isMoving = false;
        }
    }
}
