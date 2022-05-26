using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HPController : MonoBehaviour
{
    public bool IsDeath = false;
    public int CountHealthPoints = 3;

    [SerializeField] private List<GameObject> _healthPoints;
    

    private void FixedUpdate()
    {
        if (CountHealthPoints == 0 && GameObject.Find("Circle(Clone)") == null && IsDeath == false)
        {
            IsDeath = true;
            Death();
        }
        if (CountHealthPoints == 3)
        {
            IsDeath = false;
            foreach (var HealthPoint in _healthPoints)
            {
                HealthPoint.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
            }
        }
    }
    public void RemoveHealthPoint()
    {
        if (CountHealthPoints > 0)
        {
            CountHealthPoints--;
        }
        _healthPoints[3 - CountHealthPoints - 1].GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.4f, 0.4f);
    }
    private void Death()
    {
        StartCoroutine(GameObject.Find("LevelsController").GetComponent<LevelsController>().DownMoveCamera());
    }
}
