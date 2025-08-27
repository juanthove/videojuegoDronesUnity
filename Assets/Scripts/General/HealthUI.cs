using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{

    public Slider healthSlider3D;
    
    public void Start3DSlider(float maxValue){
        healthSlider3D.maxValue = maxValue;
        healthSlider3D.value = maxValue;
    }

    public void Update3DSlider(float value){
        healthSlider3D.value = value;
        
    }
}
