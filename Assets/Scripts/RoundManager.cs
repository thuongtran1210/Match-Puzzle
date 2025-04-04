using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public float roundTime;
    private UIManager uiManager;
    private bool endingRound = false;
    private Board board;
    public int currentScore;
    public float displayScore;
    public float scoreSpeed;
    // Start is called before the first frame update
    void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        board = FindObjectOfType<Board>();
    }

    // Update is called once per frame
    void Update()
    {
        if (roundTime > 0)
        {
            roundTime -= Time.deltaTime;
            if (roundTime < 0)
            {
                roundTime = 0;
                endingRound = true;
            }
        }
        if (endingRound && board.currentState == Board.BoardState.move)
        {
            WinCheck();
            endingRound = false;
        }
        uiManager.timeText.text = roundTime.ToString("F3");
        displayScore = Mathf.Lerp(displayScore, currentScore,scoreSpeed * Time.deltaTime);
        uiManager.scoreText.text = displayScore.ToString("0");
    }
    private void WinCheck()
    {
        uiManager.roundOverScreen.SetActive(true);

    }
}
