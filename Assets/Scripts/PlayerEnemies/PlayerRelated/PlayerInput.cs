using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float HorizontalDirection { get; private set; }
    public bool isJumpButtonPressed { get; private set; }
    public bool isDownButtonPressed { get; private set; }
    public bool isAttackButtonPressed { get; private set; }

    private bool shouldDetectInput = true;

    private void OnEnable()
    {
        GameManager.onFail += BlockInput;
        GameManager.onWin += BlockInput;

    }

    private void OnDisable()
    {
        GameManager.onFail -= BlockInput;
        GameManager.onWin -= BlockInput;
    }

    private void BlockInput()
    {
        shouldDetectInput = false;
        HorizontalDirection = 0;
    }


    void Update()
    {
        if (shouldDetectInput)
        {
            HorizontalDirection = Input.GetAxis(StringConsts.HORIZONTAL_AXIS);
            isJumpButtonPressed = Input.GetButtonDown(StringConsts.JUMP);
            isAttackButtonPressed = Input.GetButtonDown(StringConsts.ATTACK);
            isDownButtonPressed = Input.GetButtonDown(StringConsts.VERTICAL_AXIS) && Input.GetAxisRaw(StringConsts.VERTICAL_AXIS) == -1; 
        }
    }
}
