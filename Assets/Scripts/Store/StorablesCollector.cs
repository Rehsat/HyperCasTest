using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorablesCollector : MonoBehaviour
{
    
}

public interface IStorablesContainer
{
    public List<Storable> GetStorables(int count);
}