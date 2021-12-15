using GameManagement;
using Player;
using UnityEditor;
using UnityEngine;

namespace Hazard
{
    public class TripleButton : MonoBehaviour
    {
        public ThirdOfButton button1;
        public ThirdOfButton button2;
        public ThirdOfButton button3;

        public bool startsFinalCountdown = false;

        [SerializeField] public Openable door;

        public float hitTimeframe;
        public bool pressed = false;

        private void Update()
        {
            if (button1.pressed && button2.pressed && button3.pressed)
            {
                pressed = true;
                PlayerEntity.Instance.threeButtonDoorsOpened[
                    ArrayUtility.IndexOf(LevelManager.Instance.threeButtonDoors, this)] = true;
                door.Open();
                if (startsFinalCountdown)
                {
                    LevelManager.Instance.StartFinalCountdown();
                }
            }
        }
    }
}