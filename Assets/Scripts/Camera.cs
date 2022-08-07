using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private Vector3 currentTargetPosition;

    void Update()
    {
        currentTargetPosition = target.transform.position;

        currentTargetPosition.z = -10f;

        transform.position = Vector3.Lerp(transform.position, currentTargetPosition, Time.deltaTime);
    }
}
