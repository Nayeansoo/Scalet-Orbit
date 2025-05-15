using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScenes : MonoBehaviour
{
    public void ClickStart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
