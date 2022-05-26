using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsController : MonoBehaviour
{
    public int NumberLevel = 0;
    public int ThisScene;

    private Vector3 CameraPosition;

    [SerializeField] private List<GameObject> _playerIntarfaceAllLevels;
    [SerializeField] private List<GameObject> _finishPointsAllLevels;
    [SerializeField] private List<GameObject> _HPControllersAllLevels;

    private void Start()
    {
        GameObject.Find("Panel").GetComponent<Animation>().Play("StartScene");
        ThisScene = SceneManager.GetActiveScene().buildIndex;
    }
    public void SwitchLevel()
    {
        FindObjectOfType<MenuManager>().UnShowPauseButton();
        NumberLevel++;
        GameObject.Find($"FinishPoint_{NumberLevel - 1}").SetActive(false);
        if  (NumberLevel >= 3)
        {
            GameObject.Find("Panel").GetComponent<Animation>().Play("EndScene");
        }
        else
        {
            LevelOff(NumberLevel - 1);
            StartCoroutine(UpMoveCamera());
        }
    }
    IEnumerator UpMoveCamera()
    {
        for(int i = 0; i < 50; i++)
        {
            CameraPosition = GameObject.Find("Main Camera").transform.position;
            CameraPosition.y += 0.210f;
            GameObject.Find("Main Camera").transform.position = CameraPosition;
            yield return new WaitForSeconds(0.01f);
        }
        foreach (var ball in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Destroy(ball);       
        }
        LevelOn(NumberLevel);
        FindObjectOfType<MenuManager>().ShowPauseButton();
    }
    public IEnumerator DownMoveCamera()
    {
        for (int i = 0; i < (100 * NumberLevel)/2; i++)
        {
            CameraPosition = GameObject.Find("Main Camera").transform.position;
            CameraPosition.y -= 0.210f;
            GameObject.Find("Main Camera").transform.position = CameraPosition;
            yield return new WaitForSeconds(0.01f);
        }
        foreach (var ball in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Destroy(ball);
        }
        FindObjectOfType<MenuManager>().ShowPauseButton();
        LevelOff(NumberLevel);
        NumberLevel = 0;
        LevelOn(NumberLevel);
        LevelUpdate();
    }
    private void LevelOff(int numLevel)
    {
        for (int i = 0; i < _playerIntarfaceAllLevels.Count; i++)
        {

            if (_playerIntarfaceAllLevels[i].tag == $"PI_{numLevel}")
            {
                _playerIntarfaceAllLevels[i].SetActive(false);
            }
        }
    }
    private void LevelOn(int numLevel)
    {
        for (int i = 0; i < _playerIntarfaceAllLevels.Count; i++)
        {

            if (_playerIntarfaceAllLevels[i].tag == $"PI_{numLevel}")
            {
                _playerIntarfaceAllLevels[i].SetActive(true);
            }
        }
    }
    public void LevelUpdate()
    {
        foreach (var finishPoint in _finishPointsAllLevels)
        {
            finishPoint.SetActive(true);
        }
        foreach (var HPController in _HPControllersAllLevels)
        {
            HPController.GetComponent<HPController>().CountHealthPoints = 3;
        }
    }
}
