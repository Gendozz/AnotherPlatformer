using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIMenuController : MonoBehaviour
{
    [SerializeField] private CanvasGroup failCanvasGroup;
    [SerializeField] private CanvasGroup winCanvasGroup;
    [SerializeField] private CanvasGroup pauseCanvasGroup;

    [SerializeField] private TMP_Text totalScoreFail;
    [SerializeField] private TMP_Text totalTimePastFail;

    [SerializeField] private TMP_Text totalScoreWin;
    [SerializeField] private TMP_Text totalTimePastWin;

    private string totalScores;
    private string timePast;


    private void OnEnable()
    {
        Player.onPlayerKilled += ShowFailCanvas;
        GameManager.onFail += ShowFailCanvas;
        GameManager.onWin += ShowWinCanvas;
        GameManager.onGameChangesPausedState += ShowPauseCanvas;
    }

    private void OnDisable()
    {
        Player.onPlayerKilled -= ShowFailCanvas;
        GameManager.onFail -= ShowFailCanvas;
        GameManager.onWin -= ShowWinCanvas;
        GameManager.onGameChangesPausedState -= ShowPauseCanvas;
    }

    private void Awake()
    {
        winCanvasGroup.gameObject.SetActive(true);
        failCanvasGroup.gameObject.SetActive(true);
        pauseCanvasGroup.gameObject.SetActive(true);
        SetUpCanvasGroup(winCanvasGroup, 0, false, false);
        SetUpCanvasGroup(failCanvasGroup, 0, false, false);
        SetUpCanvasGroup(pauseCanvasGroup, 0, false, false);
    }

    public void ShowFailCanvas()
    {
        totalScoreFail.text = "Scores: " + totalScores;
        totalTimePastFail.text = "Time: " + timePast;
        SetUpCanvasGroup(failCanvasGroup, 1, true, true);
    }

    public void ShowWinCanvas()
    {
        totalScoreWin.text = "Scores: " + totalScores;
        totalTimePastWin.text = "Time: " + timePast;
        SetUpCanvasGroup(winCanvasGroup, 1, true, true);
    }

    private void ShowPauseCanvas(bool toShow)
    {
        float canvasAlpha = toShow ? 1 : 0;
        SetUpCanvasGroup(pauseCanvasGroup, canvasAlpha, toShow, toShow);
    }

    public void SetUpCanvasGroup(CanvasGroup canvasGroup, float alpha, bool blocksRaycasts, bool interactable)
    {
        canvasGroup.alpha = alpha;
        canvasGroup.blocksRaycasts = blocksRaycasts;
        canvasGroup.interactable = interactable;
    }

    public void SetLevelStats(string totalScores, string timePast)
    {
        this.totalScores = totalScores;
        this.timePast = timePast;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
