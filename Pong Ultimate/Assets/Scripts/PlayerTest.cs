using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTest : MonoBehaviour
{
  [SerializeField] float speed; // Paddle spped - can change on editor

  private Rigidbody rb;

  private Vector3 screenBounds;
  private float objHeight;
  private float moveY;

  private void Start()
  {
    rb = GetComponent<Rigidbody>();

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
    Vector3 movement = new Vector3(0.0f, moveY, 0.0f);
    rb.AddForce(movement * speed);
    
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
