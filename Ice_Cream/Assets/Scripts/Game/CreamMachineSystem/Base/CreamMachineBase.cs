﻿using DG.Tweening;
using Game.CreamMachineSystem.Controllers;
using Game.IceCreamSystem.Base;
using Game.View.Helpers;
using UnityEngine;
using Zenject;

namespace Game.CreamMachineSystem.Base
{
    public class CreamMachineBase : MonoBehaviour
    {
        private Transform _chocolateStick;
        private Transform _vanillaStick;
        private Transform _strawberryStick;
        
        #region Controllers

        private PlayerInputController _playerInputController;
        private CreamMachineMovementController _creamMachineMovementController;
        private CreamMachineCreamController _creamMachineCreamController;

        #endregion

        [Inject]
        private void OnInstaller(PlayerInputController playerInputController)
        {
            _playerInputController = playerInputController;
            _creamMachineMovementController = GetComponent<CreamMachineMovementController>(); 
            _creamMachineCreamController = GetComponent<CreamMachineCreamController>(); 

            _chocolateStick = transform.Find("ChocolateStick");
            _vanillaStick = transform.Find("VanillaStick");
            _strawberryStick = transform.Find("StrawberryStick");
        }
        
        public void Initialize()
        {
            _playerInputController.Initialize();
            _creamMachineMovementController.Initialize();
            _creamMachineCreamController.Initialize(_creamMachineMovementController);
            InputEventsSubscriptions();
        }

        private void InputEventsSubscriptions()
        {
            // Moving machine sticks
            
            _playerInputController.SubscribeHoldingEvent(CreamType.CHOCOLATE, () =>
                _chocolateStick.DORotate(Vector3.forward * 45f, 1f)
                );
            _playerInputController.SubscribeReleasingEvent(CreamType.CHOCOLATE, () =>
                _chocolateStick.DORotate(Vector3.zero, 1f)
            );
            
            _playerInputController.SubscribeHoldingEvent(CreamType.VANILLA, () =>
                _vanillaStick.DORotate(Vector3.forward * 45f, 1f)
            );
            _playerInputController.SubscribeReleasingEvent(CreamType.VANILLA, () =>
                _vanillaStick.DORotate(Vector3.zero, 1f)
            );
            
            _playerInputController.SubscribeHoldingEvent(CreamType.STRAWBERRY, () =>
                _strawberryStick.DORotate(Vector3.forward * 45f, 1f)
            );
            _playerInputController.SubscribeReleasingEvent(CreamType.STRAWBERRY, () =>
                _strawberryStick.DORotate(Vector3.zero, 1f)
            );
        }
    }
}
