using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanselShot : MonoBehaviour
{
    private Color _color;
    private void Start()
    {
        _color = gameObject.GetComponent<SpriteRenderer>().color;
    }
    private void Update()
    {
        if (PlayerController.BallIsFlying == true)
            _color.a = 1;
        else
            _color.a = 0;
        gameObject.GetComponent<SpriteRenderer>().color = _color;
    }
    private void OnMouseUp()
    {
        DeleteBall();
    }
    private void DeleteBall()
    {
        GameObject[] bals = GameObject.FindGameObjectsWithTag("Bullet");
        if (bals != null)
        foreach (var ball in bals)
        {
            Destroy(ball);
        }
        
    }
}
