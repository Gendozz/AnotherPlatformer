using UnityEngine;
using TMPro;
using System.Collections;

public class UIHUDDisplayer : MonoBehaviour
{
    [Header("Lives")]
    [SerializeField] private TMP_Text lives;
    [SerializeField] private string textOnLivesField;

    [Header("Scores")]
    [SerializeField] private TMP_Text scores;
    [SerializeField] private string textOnScoresField;

    [Header("Stopwatch")]
    [SerializeField] private TMP_Text stopwatchText;
    [SerializeField] private string textOnStopWatchField;
 
    private WaitForSeconds oneSecondDelay = new WaitForSeconds(1);

    private void Start()
    {
        //StartCoroutine(ShowPastTime());

    }
    private void OnEnable()
    {
        Player.onPlayerLiveAmountChanged += ShowActualPlayerHealth;
        Player.onPlayerScoresAmountChanged += ShowActualPlayerScores;
        StartCoroutine(ShowPastTime());

    }

    private void OnDisable()
    {
        Player.onPlayerLiveAmountChanged -= ShowActualPlayerHealth;
        Player.onPlayerScoresAmountChanged -= ShowActualPlayerScores;
    }

    private void ShowActualPlayerHealth(int actualPlayerLives)
    {
        lives.text = textOnLivesField + actualPlayerLives.ToString();
    }

    private void ShowActualPlayerScores(int actualPlayerScores)
    {
        scores.text = textOnScoresField + actualPlayerScores.ToString();
    }

    private IEnumerator ShowPastTime()
    {
        while (true)
        {
            float stopwatch = Time.timeSinceLevelLoad; 
            string minutes = Mathf.Floor(stopwatch / 60).ToString("00");
            string seconds = (stopwatch % 60).ToString("00");
            stopwatchText.text = textOnStopWatchField + string.Format("{0}:{1}", minutes, seconds);
            yield return oneSecondDelay; 
        }
    }

    public void ShowPastTime(string timePast)
    {

    }
}
