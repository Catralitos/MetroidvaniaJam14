using GameManagement;
using Player;
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
                for (int i = 0; i < PlayerEntity.Instance.threeButtonDoorsOpened.Length; i++)
                {
                    if (LevelManager.Instance.threeButtonDoors[i] == this)
                    {
                        PlayerEntity.Instance.threeButtonDoorsOpened[i] = true;
                        break;
                    }
                }
                door.Open();
                if (startsFinalCountdown)
                {
                    LevelManager.Instance.StartFinalCountdown();
                }
            }
        }
    }
}