using System;
using System.Collections;
using UnityEngine;

namespace QuickThrow.Utils
{
    public class CoroutineRunner : MonoBehaviour
    {
        public static event Action OnUpdate;
        private static CoroutineRunner _instance;

        public static Coroutine Run(IEnumerator routine)
        {
            if (_instance == null)
            {
                var go = new GameObject("QuickThrowCoroutineRunner");
                GameObject.DontDestroyOnLoad(go);
                _instance = go.AddComponent<CoroutineRunner>();
            }
            return _instance.StartCoroutine(routine);
        }  
        private void Update()
        {
            OnUpdate?.Invoke();
        }
    }
}