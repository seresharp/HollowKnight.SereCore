using System;
using System.Reflection;
using Modding;
using UnityEngine;
using static SeanprCore.LogHelper;

namespace SeanprCore
{
    [Serializable]
    public class BaseSettings : ModSettings, ISerializationCallbackReceiver
    {
        private readonly FieldInfo[] _fields;

        protected BaseSettings()
        {
            _fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                                          BindingFlags.DeclaredOnly);
        }

        public void OnBeforeSerialize()
        {
            try
            {
                BeforeSerialize?.Invoke();
            }
            catch (Exception e)
            {
                LogError(e);
            }

            foreach (FieldInfo field in _fields)
            {
                SetString(JsonUtility.ToJson(field.GetValue(this)), field.Name);
            }
        }

        public void OnAfterDeserialize()
        {
            foreach (FieldInfo field in _fields)
            {
                field.SetValue(this, JsonUtility.FromJson(GetString(null, field.Name), field.FieldType));
            }

            try
            {
                AfterDeserialize?.Invoke();
            }
            catch (Exception e)
            {
                LogError(e);
            }
        }

        protected event SerializationEvent BeforeSerialize;
        protected event SerializationEvent AfterDeserialize;

        protected delegate void SerializationEvent();
    }
}