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

        private void Start()
        {
            Instance = this;
        }

        private void Update()
        {
            if (activeWinPlates.Count == totalPlates)
            { 
                //TODO: make the next scene load or shit
                Debug.Log("you win");
            }
        }
    }
}