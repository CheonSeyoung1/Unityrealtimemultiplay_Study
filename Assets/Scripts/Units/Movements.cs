using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 4f; 
    [SerializeField] private float maxVelocityChange = 10f;

    private Vector2 input = Vector2.zero;
    private Rigidbody myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var m_horizontal = Input.GetAxisRaw("Horizontal");
        var m_vertical = Input.GetAxisRaw("Vertical");
        
        input.x = m_horizontal;
        input.y = m_vertical;
        input.Normalize();
    }
	private void FixedUpdate()
	{
        myRigidbody.AddForce(CalculateMovement(walkSpeed), ForceMode.VelocityChange);
	}
	private Vector3 CalculateMovement(float _speed)
    {
        const float MIN_MOVEMENT = 0.5f;

        Vector3 value = Vector3.zero;
        var m_targetVelocity = new Vector3(input.x, 0, input.y);
        input = Vector3.zero;

        m_targetVelocity = transform.TransformDirection(m_targetVelocity);
        m_targetVelocity *= _speed;

        Vector3 m_velocity = myRigidbody.velocity;
        if (m_targetVelocity.magnitude > MIN_MOVEMENT)
        {
            value = m_targetVelocity - m_velocity;
            value.x = Mathf.Clamp(value: value.x, min: -maxVelocityChange, max: maxVelocityChange);
            value.z = Mathf.Clamp(value: value.z, min: -maxVelocityChange, max: maxVelocityChange);
            value.y = 0;
        }

        return value;
    }

}
