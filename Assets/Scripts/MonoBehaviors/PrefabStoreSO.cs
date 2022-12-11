using UnityEngine;

namespace ZombieDOTS
{
    [CreateAssetMenu]
    public class PrefabStoreSO : ScriptableObject
    {
        public static PrefabStoreSO Singleton { get; private set; }

        [SerializeField] GameObject zombie;

        public GameObject Zombie => zombie;

        void Awake()
        {
            SetSingleton();
        }

        void OnEnable()
        {
            SetSingleton();
        }

        void SetSingleton()
        {
            if (Singleton && Singleton != this)
            {
                Debug.LogWarning($"There should be only one asset of type {GetType()}.");
                return;
            }

            Singleton = this;
        }
    }
}