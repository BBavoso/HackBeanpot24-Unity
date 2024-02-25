using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailRatioBar : MonoBehaviour
{
   public Slider FailSlider;

        public void SetFailSlider(float ratio)
    {
        FailSlider.value = ratio;
    }
}
