using Config;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View.Helpers
{
    public class SmoothnessSettings : MonoBehaviour
    {
        private float _veryHigh = 0.01f;
        
        private Dropdown _dropdown;
        public void Initialize()
        {
            GameConfig.SMOOTHNESS = _veryHigh;
        }
     }
}
