using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public bool isWin = false;

    public bool isFail = false;

    private bool isPaused = false;

    private bool isBlocked = false;

    private float timePast;
    
    
    [SerializeField] private Player player;

    [SerializeField] private UIMenuController uIMenuController;

    [SerializeField] private UIHUDDisplayer hudDisplayer;


    public static Action onWin;
    public static Action onFail;
    public static Action<bool> onGameChangesPausedState;

    private WaitForSeconds oneSecondDelay = new WaitForSeconds(1);

    private void OnEnable()
    {
        EndLevel.onPlayerGotToLevelEnd += DoWinSequence;
        Player.onPlayerKilled += DoFailSequence;
    }

    private void OnDisable()
    {
        EndLevel.onPlayerGotToLevelEnd -= DoWinSequence;
        Player.onPlayerKilled -= DoFailSequence;
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        Time.timeScale = 1f;
        StartCoroutine(CountPastTime());
    }

    private void DoWinSequence()
    {
        SetLevelStats();
        onWin?.Invoke();
    }

    private void DoFailSequence()
    {
        SetLevelStats();
       onFail?.Invoke();
    }

    private void SetLevelStats()
    {
        uIMenuController.SetLevelStats(player.CurrentScoreAmount.ToString(), GetTimePastFormattedString(timePast));

    }


    private void Update()
    {
        if (Input.GetButtonDown(StringConsts.ESC))
        {
            ChangeGamePauseState();
        }
    }

    private IEnumerator CountPastTime()
    {
        while (true)
        {
            timePast = Time.timeSinceLevelLoad;
            hudDisplayer.ShowPastTime(GetTimePastFormattedString(timePast));
            yield return oneSecondDelay;
        }
    }

    private string GetTimePastFormattedString(float timePast)
    {
        string minutes = Mathf.Floor(timePast / 60).ToString("00");
        string seconds = (timePast % 60).ToString("00");
        return string.Format("{0}:{1}", minutes, seconds);
    }

    #region Pause
    public void ChangeGamePauseState()
    {
        isPaused = !isPaused;
        onGameChangesPausedState?.Invoke(isPaused);

        if (!isPaused)
        {
            UnpauseGame();
        }
        else if (isPaused)
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void UnpauseGame()
    {
        Time.timeScale = 1;
    } 
    #endregion



}
