using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SeanprCore
{
    public static class ComponentExtensions
    {
        public static PlayMakerFSM LocateFSM(this Component self, string fsmName)
            => self.gameObject.LocateFSM(fsmName);
    }
}
