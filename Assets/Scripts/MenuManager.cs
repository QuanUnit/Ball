using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    private GameObject[] _buttons;
    private Animator _pauseMenuAnimation;
    private GameObject _pauseButton;

    private void Awake()
    {
        _buttons = GameObject.FindGameObjectsWithTag("Buttons");

        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            _pauseMenuAnimation = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<Animator>();
            _pauseButton = GameObject.FindGameObjectWithTag("PauseButton");
        }
    }

    public void PlayButtonPressed()
    {
        _panel.GetComponent<Animation>().Play("CloseMenuAnim");
        foreach (var button in _buttons)
        {
            button.GetComponent<Button>().enabled = false;
        }
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitButtonPressed()
    {
        Application.Quit();
    }
    public void PauseButtonPressed()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        playerController.enabled = false;
        playerController.UnShowArrow();
        _pauseMenuAnimation.Play("PauseMenuDownAnim");
        _pauseButton.SetActive(false);
        Time.timeScale = 0f;
    }
    public void ContinueButtonPressed()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        playerController.enabled = true;
        _pauseMenuAnimation.Play("PauseMenuUpAnim");
        _pauseButton.SetActive(true);
        Time.timeScale = 1;
    }
    public void BackToMainMenuButtonPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void ShowPauseButton()
    {
        _pauseButton.SetActive(true);
    }
    public void UnShowPauseButton()
    {
        _pauseButton.SetActive(false);
    }
}
