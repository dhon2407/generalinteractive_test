using System;
using UnityEngine;

namespace Helper
{
    public class PoolableMonobehaviour : MonoBehaviour
    {
        public event Action<PoolableMonobehaviour> OnPoolableObjectDisable;
        public virtual void OnDisable() => OnPoolableObjectDisable?.Invoke(this);
    }
}