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
                anim.Play("Menu");
                yield return new WaitForSeconds(1f);
                _menuIsOpen = true;
            }
            else
            {
                anim.Play("MenuExit");
                yield return new WaitForSeconds(1f);
                _menuIsOpen = false;
            }
        }
    }
}