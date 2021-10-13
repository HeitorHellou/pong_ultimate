using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  [SerializeField] float speed; // Paddle spped - can change on editor

  private Vector3 screenBounds; // Area in which the player can move
  private float objHeight; // objHeight - y size
  private float moveY; // stores the y movement of the play input - pong can only move on vertical

  private void Start()
  {
    screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 2));
  }

  private void FixedUpdate()
  {
    MovePaddle();
  }

  void OnMove(InputValue movementValue)
  {
    Vector2 movementVector = movementValue.Get<Vector2>(); // Getting the vector2 data from movementValue
    moveY = movementVector.y;
  }

  // Moving the player
  public void MovePaddle()
  {
    Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z); // Getting the current paddle position
    pos.y = moveY; // Assingning the movement to the Y axis

    transform.Translate(0, pos.y * speed * Time.fixedDeltaTime, 0); // Moving the paddle
    CheckBoundaries(); // Checking if the player is off boundaries (camera space)
  }

  // Checking the limits in which the player can move
  public void CheckBoundaries()
  {
    Vector3 viewPos = transform.position;
    viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objHeight, screenBounds.y - objHeight);
    if (gameObject.tag == "Player")
      transform.position = new Vector3(-8.2f, Mathf.Clamp(viewPos.y, screenBounds.y * -1 + transform.lossyScale.y + 1, screenBounds.y - transform.lossyScale.y + 1), 2);
    if (gameObject.tag == "Player 2")
      transform.position = new Vector3(8.2f, Mathf.Clamp(viewPos.y, screenBounds.y * -1 + transform.lossyScale.y + 1, screenBounds.y - transform.lossyScale.y + 1), 2);
  }

  public void ResetPlayerPosition()
  {
    if (gameObject.tag == "Player")
      transform.position = new Vector3(-8.2f, 1, 2);
    if (gameObject.tag == "Player 2")
      transform.position = new Vector3(8.2f, 1, 2);
  }
}
