using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStick : MonoBehaviour
{
    [SerializeField] private float _movingInterval;
    [SerializeField] private float _speed;
    [SerializeField] private int _dir = 1;

    private Vector3 _startStickPosition;
    private float _size;
    private float _scale;
    
    private void Start()
    { 
        _startStickPosition = transform.position;
        DrawGizmosLine();
    }
    void FixedUpdate()
    {
        MoveStick();
    }
    private void MoveStick()
    {
        transform.position += transform.right * _dir / 10 * _speed * Time.deltaTime;
        if (Vector3.Distance(_startStickPosition, transform.position) >= _movingInterval)
            _dir = -_dir;
    }
    private void DrawGizmosLine()
    {
        _size = GetComponent<SpriteRenderer>().sprite.rect.width / 4;
        _scale = transform.localScale.x;
        _size *= _scale;
        Debug.DrawLine(_startStickPosition + transform.right * (_movingInterval + _size / 2), _startStickPosition + transform.right * -(_movingInterval + _size / 2), Color.green, 9999999);
    }
}
