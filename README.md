# Ice Cream Game 🍦

## Overview 🎮
Ice Cream Game is a Unity-based simulation where players control an ice cream machine to create multi-layered ice creams using different cream types. The game features a physics-driven cream dispensing system, smooth animations, and interactive UI elements.

![image](https://github.com/Asbaq/Ice_Cream/assets/62818241/93f9c5df-2e0e-452d-a378-1150ef32b980)

## Player Interaction 🔄
The player controls an ice cream machine to dispense different flavors (Chocolate, Vanilla, Strawberry) while following a predefined path.

### Key Features 🔐
- Real-time cream dispensing system.
- Smooth machine movement along a Bezier curve.
- Interactive UI elements for user input.
- Dynamic animations using DOTween.

---

## **Extension Methods (OnurExtensionMethods)**
Provides utility methods for safe event invocation and transformation manipulation.

### Key Features
- SafeInvoke ensures safe invocation of events.
- Clone method allows deep copying of list objects.
- ChangePositionY simplifies modifying Transform positions.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public static class OnurExtensionMethods
    {
        public static void SafeInvoke(this Action source)
        {
            if (source != null) source.Invoke();
        }

        public static void SafeInvoke<T>(this Action<T> source, T value)
        {
            if (source != null) source.Invoke(value);
        }

        public static void ChangePositionY(this Transform thisPosition, float yPosition)
        {
            var tempPos = thisPosition.position;
            tempPos.y = yPosition;
            thisPosition.position = tempPos;
        }
    }
}
```

---

## **Cream Button (CreamButton.cs)**
Handles UI interactions for the ice cream machine buttons.

### Key Features
- Detects button press and release.
- Triggers events for cream dispensing.
- Uses DOTween for UI scaling animations.

```csharp
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Helpers
{
    public class CreamButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Action OnButtonHolding;
        public Action OnButtonReleased;

        private bool _isHolding;
        private Tween _animTween;

        private void Update()
        {
            if (_isHolding)
            {
                OnButtonHolding.SafeInvoke();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isHolding = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isHolding = false;
            OnButtonReleased.SafeInvoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _animTween?.Kill();
            _animTween = transform.DOScale(0.9f, 0.2f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _animTween?.Kill();
            _animTween = transform.DOScale(1f, 0.2f);
        }
    }
}
```

---

## **Singleton Manager (GenericSingleton.cs)**
Manages singleton instances for essential game objects.

### Key Features
- Ensures a single instance of an object exists in the scene.
- Automatically creates an instance if none exists.

```csharp
using UnityEngine;

namespace Helpers
{
    public class GenericSingleton<T> : MonoBehaviour where T : Component
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        instance = obj.AddComponent<T>();
                    }
                }
                return instance;
            }
        }
    }
}
```

---

## **Dependency Injection (GameInstaller.cs)**
Sets up essential game dependencies using Zenject.

### Key Features
- Binds core game objects like IceCreamBase and CreamMachineBase.
- Ensures proper dependency injection for a modular structure.

```csharp
using Game.CreamMachineSystem.Base;
using Game.IceCreamSystem.Base;
using Game.View.Helpers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private IceCreamBase _iceCreamBase;
        [SerializeField]
        private CreamMachineBase _creamMachineBase;
        [SerializeField]
        private PlayerInputController _playerInput;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_creamMachineBase);
            Container.BindInstance(_iceCreamBase);
            Container.BindInstance(_playerInput);
        }
    }
}
```

---

## Conclusion 🎯
Ice Cream Game is a dynamic, physics-driven game that allows players to create multi-layered ice creams. With interactive UI elements, smooth animations, and a structured architecture, it provides an engaging gameplay experience. Let me know if you need any additions or refinements! 🚀

