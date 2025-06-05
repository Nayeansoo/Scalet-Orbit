using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManualUI : MonoBehaviour
{

    public RawImage[] images;
    public Color selectedColor;
    public Color PressColor;
    public Color normalColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
