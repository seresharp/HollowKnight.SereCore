using System;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SeanprCore
{
    [PublicAPI]
    [Obsolete("This class is obsolete. Use SereCore.BaseSettings instead.", true)]
    public class BaseSettings : SereCore.BaseSettings
    {
        private readonly Dictionary<SerializationEvent, SereCore.BaseSettings.SerializationEvent> baseDelegateDict
            = new Dictionary<SerializationEvent, SereCore.BaseSettings.SerializationEvent>();

        protected new event SerializationEvent BeforeSerialize
        {
            add => base.BeforeSerialize += GetBaseDelegate(value);
            remove => base.BeforeSerialize -= GetBaseDelegate(value);
        }

        protected new event SerializationEvent AfterDeserialize
        {
            add => base.AfterDeserialize += GetBaseDelegate(value);
            remove => base.AfterDeserialize -= GetBaseDelegate(value);
        }

        protected new delegate void SerializationEvent();

        private SereCore.BaseSettings.SerializationEvent GetBaseDelegate(SerializationEvent e)
        {
            if (baseDelegateDict.TryGetValue(e, out SereCore.BaseSettings.SerializationEvent baseDelegate))
            {
                return baseDelegate;
            }

            Delegate d = Delegate.CreateDelegate(typeof(SereCore.BaseSettings.SerializationEvent), e.Target, e.Method);
            baseDelegate = (SereCore.BaseSettings.SerializationEvent)d;
            baseDelegateDict[e] = baseDelegate;

            return baseDelegate;
        }
    }

    [PublicAPI]
    [Obsolete("This class is obsolete. Use SereCore.ComponentExtensions instead.", true)]
    public static class ComponentExtensions
    {
        [Obsolete("This method is obsolete. Use SereCore.ComponentExtensions.LocateFSM instead.", true)]
        public static PlayMakerFSM LocateFSM(this Component self, string fsmName)
            => SereCore.ComponentExtensions.LocateFSM(self, fsmName);
    }

    [PublicAPI]
    [Obsolete("This class is obsolete. Use SereCore.DictionaryExtensions instead", true)]
    public static class DictionaryExtensions
    {
        [Obsolete("This method is obsolete. Use SereCore.DictionaryExtensions.AsTuples instead.", true)]
        public static IEnumerable<(TKey, TVal)> AsTuples<TKey, TVal>(this IDictionary<TKey, TVal> self)
            => SereCore.DictionaryExtensions.AsTuples(self);

        [Obsolete("This method is obsolete. Use SereCore.DictionaryExtensions.Deconstruct instead.", true)]
        public static void Deconstruct<TKey, TVal>(this KeyValuePair<TKey, TVal> self, out TKey key, out TVal val)
            => SereCore.DictionaryExtensions.Deconstruct(self, out key, out val);
    }

    [PublicAPI]
    [Obsolete("This class is obsolete. Use SereCore.Enemies instead", true)]
    public static class Enemies
    {
        [Obsolete("This property is obsolete. Use SereCore.Enemies.All instead", true)]
        public static IEnumerable<HealthManager> All
            => SereCore.Enemies.All;

        [Obsolete("This method is obsolete. Use SereCore.Enemies.BeginTracking instead", true)]
        public static void BeginTracking()
            => SereCore.Enemies.BeginTracking();

        [Obsolete("This method is obsolete. Use SereCore.Enemies.EndTracking instead", true)]
        public static void EndTracking()
            => SereCore.Enemies.EndTracking();

        [Obsolete("This method is obsolete. Use SereCore.Enemies.GetClosest instead", true)]
        public static HealthManager GetClosest(Func<HealthManager, bool> isValid = null,
            float maxDist = float.PositiveInfinity, Vector3? pos = null)
            => SereCore.Enemies.GetClosest(isValid, maxDist, pos);
    }

    [PublicAPI]
    [Obsolete("This class is obsolete. Use SereCore.EnemyDamager instead.", true)]
    public class EnemyDamager : SereCore.EnemyDamager { }

    [PublicAPI]
    [Obsolete("This class is obsolete. Use SereCore.Fonts instead.", true)]
    public static class Fonts
    {
        [Obsolete("This method is obsolete. Use SereCore.Fonts.Get instead.", true)]
        public static Font Get(string name)
            => SereCore.Fonts.Get(name);
    }

    [PublicAPI]
    [Obsolete("This class is obsolete. Use SereCore.GameObjectExtensions instead.", true)]
    public static class GameObjectExtensions
    {
        [Obsolete("This method is obsolete. Use SereCore.GameObjectExtensions.LocateFSM instead.", true)]
        public static PlayMakerFSM LocateFSM(this GameObject self, string fsmName)
            => SereCore.GameObjectExtensions.LocateFSM(self, fsmName);

        [Obsolete("This method is obsolete. Use SereCore.GameObjectExtensions.IsChildOf instead.", true)]
        public static bool IsChildOf(this GameObject self, GameObject maybeParent)
            => SereCore.GameObjectExtensions.IsChildOf(self, maybeParent);

        [Obsolete("This method is obsolete. Use SereCore.GameObjectExtensions.GetComponentInSelfChildOrParent instead.", true)]
        public static T GetComponentInSelfChildOrParent<T>(this GameObject self)
            => SereCore.GameObjectExtensions.GetComponentInSelfChildOrParent<T>(self);
    }

    [PublicAPI]
    [Obsolete("This class is obsolete. Use SereCore.ModCommonExtensions instead.", true)]
    public static class ModCommonExtensions
    {
        [Obsolete("This method is obsolete. Use SereCore.ModCommonExtensions.FindGameObjectInChildren instead.", true)]
        public static GameObject FindGameObjectInChildren(this GameObject gameObject, string name)
            => SereCore.ModCommonExtensions.FindGameObjectInChildren(gameObject, name);

        [Obsolete("This method is obsolete. Use SereCore.ModCommonExtensions.FindGameObject instead.", true)]
        public static GameObject FindGameObject(this Scene scene, string name)
            => SereCore.ModCommonExtensions.FindGameObject(scene, name);
    }

    [PublicAPI]
    [Obsolete("This class is obsolete. Use SereCore.PlayMakerExtensions instead.", true)]
    public static class PlayMakerExtensions
    {
        [Obsolete("This method is obsolete. Use SereCore.PlayMakerExtensions.AddState instead.", true)]
        public static void AddState(this PlayMakerFSM self, FsmState state)
            => SereCore.PlayMakerExtensions.AddState(self, state);

        [Obsolete("This method is obsolete. Use SereCore.PlayMakerExtensions.GetState instead.", true)]
        public static FsmState GetState(this PlayMakerFSM self, string name)
            => SereCore.PlayMakerExtensions.GetState(self, name);

        [Obsolete("This method is obsolete. Use SereCore.PlayMakerExtensions.RemoveActionsOfType instead.", true)]
        public static void RemoveActionsOfType<T>(this FsmState self) where T : FsmStateAction
            => SereCore.PlayMakerExtensions.RemoveActionsOfType<T>(self);

        [Obsolete("This method is obsolete. Use SereCore.PlayMakerExtensions.GetActionOfType instead.", true)]
        public static T GetActionOfType<T>(this FsmState self) where T : FsmStateAction
            => SereCore.PlayMakerExtensions.GetActionOfType<T>(self);

        [Obsolete("This method is obsolete. Use SereCore.PlayMakerExtensions.GetActionsOfType instead.", true)]
        public static T[] GetActionsOfType<T>(this FsmState self) where T : FsmStateAction
            => SereCore.PlayMakerExtensions.GetActionsOfType<T>(self);

        [Obsolete("This method is obsolete. Use SereCore.PlayMakerExtensions.ClearTransitions instead.", true)]
        public static void ClearTransitions(this FsmState self)
            => SereCore.PlayMakerExtensions.ClearTransitions(self);

        [Obsolete("This method is obsolete. Use SereCore.PlayMakerExtensions.RemoveTransitionsTo instead.", true)]
        public static void RemoveTransitionsTo(this FsmState self, string toState)
            => SereCore.PlayMakerExtensions.RemoveTransitionsTo(self, toState);

        [Obsolete("This method is obsolete. Use SereCore.PlayMakerExtensions.AddTransition instead.", true)]
        public static void AddTransition(this FsmState self, string eventName, string toState)
            => SereCore.PlayMakerExtensions.AddTransition(self, eventName, toState);

        [Obsolete("This method is obsolete. Use SereCore.PlayMakerExtensions.AddFirstAction instead.", true)]
        public static void AddFirstAction(this FsmState self, FsmStateAction action)
            => SereCore.PlayMakerExtensions.AddFirstAction(self, action);

        [Obsolete("This method is obsolete. Use SereCore.PlayMakerExtensions.AddAction instead.", true)]
        public static void AddAction(this FsmState self, FsmStateAction action)
            => SereCore.PlayMakerExtensions.AddAction(self, action);
    }

    [PublicAPI]
    [Obsolete("This class is obsolete. Use SereCore.Ref instead.", true)]
    public static class Ref
    {
        [Obsolete("This property is obsolete. Use SereCore.Ref.PD instead.", true)]
        public static PlayerData PD
            => SereCore.Ref.PD;

        [Obsolete("This property is obsolete. Use SereCore.Ref.Hero instead.", true)]
        public static HeroController Hero
            => SereCore.Ref.Hero;

        [Obsolete("This property is obsolete. Use SereCore.Ref.HeroRigidbody instead.", true)]
        public static Rigidbody2D HeroRigidbody
            => SereCore.Ref.HeroRigidbody;

        [Obsolete("This property is obsolete. Use SereCore.Ref.HeroNailFSM instead.", true)]
        public static PlayMakerFSM HeroNailFSM
            => SereCore.Ref.HeroNailFSM;

        [Obsolete("This property is obsolete. Use SereCore.Ref.GM instead.", true)]
        public static GameManager GM
            => SereCore.Ref.GM;

        [Obsolete("This property is obsolete. Use SereCore.Ref.Input instead.", true)]
        public static InputHandler Input
            => SereCore.Ref.Input;

        [Obsolete("This property is obsolete. Use SereCore.Ref.UI instead.", true)]
        public static UIManager UI
            => SereCore.Ref.UI;
    }

    [PublicAPI]
    [Obsolete("This class is obsolete. Use SereCore.ResourceHelper instead.", true)]
    public static class ResourceHelper
    {
        [Obsolete("This method is obsolete. Use SereCore.ResourceHelper.GetSprites instead.", true)]
        public static Dictionary<string, Sprite> GetSprites(string prefix = null)
            => SereCore.ResourceHelper.GetSprites(prefix);
    }

    [PublicAPI]
    [Obsolete("This class is obsolete. Use SereCore.SceneNames instead.", true)]
    public abstract class SceneNames : SereCore.SceneNames { }

    [PublicAPI]
    [Obsolete("This class is obsolete. Use SereCore.StringExtensions instead.", true)]
    public static class StringExtensions
    {
        [Obsolete("This method is obsolete. Use SereCore.StringExtensions.TryToEnum instead.", true)]
        public static bool TryToEnum<T>(this string self, out T val) where T : Enum
            => SereCore.StringExtensions.TryToEnum(self, out val);
    }

    [PublicAPI]
    [Obsolete("This class is obsolete. Use SereCore.TextureExtensions instead.", true)]
    public static class TextureExtensions
    {
        [Obsolete("This method is obsolete. Use SereCore.TextureExtensions.SaveToFile instead.", true)]
        public static void SaveToFile(this Texture2D self, string path)
            => SereCore.TextureExtensions.SaveToFile(self, path);
    }
}
