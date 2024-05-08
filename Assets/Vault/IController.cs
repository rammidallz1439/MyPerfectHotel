using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vault
{
    public interface IController
    {
        void Initial();
        void RegisterListener();
        void UnRegisterListener();
        void Release();
        
    }
}

