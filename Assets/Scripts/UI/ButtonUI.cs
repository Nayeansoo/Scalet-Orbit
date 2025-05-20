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

    public Texture selectedTexture;
    public Texture pressTexture;
    public Texture normalTexture;

    private void Start()
    {

    }

    public void SelectButton(int val)
    {
        images[val].color = selectedColor;
        images[val].texture = selectedTexture;
    }

    public void PressButton(int val)
    {
        images[val].color = PressColor;
        images[val].texture = pressTexture;
    }

    public void NormalButton(int val)
    {
        images[val].color = normalColor;
        images[val].texture = normalTexture;
    }
}