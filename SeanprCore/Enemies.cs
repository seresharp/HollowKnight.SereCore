using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ReSharper disable file UnusedMember.Global

namespace SeanprCore
{
    public static class Enemies
    {
        private static bool _tracking;
        private static List<HealthManager> _enemies;

        public static IEnumerable<HealthManager> All
        {
            get
            {
                if (!_tracking)
                {
                    throw new InvalidOperationException($"{nameof(Enemies)}.{nameof(All)} called while not tracking");
                }

                _enemies.RemoveAll(enemy => enemy == null);

                // This check is required because some extremely cursed enemies only have colliders situationally
                return _enemies.Where(enemy => enemy.GetComponent<Collider2D>());
            }
        }

        public static void BeginTracking()
        {
            On.HealthManager.OnEnable -= TrackEnemy;
            On.HealthManager.OnEnable += TrackEnemy;

            _enemies = new List<HealthManager>();
            _tracking = true;
        }

        public static void EndTracking()
        {
            On.HealthManager.OnEnable -= TrackEnemy;

            _enemies = null;
            _tracking = false;
        }

        public static HealthManager GetClosest(Func<HealthManager, bool> isValid = null,
            float maxDist = float.PositiveInfinity, Vector3? pos = null)
        {
            if (pos == null)
            {
                if (Ref.Hero == null)
                {
                    return null;
                }

                pos = Ref.Hero.transform.position;
            }

            float dist = float.PositiveInfinity;
            HealthManager closest = null;

            foreach (HealthManager hm in All)
            {
                float hmDist = Vector3.Distance(pos.Value, hm.transform.position);
                if (hmDist < dist && hmDist < maxDist && (isValid == null || isValid(hm)))
                {
                    dist = hmDist;
                    closest = hm;
                }
            }

            return closest;
        }

        private static void TrackEnemy(On.HealthManager.orig_OnEnable orig, HealthManager self)
        {
            // I think there could be duplicates in the case of an hm getting disabled/re-enabled without this check
            if (!_enemies.Contains(self))
            {
                _enemies.Add(self);
            }
        }
    }
}