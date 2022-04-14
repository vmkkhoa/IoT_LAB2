using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class Toggle_Switch : MonoBehaviour
{
    public int switchState = 1;
    public GameObject switchBtn;
    public Button button;
    public RawImage backround;

    public void OnSwitchButtonClicked()
    {
        switchBtn.transform.DOLocalMoveX(-switchBtn.transform.localPosition.x, 0.2f);
        switchState = Math.Sign(-switchBtn.transform.localPosition.x);
       // Debug.Log(switchState);
        
    }
    public void OnChangeValue()
    {
        ColorBlock cb = button.colors;
        if (switchState == -1)
        {
            cb.normalColor = Color.yellow;
            cb.highlightedColor = Color.yellow;
            cb.pressedColor = Color.yellow;
            cb.selectedColor = Color.yellow;
        }
        else if(switchState == 1)
        {
            cb.normalColor = Color.yellow;
            cb.highlightedColor = Color.yellow;
            cb.pressedColor = Color.yellow;
            cb.selectedColor = Color.yellow;
        }
        button.colors = cb;
    }
 

    

}