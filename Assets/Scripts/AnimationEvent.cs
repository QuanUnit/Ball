using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEvent : MonoBehaviour
{
    public static bool CanPlay;
    private void Start()
    {
        CanPlay = false;
    }
    public void StartPlay()
    {
        CanPlay = true;
    }
    public void SwitchScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
