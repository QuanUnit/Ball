using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationStick : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _dirRotaion = 1;

    void FixedUpdate()
    {
        gameObject.transform.Rotate(new Vector3(0, 0, Time.deltaTime * _dirRotaion * _speed));
    }
}
