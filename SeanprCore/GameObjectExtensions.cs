using UnityEngine;

// ReSharper disable file UnusedMember.Global

namespace SeanprCore
{
    public static class GameObjectExtensions
    {
        public static PlayMakerFSM LocateFSM(this GameObject self, string fsmName)
        {
            return FSMUtility.LocateFSM(self, fsmName);
        }

        public static bool IsChildOf(this GameObject self, GameObject maybeParent)
        {
            Transform parent = self.transform.parent;
            while (parent != null)
            {
                if (parent.gameObject == maybeParent)
                {
                    return true;
                }

                parent = parent.transform.parent;
            }

            return false;
        }

        // No component restriction because of interfaces. Unity can probably be trusted to handle type checking
        public static T GetComponentInSelfChildOrParent<T>(this GameObject self)
        {
            T component = self.GetComponent<T>();

            if (component != null)
            {
                return component;
            }

            component = self.GetComponentInChildren<T>();

            return component != null ? component : self.GetComponentInParent<T>();
        }
    }
}