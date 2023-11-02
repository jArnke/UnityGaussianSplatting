using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementProvider : MonoBehaviour
{
    [SerializeField] InputActionProperty joystick;
    [SerializeField] InputActionProperty head;
    [SerializeField] Transform headT;
    [SerializeField] float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = joystick.action.ReadValue<Vector2>().y;
        Vector3 dir = head.action.ReadValue<Quaternion>() * Vector3.forward;
        dir = headT.forward;
        this.transform.position += dir * x * speed * Time.deltaTime;
        Debug.Log(x);
        
    }
}
