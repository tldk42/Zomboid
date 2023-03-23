using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Core.Inspector
{
    [System.Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<TKey> Keys = new List<TKey>();
        [SerializeField] private List<TValue> Values = new List<TValue>();

        public void OnBeforeSerialize()
        {
            Keys.Clear();
            Values.Clear();
            foreach (var pair in this)
            {
                Keys.Add(pair.Key);
                Values.Add(pair.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            Clear();
            if (Keys.Count != Values.Count)
            {
                throw new SystemException($"키{Keys.Count} : 값{Values.Count} | 쌍이 맞지 않음");
            }

            for (var i = 0; i < Keys.Count; ++i)
            {
                Add(Keys[i], Values[i]);
            }
        }
    }
}