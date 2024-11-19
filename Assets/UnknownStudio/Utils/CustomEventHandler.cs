using UnityEngine;
using UnityEngine.Events;

namespace UnknownStudio.Utils
{
    [AddComponentMenu("Unknown Studio/Utils - Custom Event Handler")]
    public class CustomEventHandler : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnAwake;
        [SerializeField] private UnityEvent OnStart;

        void Awake()
        {
            OnAwake?.Invoke();
        }

        void Start()
        {
            OnStart?.Invoke();
        }
    }
}
