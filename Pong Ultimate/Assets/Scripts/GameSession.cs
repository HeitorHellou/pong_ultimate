using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
  [SerializeField] int score_p1 = 0, score_p2 = 0; // Score player 1 and player 2
  [SerializeField] Text p1_score, p2_score;

  private void Awake()
  {
    int gameSessionCount = FindObjectsOfType<GameSession>().Length;
    if (gameSessionCount > 1) // If the game alredy has a game session, keep it
      Destroy(gameObject);
    else
      DontDestroyOnLoad(gameObject);
  }

  private void Start()
  {
    p1_score.text = score_p1.ToString();
    p2_score.text = score_p2.ToString();
  }

  // Player 1 scores
  public void ScorePointP1()
  {
    score_p1++;
    p1_score.text = score_p1.ToString();
  }

  // Player 2 scores
  public void ScorePointP2()
  {
    score_p2++;
    p2_score.text = score_p2.ToString();
  }
}
