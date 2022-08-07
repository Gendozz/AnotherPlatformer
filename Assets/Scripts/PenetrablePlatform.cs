using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(PlatformEffector2D))]
public class PenetrablePlatform : MonoBehaviour
{
    [SerializeField] private float switchToNormalDelaySeconds;

    private PlatformEffector2D platformEffector2;

    private void Start()
    {
        platformEffector2 = GetComponent<PlatformEffector2D>();
    }

    private void OnEnable()
    {
        PlayerMovement.onPlayerJumpDown += LetPlayerDown;
    }

    private void OnDisable()
    {
        PlayerMovement.onPlayerJumpDown -= LetPlayerDown;
    }

    private void LetPlayerDown()
    {
        platformEffector2.rotationalOffset = 180;
        StartCoroutine(SwitchBackToNormal());
    }

    private IEnumerator SwitchBackToNormal()
    {
        yield return new WaitForSeconds(switchToNormalDelaySeconds);
        platformEffector2.rotationalOffset = 0;

    }
}
