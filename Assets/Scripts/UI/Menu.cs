using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Menu 
{
   public GameObject canvas;
   public MenuType type;
}
public enum MenuType{
    UI,SkeletonUI,PauseMenu,LoadMenu,OptionsMenu,SaveMenu
}