using Game.CreamMachineSystem.Base;
using Game.IceCreamSystem.Base;
using Game.IceCreamSystem.Managers;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        private IceCreamBase _iceCreamBase;
        private CreamMachineBase _creamMachine;
        
        [Inject]
        private void OnInstaller(IceCreamBase iceCreamBase,CreamMachineBase creamMachineBase)
        {
            _iceCreamBase = iceCreamBase;
            _creamMachine = creamMachineBase;
        }

        private void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            _iceCreamBase.Initialize();
            _creamMachine.Initialize();
            
            CreamPiecePoolManager.Instance.Initialize();
        }
    }
}
