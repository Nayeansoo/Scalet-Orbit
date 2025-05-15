using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour
{
    public RawImage[] images;
    public Color selectedColor;
    public Color PressColor;
    public Color normalColor;

    private void Start()
    {

    }

    public void SelectButton(int val)
    {
        images[val].color = selectedColor;
    }

    public void PressButton(int val)
    {
        images[val].color = PressColor;
    }

    public void NormalButton(int val)
    {
        images[val].color = normalColor;
    }
}