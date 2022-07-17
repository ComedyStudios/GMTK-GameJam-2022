using System;
using UnityEngine;

namespace scrips.GameMechanics
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        [SerializeField]
        protected AudioClip diceSfx;
        
        [SerializeField]
        private AudioSource sourceSfx;

        private void Awake()
        {
            Instance = this;
        }

        public void DiceSfx()
        {
            sourceSfx.clip = diceSfx;
            sourceSfx.Play();
        }
    }
}