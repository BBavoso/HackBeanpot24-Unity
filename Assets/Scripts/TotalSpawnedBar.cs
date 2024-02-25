using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalSpawnedBar : MonoBehaviour
{
    public Slider SpawnedSlider;

        public void SetSpawnedSlider(float spawned)
    {
        SpawnedSlider.value = spawned;
    }
}
