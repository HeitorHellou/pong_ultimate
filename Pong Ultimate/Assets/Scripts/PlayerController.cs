using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField] float speed; // Paddle spped - can change on editor

  private Vector3 screenBounds;
  private float objWidth, objHeight;

  private void Start()
  {
    screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 2));
  }

  private void FixedUpdate()
  {
    MovePaddle();
  }

  // Moving the player
  public void MovePaddle()
  {
    float moveY = Input.GetAxis("Vertical"); // Getting the input from the player
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
    transform.position = new Vector3(-8.2f, Mathf.Clamp(viewPos.y, screenBounds.y * -1 + transform.lossyScale.y + 1, screenBounds.y - transform.lossyScale.y + 1), 2);
  }

  public void ResetPlayerPosition()
  {
    transform.position = new Vector3(-8.2f, 1, 2);
  }
}
