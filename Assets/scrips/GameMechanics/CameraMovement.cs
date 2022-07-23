using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace scrips.GameMechanics
{
    public class CameraMovement : MonoBehaviour
    {
        public InputAction cameraMovementAction;

        private bool _isMoving = false; 

        private void OnEnable()
        {
            cameraMovementAction.Enable();
        }

        private void OnDisable()
        {
            cameraMovementAction.Disable();
        }

        private void Start()
        {
            cameraMovementAction.performed += context => StartCoroutine(RotateCamera(context.ReadValue<float>()));
        }

        private IEnumerator RotateCamera(float value)
        {
            if (!_isMoving)
            {
                _isMoving = true;
                const int totalDeg = 90;
                const float rotateAmount = 1f;
                var currentDeg = 0f;
                while (totalDeg > currentDeg)
                {
                    transform.Rotate(Vector3.up, rotateAmount *value, Space.World);
                    currentDeg += rotateAmount;
                    yield return new WaitForSeconds(1f/160);
                }
                _isMoving = false;
            }
        }
    }
}