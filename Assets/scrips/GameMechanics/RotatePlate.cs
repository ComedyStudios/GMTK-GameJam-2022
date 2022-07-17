using System;
using System.Collections;
using UnityEngine;

namespace scrips.GameMechanics
{
    public class RotatePlate : MonoBehaviour
    {
        private bool _diceHasBeenRotated = false;
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Dice") && !_diceHasBeenRotated)
            {
                var movementScript = other.gameObject.GetComponent<DiceMovement>();
                if (!movementScript.diceMoving)
                {
                    _diceHasBeenRotated = true;
                    StartCoroutine(RotateDice(movementScript, other.gameObject));
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Dice"))
            {
                _diceHasBeenRotated = false;
            }
        }

        private IEnumerator RotateDice(DiceMovement movementScript, GameObject dice)
        {
            var dices = GameObject.FindGameObjectsWithTag("Dice");
            foreach (var die in dices)
            {
                die.GetComponent<DiceMovement>().diceMoving = true;
            }
            movementScript.diceMoving = true;
            const int totalDeg = 90;
            const int rotateAmount = 2;
            var tempCubeRotation = dice.transform.eulerAngles.y;
            var currentDeg = 0;
            while (totalDeg > currentDeg)
            {
                dice.transform.Rotate(Vector3.up, rotateAmount, Space.World);
                currentDeg += rotateAmount;
                yield return new WaitForSeconds(1f/60);
            }

            dice.transform.eulerAngles = new Vector3(dice.transform.eulerAngles.x, tempCubeRotation+ 90,dice.transform.eulerAngles.z);
            movementScript.diceMoving = false;
            foreach (var die in dices)
            {
                die.GetComponent<DiceMovement>().diceMoving = false;
            }
        }
    }
}