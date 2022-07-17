using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace scrips.GameMechanics
{
    public class GameStateManager : MonoBehaviour
    {
        public int totalPlates;
        //[HideInInspector]
        public List<WinPlate> activeWinPlates = new List<WinPlate>();
        public static GameStateManager Instance;
        public Animator gameUIAnimator;

        private void Start()
        {
            Instance = this;
        }

        private void Update()
        {
            if (activeWinPlates.Count == totalPlates)
            {
                gameUIAnimator.gameObject.GetComponent<CanvasController>().enabled = false;
                gameUIAnimator.Play("levelFinished");
                Debug.Log("you win");
            }
        }
    }
}