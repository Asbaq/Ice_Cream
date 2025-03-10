﻿using System.Collections.Generic;
using System.Linq;
using Game.IceCreamSystem.Base;
using Helpers;
using UnityEngine;

namespace Game.IceCreamSystem.Managers
{
    public class CreamPiecePoolManager : GenericSingleton<CreamPiecePoolManager>
    {
        private const string CREAM_PATH = "CreamPiece";
        
        [SerializeField] private Material _chocolateMaterial;
        [SerializeField] private Material _vanillaMaterial;
        [SerializeField] private Material _StrawberryMaterial;
        
        private List<CreamPiece> _creamPieces;
        private CreamPiece _creamPiece;
        
        public void Initialize()
        {
            _creamPieces = GetComponentsInChildren<CreamPiece>(true)?.ToList() ?? new List<CreamPiece>();
            DeactivateWholePool();
            _creamPiece = Resources.Load<CreamPiece>(CREAM_PATH);
        }

        public CreamPiece GetCreamAvailableCream(CreamType creamType)
        {
            var cream = _creamPieces?.FirstOrDefault(x => !x.IsActive);
            if (cream == null)
            {
                cream = Instantiate(_creamPiece, transform);
                cream.CreamType = creamType;
                _creamPieces?.Add(cream);
            }
            
            cream.GetComponentInChildren<MeshRenderer>().material = creamType == CreamType.CHOCOLATE ? 
                _chocolateMaterial : creamType == CreamType.VANILLA ? _vanillaMaterial : _StrawberryMaterial;
            cream.Activate();
            return cream;
        }

        public void DeactivateWholePool()
        {
            if(_creamPieces.Count <= 0)
                return;
            
            foreach (var creamPiece in _creamPieces)
            {
                creamPiece.Deactivate();
                creamPiece.CreamType = CreamType.NONE;
            }
        }
    }
}
