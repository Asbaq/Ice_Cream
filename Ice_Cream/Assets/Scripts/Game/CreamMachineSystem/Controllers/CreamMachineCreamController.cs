using BezierSolution;
using Config;
using DG.Tweening;
using Game.IceCreamSystem.Base;
using Game.IceCreamSystem.Managers;
using Game.View;
using Game.View.Helpers;
using UnityEngine;
using Zenject;

namespace Game.CreamMachineSystem.Controllers
{
    public class CreamMachineCreamController : MonoBehaviour
    {
        private IceCreamBase _currentIceCream;
        private int _currentLayer;
        
        private CreamMachineMovementController _creamMachineMovementController;

        [Inject]
        public void OnInstaller(IceCreamBase iceBase)
        {

            _currentIceCream = iceBase;
            
        }
        
        public void Initialize(CreamMachineMovementController creamMachineMovementController)
        {
            _currentLayer = 0;

            _creamMachineMovementController = creamMachineMovementController;
            
            _creamMachineMovementController.OnPathCompleted += UpdateLayer;
            _creamMachineMovementController.OnCreamGenerated += GeneratedCream;
            
            UpdateLayer();
        }

        private void GeneratedCream(CreamType creamType, BezierSpline bezier, Transform iceCreamFilter)
        {
            var piece = CreamPiecePoolManager.Instance.GetCreamAvailableCream(creamType);
            piece.transform.eulerAngles = new Vector3(-90,0,0);
            piece.transform.position = iceCreamFilter.position;
            piece.transform.DOMove(bezier.GetPoint(_creamMachineMovementController.NormalizedT), 3f);
            var look = Quaternion.LookRotation(bezier.GetTangent(_creamMachineMovementController.NormalizedT));
            piece.transform.DORotateQuaternion(look, 3f);

        }
        
        private void UpdateLayer()
        {
            if(_currentIceCream == null)
                return;
            
            var creamSpline = _currentIceCream.CreamSplineManager.GetCreamByLayer(_currentLayer++);

           // CheckLevelStatus();
            if (creamSpline != null)
            {
                _creamMachineMovementController.NormalizedT = 0;
                _creamMachineMovementController.SetBezierSpline(creamSpline.BezierSpline);
            }
        }
    }
}
