using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UISlidersHandler : MonoBehaviour
{
    public static Slider healthSlider{get;private set;}
    public static Slider enduranceSlider{get;private set;}
    
    private void Awake() {
        healthSlider = GameObject.FindGameObjectWithTag("SliderHP").GetComponent<Slider>();
        enduranceSlider = GameObject.FindGameObjectWithTag("SliderEndurance").GetComponent<Slider>();
    }

     public static void EnableHealthSlider(bool isEnabled){
        if(isEnabled && UISlidersHandler.healthSlider != null)UISlidersHandler.healthSlider.transform.localScale = new Vector3(1,1,1);;
        if(!isEnabled)UISlidersHandler.healthSlider.transform.localScale = new Vector3(1,0,1);;
    }
    public static void EnableEnduranceSlider(bool isEnabled){
    if(UISlidersHandler.enduranceSlider == null)UISlidersHandler.enduranceSlider = GameObject.FindGameObjectWithTag("SliderEndurance").GetComponent<Slider>();
    if(UISlidersHandler.enduranceSlider != null){
        if(isEnabled)UISlidersHandler.enduranceSlider.transform.localScale = new Vector3(1,1,1);
        if(!isEnabled)UISlidersHandler.enduranceSlider.transform.localScale = new Vector3(1,0,1);;
      }
    }
     public static void SetSliderValue(float value,float maxValue,Slider slider){
        slider.value = value;
        slider.maxValue = maxValue;
    }
}
