using UnityEngine;

namespace Extensions
{
    public static class LayerMaskExtensions
    {
        public static bool HasLayer(this LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }
    }
}