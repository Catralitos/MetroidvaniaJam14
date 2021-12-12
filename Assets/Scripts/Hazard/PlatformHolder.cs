using System.Collections.Generic;
using UnityEngine;

namespace Hazard
{
    public class PlatformHolder : Openable
    {
        public List<GameObject> platforms;

        private void Start()
        {
            Close();
        }

        public override void Open()
        {
            foreach (GameObject platform in platforms)
            {
                platform.SetActive(true);
            }
        }
    
        public override void Close()
        {
            foreach (GameObject platform in platforms)
            {
                platform.SetActive(false);
            }
        }
    }
}
