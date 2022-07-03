using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] float stoppingDistance = 0.5f;
    [SerializeField] float speed = 2f;
    Animator anim = null;
    bool isWalking = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Rotation();
    }

    private void Update()
    {
        Movement();
    }

    private void Rotation()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        float angle = Angle(transform.forward, direction);

        Vector3 crossProduct = Vector3.Cross(transform.forward, direction);
        bool isClockwise = crossProduct.y > 0;

        Vector3 forwardTransform = RotateAboutAngle(transform.forward, angle, isClockwise);
        transform.forward = forwardTransform;
    }

    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Walk");
            isWalking = true;
        }
        if (Vector3.Distance(transform.position, target.position) < stoppingDistance)
        {
            anim.ResetTrigger("Walk");
            anim.SetTrigger("Idle");
        }
        else if (isWalking && Vector3.Distance(transform.position, target.position) > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    Vector3 RotateAboutAngle(Vector3 vector, float angle, bool isClockwise)
    {
        if (isClockwise)
        {
            angle *= Mathf.Rad2Deg;
            angle = 360 - angle;
            angle *= Mathf.Deg2Rad;
        }

        float x = vector.x * Mathf.Cos(angle) - vector.z * Mathf.Sin(angle);
        float z = vector.x * Mathf.Sin(angle) + vector.z * Mathf.Cos(angle);
        return new Vector3(x, vector.y, z);
    }

    float Angle(Vector3 vector1, Vector3 vector2)
    {
        float dotProduct = Vector3.Dot(vector1, vector2);
        return Mathf.Acos(dotProduct / (vector1.magnitude * vector2.magnitude));
    }
}
