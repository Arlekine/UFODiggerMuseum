using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienRotateAnimation : MonoBehaviour
{
   public event Action OnRotateComplete;
   
   private void CompleteRotate()
   {
      OnRotateComplete?.Invoke();
   }
}
