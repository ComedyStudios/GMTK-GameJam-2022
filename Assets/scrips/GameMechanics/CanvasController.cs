using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace scrips.GameMechanics
{
    public class CanvasController : MonoBehaviour
    {
        public InputAction menuAction;
        public Animator anim;
        public ArduinoTest arduino;
        private bool _menuIsOpen = false;

        private bool _buttonPressed = false;
        
        private void OnEnable()
        {
            menuAction.Enable();
        }

        private void OnDisable()
        {
            menuAction.Disable();
        }

        private void Start()
        {
            menuAction.performed += _ => StartCoroutine(OpenMenu());
        }

        private void Update()
        {
            if (arduino.inputs.r && !_buttonPressed)
            {
                _buttonPressed = true;
                Debug.Log("le");
                StartCoroutine(OpenMenu());
            }

            if (!arduino.inputs.r && _buttonPressed)
            {
                _buttonPressed = false;
            }
        }

        private IEnumerator OpenMenu()
        {
            if (!_menuIsOpen)
            {
                var dices = GameObject.FindGameObjectsWithTag("Dice");
                foreach (var die in dices)
                {
                    die.GetComponent<DiceMovement>().diceMoving = true;
                }
                anim.Play("Menu");
                yield return new WaitForSeconds(1f);
                _menuIsOpen = true;
            }
            else
            {
                var dices = GameObject.FindGameObjectsWithTag("Dice");
                foreach (var die in dices)
                {
                    die.GetComponent<DiceMovement>().diceMoving = false;
                }
                anim.Play("MenuExit");
                yield return new WaitForSeconds(1f);
                _menuIsOpen = false;
            }
        }
    }
}