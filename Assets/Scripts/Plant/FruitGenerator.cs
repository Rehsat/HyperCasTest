using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitGenerator : MonoBehaviour
{
    [SerializeField] private float _secondsToGenerate = 1;
    [SerializeField] private int _generationPerTick = 1;
    
    public Action<int> OnPlantsGenerated;
    
    private int _generatedPlantCount;
    private int _maxPlantGenerate;
    
    public int GeneratedPlantCount
    {
        get => _generatedPlantCount;
        private set
        {
            value = Math.Clamp(value, 0,_maxPlantGenerate );
            
            var generatedCount = value - _generatedPlantCount;
            if (generatedCount > 0)
                OnPlantsGenerated?.Invoke(generatedCount);
            
            _generatedPlantCount = value;
            
            if(_generatedPlantCount < _maxPlantGenerate)
                StartGenerate();
        }
    }
    
    public void Init(int maxPlantGenerate)
    {
        _maxPlantGenerate = maxPlantGenerate;
        StartGenerate();
    }

    public void StartGenerate()
    {
        StopAllCoroutines();
        StartCoroutine(Generate());
    }

    public void TakeFruit()
    {
        GeneratedPlantCount--;
    }

    private IEnumerator Generate()
    {
        yield return new WaitForSeconds(_secondsToGenerate);
        GeneratedPlantCount += _generationPerTick;
    }
}
