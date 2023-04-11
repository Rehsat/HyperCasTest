using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="FruitData", menuName ="PlantsData/FruitData")]
public class FruitData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _cost;
    [SerializeField] private int _growthTime;
    [SerializeField] private Sprite _sprite;

    public int Id => GetHashCode();
    
    public string Name => _name;

    public int Cost => _cost;

    public int GrowthTime => _growthTime;

    public Sprite Sprite => _sprite;
    
}
