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

        private bool _menuIsOpen = false;
        
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