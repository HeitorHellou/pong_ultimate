using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
  private float speed = 5f; // Paddle spped
  private Vector3 screenBounds; // Area in which the player can move
  private float objHeight; // objHeight - y size

  private GameObject ball;
  private Vector3 ballPos;

  private void Start()
  {
    screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 2));
    ball = GameObject.FindGameObjectWithTag("Ball");
  }

  private void Update()
  {
    Move();
    CheckAIBoundaries();
  }

  public void Move()
  {
    if (ball.GetComponent<BallController1v1>().ballDir == Vector3.right)
    {
      ballPos = ball.transform.position;

      transform.Translate(0, ballPos.y * speed * Time.deltaTime, 0);

      CheckAIBoundaries();
    }
  }

  public void CheckAIBoundaries()
  {
    Vector3 viewPos = transform.position;
    viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objHeight, screenBounds.y - objHeight);

    transform.position = new Vector3(8.2f, Mathf.Clamp(viewPos.y, screenBounds.y * -1 + transform.lossyScale.y + 1, screenBounds.y - transform.lossyScale.y + 1), 2);
  }

  public void ResetAIPosition()
  {
    transform.position = new Vector3(8.2f, 1, 2);
    GetComponent<Rigidbody>().velocity = Vector3.zero;
  }
}
