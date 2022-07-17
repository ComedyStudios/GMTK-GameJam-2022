using System;
using TMPro;
using UnityEngine;

namespace scrips.GameMechanics
{
    public class WinPlate : MonoBehaviour
    {
        public TextMeshProUGUI text;
        [Range(0,6)]
        public int rightNumber;

        private void Start()
        {
            if (rightNumber != 0)
            {
                text.text = rightNumber.ToString();
            }
            else text.text = "*";
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
                //accept any number
                else if (rightNumber == 0 &&!GameStateManager.Instance.activeWinPlates.Contains(this) )
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