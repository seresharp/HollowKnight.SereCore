using System.Collections.Generic;
using System.Linq;
using GlobalEnums;
using JetBrains.Annotations;
using Modding;
using UnityEngine;

namespace SereCore
{
    [PublicAPI]
    public class EnemyDamager : MonoBehaviour
    {
        private readonly List<ColliderInfo> _hitReponders = new List<ColliderInfo>();
        public bool BypassIframes;
        public bool BypassStun;
        public float Cooldown = 1f;
        public int Damage = 5;
        public bool GainSoul;
        public AttackTypes Type = AttackTypes.Generic;

        private void Update()
        {
            for (int i = 0; i < _hitReponders.Count; i++)
            {
                if (_hitReponders[i].Responder == null)
                {
                    _hitReponders.RemoveAt(i);
                    i--;
                    continue;
                }

                _hitReponders[i].Cooldown -= Time.deltaTime;

                if (!_hitReponders[i].Touching)
                {
                    continue;
                }

                if (_hitReponders[i].Cooldown <= 0)
                {
                    _hitReponders[i].Cooldown = Cooldown;
                    DoDamage(_hitReponders[i]);
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

            ColliderInfo colInfo = _hitReponders.FirstOrDefault(info => info.Responder == hit);

            if (colInfo == null)
            {
                colInfo = new ColliderInfo
                {
                    Responder = hit,
                    Cooldown = 0
                };

                _hitReponders.Add(colInfo);
            }

            colInfo.Touching = true;
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

            foreach (ColliderInfo info in _hitReponders)
            {
                if (info.Responder != hit)
                {
                    continue;
                }

                info.Touching = false;
                break;
            }
        }

        private void OnDisable()
        {
            _hitReponders.Clear();
        }

        private void DoDamage(ColliderInfo target)
        {
            if (Damage <= 0)
            {
                return;
            }

            Component c = target.Responder as Component;
            if (c == null)
            {
                return;
            }

            FSMUtility.SendEventToGameObject(c.gameObject, "TAKE DAMAGE");

            HitInstance hit = new HitInstance
            {
                Source = gameObject,
                AttackType = Type,
                CircleDirection = false,
                DamageDealt = Damage,
                IgnoreInvulnerable = false,
                MagnitudeMultiplier = 0,
                MoveAngle = 0,
                MoveDirection = false,
                Multiplier = 1,
                SpecialType = SpecialTypes.None,
                IsExtraDamage = false
            };

            if (target.Responder is HealthManager hm)
            {
                float oldIframes = 0f;
                if (BypassIframes)
                {
                    oldIframes = ReflectionHelper.GetAttr<HealthManager, float>(hm, "evasionByHitRemaining");
                    ReflectionHelper.SetAttr(hm, "evasionByHitRemaining", 0f);
                }

                PlayMakerFSM stunFSM = null;
                if (BypassStun)
                {
                    stunFSM = ReflectionHelper.GetAttr<HealthManager, PlayMakerFSM>(hm, "stunControlFSM");
                    ReflectionHelper.SetAttr(hm, "stunControlFSM", (PlayMakerFSM)null);
                }

                hm.Hit(hit);

                if (BypassIframes)
                {
                    ReflectionHelper.SetAttr(hm, "evasionByHitRemaining", oldIframes);
                }

                if (BypassStun)
                {
                    ReflectionHelper.SetAttr(hm, "stunControlFSM", stunFSM);
                }
            }
            else
            {
                target.Responder.Hit(hit);
            }

            if (GainSoul)
            {
                Ref.Hero.SoulGain();
            }
        }

        private class ColliderInfo
        {
            public float Cooldown;
            public IHitResponder Responder;
            public bool Touching;
        }
    }
}