using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField] float speed; // Paddle spped - can change on editor

  float yMin = -1f, yMax = 2f; // Minimum and maximum value in which the player can transverse - Y axis

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
    if (transform.position.y < yMin)
      transform.position = new Vector3(-8.2f, yMin, 0.0f);
    if (transform.position.y > yMax)
      transform.position = new Vector3(-8.2f, yMax, 0.0f);
  }
}
