using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static bool BallIsFlying;
    public static bool CanPlay;

    [SerializeField] private GameObject _arrow;
    [SerializeField] private GameObject _childArrow;
    [SerializeField] private GameObject _prefabCircle;

    private Vector2 _mousePos;
    private Color _mainCircleColor;
    private Color _arrowColor;

    private Rigidbody2D _circleRigidbody2D;
    private GameObject _circle;
    [Range(1, 10)] [SerializeField] private float _speedBall;  

    public void Start()
    {
        _arrowColor = _childArrow.GetComponent<SpriteRenderer>().color;
        _mainCircleColor = GetComponent<SpriteRenderer>().color;
    }
    public void Update()
    {
        PlayerControll();
    }
    private void RotateArrow() // Поворачивает стрелку.
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngle()));
    }

    private float GetAngle() // Считает угол поворота стрелки.
    {
        float Des_Y = (GameObject.Find("LevelsController").GetComponent<LevelsController>().NumberLevel * 10.5f);
        float m_Pos = _mousePos.y - Des_Y;
        _mousePos.y = m_Pos;
        float angle, vector1, vector2, scalar;  
        scalar = 0 * _mousePos.x  + 1 * _mousePos.y;
        vector1 = (float)Math.Sqrt(1);
        vector2 = (float)Math.Sqrt(_mousePos.x * _mousePos.x + _mousePos.y * _mousePos.y);
        angle = (float)Math.Acos(scalar / (vector1 * vector2)) * 180 / Mathf.PI;
        
        if (_mousePos.y > 0 && _mousePos.x < 0 || _mousePos.x < 0 && _mousePos.y < 0)
        {
            return angle;
        }
        return -angle;
    }
    private void Shot()
    {
        _circle = Instantiate(_prefabCircle, gameObject.transform.position, Quaternion.identity);
        _circleRigidbody2D = _circle.GetComponent<Rigidbody2D>();
        _circleRigidbody2D.AddForce(GetUnitVector(_mousePos) * _speedBall, ForceMode2D.Impulse);
        GameObject.Find("HPController").GetComponent<HPController>().RemoveHealthPoint();
    }
    private Vector2 GetUnitVector(Vector2 MouseVector) // Считает единичный вектор направления курсора мыши.
    {
        float LenghtMouseVector = (float)Math.Sqrt(MouseVector.x * MouseVector.x + MouseVector.y * MouseVector.y);
        Vector2 LenghtUnitVector = new Vector2(MouseVector.x / LenghtMouseVector, MouseVector.y / LenghtMouseVector);
        return LenghtUnitVector;
    }
    private void PlayerControll()
    {
        CanPlay = AnimationEvent.CanPlay;

        if (GameObject.FindGameObjectWithTag("Bullet") != null)
        {
            BallIsFlying = true;
        }
        else
        {
            BallIsFlying = false;
        }
            

        if (Input.GetKey(KeyCode.Mouse0) && CanPlay == true && BallIsFlying == false)
        {
            ShowArrow();
            RotateArrow();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && CanPlay == true && BallIsFlying == false) 
        {
            UnShowArrow();
            Shot();
        }
        if(BallIsFlying == true)
            _mainCircleColor.a = 0;
        else
            _mainCircleColor.a = 1;

        gameObject.GetComponent<SpriteRenderer>().color = _mainCircleColor;
      
    }
    public void ShowArrow()
    {
        _arrowColor.a = 1;
        _childArrow.GetComponent<SpriteRenderer>().color = _arrowColor;
    }
    public void UnShowArrow()
    {
        _arrowColor.a = 0;
        _childArrow.GetComponent<SpriteRenderer>().color = _arrowColor;
    }
}
