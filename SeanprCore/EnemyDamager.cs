using System;
using System.Collections.Generic;
using System.Linq;
using GlobalEnums;
using Modding;
using UnityEngine;

namespace SeanprCore
{
    public class EnemyDamager : MonoBehaviour
    {
        public int damage = 5;
        public float cooldown = 1f;
        public AttackTypes type = AttackTypes.Generic;
        public bool gainSoul;
        public bool bypassIframes;
        public bool bypassStun;

        private List<ColliderInfo> hitReponders = new List<ColliderInfo>();

        private void Update()
        {
            for (int i = 0; i < hitReponders.Count; i++)
            {
                if (hitReponders[i].responder == null)
                {
                    hitReponders.RemoveAt(i);
                    i--;
                    continue;
                }

                hitReponders[i].cooldown -= Time.deltaTime;

                if (!hitReponders[i].touching)
                {
                    continue;
                }

                if (hitReponders[i].cooldown <= 0)
                {
                    hitReponders[i].cooldown = cooldown;
                    DoDamage(hitReponders[i]);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (!enabled || collider.gameObject.layer != (int)PhysLayers.ENEMIES)
            {
                return;
            }

            IHitResponder hit = collider.gameObject.GetComponentInSelfChildOrParent<IHitResponder>();

            ColliderInfo colInfo = hitReponders.FirstOrDefault(info => info.responder == hit);

            if (colInfo == null)
            {
                colInfo = new ColliderInfo()
                {
                    responder = hit,
                    cooldown = 0
                };

                hitReponders.Add(colInfo);
            }

            colInfo.touching = true;
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (!enabled || collider.gameObject.layer != (int)PhysLayers.ENEMIES)
            {
                return;
            }

            IHitResponder hit = collider.gameObject.GetComponentInSelfChildOrParent<IHitResponder>();
            if (hit == null)
            {
                return;
            }

            foreach (ColliderInfo info in hitReponders)
            {
                if (info.responder == hit)
                {
                    info.touching = false;
                    break;
                }
            }
        }

        private void OnDisable()
        {
            hitReponders.Clear();
        }

        private void DoDamage(ColliderInfo target)
        {
            if (damage <= 0)
            {
                return;
            }

            Component c = target.responder as Component;
            if (c == null)
            {
                return;
            }

            FSMUtility.SendEventToGameObject(c.gameObject, "TAKE DAMAGE");

            HitInstance hit = new HitInstance()
            {
                Source = gameObject,
                AttackType = type,
                CircleDirection = false,
                DamageDealt = damage,
                IgnoreInvulnerable = false,
                MagnitudeMultiplier = 0,
                MoveAngle = 0,
                MoveDirection = false,
                Multiplier = 1,
                SpecialType = SpecialTypes.None,
                IsExtraDamage = false
            };

            if (target.responder is HealthManager hm)
            {
                float oldIframes = 0f;
                if (bypassIframes)
                {
                    oldIframes = ReflectionHelper.GetAttr<HealthManager, float>(hm, "evasionByHitRemaining");
                    ReflectionHelper.SetAttr(hm, "evasionByHitRemaining", 0f);
                }

                PlayMakerFSM stunFSM = null;
                if (bypassStun)
                {
                    stunFSM = ReflectionHelper.GetAttr<HealthManager, PlayMakerFSM>(hm, "stunControlFSM");
                    ReflectionHelper.SetAttr(hm, "stunControlFSM", (PlayMakerFSM)null);
                }

                hm.Hit(hit);

                if (bypassIframes)
                {
                    ReflectionHelper.SetAttr(hm, "evasionByHitRemaining", oldIframes);
                }

                if (bypassStun)
                {
                    ReflectionHelper.SetAttr(hm, "stunControlFSM", stunFSM);
                }
            }
            else
            {
                target.responder.Hit(hit);
            }

            if (gainSoul)
            {
                Ref.Hero.SoulGain();
            }
        }

        private class ColliderInfo
        {
            public IHitResponder responder;
            public bool touching;
            public float cooldown;
        }
    }
}
