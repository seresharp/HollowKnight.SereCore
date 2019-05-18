using UnityEngine;

// ReSharper disable file UnusedMember.Global

namespace SeanprCore
{
    public static class ComponentExtensions
    {
        public static PlayMakerFSM LocateFSM(this Component self, string fsmName)
        {
            return self.gameObject.LocateFSM(fsmName);
        }
    }
}