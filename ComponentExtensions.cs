using JetBrains.Annotations;
using UnityEngine;

namespace SereCore
{
    [PublicAPI]
    public static class ComponentExtensions
    {
        public static PlayMakerFSM LocateFSM(this Component self, string fsmName)
        {
            return self.gameObject.LocateFSM(fsmName);
        }
    }
}
