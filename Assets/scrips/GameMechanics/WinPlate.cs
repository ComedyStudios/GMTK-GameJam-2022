using System;
using TMPro;
using UnityEngine;

namespace scrips.GameMechanics
{
    public class WinPlate : MonoBehaviour
    {
        public TextMeshProUGUI text;
        [Range(1,6)]
        public int rightNumber;

        private void Start()
        {
            text.text = rightNumber.ToString();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Dice"))
            {
                var number = other.gameObject.GetComponent<DiceNumberManager>().GetNumber();
                Debug.Log(number);
                if (number == rightNumber && !GameStateManager.Instance.activeWinPlates.Contains(this))
                {
                   GameStateManager.Instance.activeWinPlates.Add(this);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Dice"))
            {
                GameStateManager.Instance.activeWinPlates.Remove(this);
            }
        }
    }
}