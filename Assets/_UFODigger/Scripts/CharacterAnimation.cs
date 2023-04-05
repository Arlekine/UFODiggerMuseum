using System;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public event Action OnWinComplete;
    
    private void CompleteWinAnimation()
    {
        OnWinComplete?.Invoke();
    }
}

