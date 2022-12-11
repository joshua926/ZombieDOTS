using UnityEngine;

namespace ZombieDOTS
{
    [CreateAssetMenu]
    public class SettingsSO : ScriptableObject
    {
        public static SettingsSO Instance { get; private set; }

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
            if (Instance && Instance != this)
            {
                Debug.LogWarning($"There should be only one asset of type {GetType()}.");
                return;
            }

            Instance = this;
        }
    }
}