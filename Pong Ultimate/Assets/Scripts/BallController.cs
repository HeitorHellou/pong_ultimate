using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
  [SerializeField] float speed; // ball speed

  Rigidbody rb;

  private void Start()
  {
    rb = GetComponent<Rigidbody>(); // Rigid body link
    StartCoroutine("WaitLaunch"); // Launching the ball
  }

  public void LaunchBall()
  {
    float dx = Random.Range(0, 2) == 0 ? -1 : 1; // Random x value to determine which side the ball will go
    float dy = Random.Range(0, 2) == 0 ? -1 : 1; // Where the ball will launch in the Y axis

    //rb.velocity = new Vector3(dx, dy, 0.0f) * speed; // Launching the ball
    rb.velocity = new Vector3(-1, 0.0f, 0.0f) * speed; // TEST
  }

  private void OnTriggerEnter(Collider other)
  {
    // Checking who scored the goal
    if (other.gameObject.tag == "Trigger P1")
    {
      // Adding a point to player score
      FindObjectOfType<GameSession>().ScorePointP2();
      // Reseting both player positions
      foreach (var x in FindObjectsOfType<PlayerController>())
      {
        x.ResetPlayerPosition();
      }
    }
    if (other.gameObject.tag == "Trigger P2")
    {
      FindObjectOfType<GameSession>().ScorePointP1();
      foreach (var x in FindObjectsOfType<PlayerController>())
      {
        x.ResetPlayerPosition();
      }
    }
    ResetBallPosition(); // Reseting ball position
    // Removing the object forces
    rb.velocity = Vector3.zero;
    rb.angularVelocity = Vector3.zero;
    // Relaunching the ball
    StartCoroutine("WaitLaunch");
  }

  public void ResetBallPosition()
  {
    transform.position = new Vector3(0.0f, 1, 2); // Reseting the ball on a new z-axis - vertical axis
  }

  // Checking where the ball collide with the player paddle
  public void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player 2"))
    {
      ChangeBallBounce(Vector3.Reflect(rb.velocity.normalized, collision.contacts[0].normal), collision.GetContact(0).point.y - collision.transform.position.y);
    }
  }

  // Boucing the ball back relative to distance from paddle center
  public void ChangeBallBounce(Vector3 direction, float y)
  {
    if (y > 0.1)
    {
      if (y < 0.483)
        rb.AddForce(new Vector3(direction.x * -1, direction.y + 75f, direction.z) * speed * Time.deltaTime);
      else if (y < 0.866)
        rb.AddForce(new Vector3(direction.x * -1, direction.y + 150f, direction.z) * speed * Time.deltaTime);
      else
        rb.AddForce(new Vector3(direction.x * -1, direction.y + 225f, direction.z) * speed * Time.deltaTime);
    }
    else if (y < -0.1)
    {
      if (y > -0.483)
        rb.AddForce(new Vector3(direction.x * -1, direction.y + -75f, direction.z) * speed * Time.deltaTime);
      else if (y > -0.866)
        rb.AddForce(new Vector3(direction.x * -1, direction.y + - 150f, direction.z) * speed * Time.deltaTime);
      else
        rb.AddForce(new Vector3(direction.x * -1, direction.y - 225f, direction.z) * speed * Time.deltaTime);
    }
    else
      rb.AddForce(new Vector3(direction.x * -1, 0.0f, direction.z) * speed * Time.deltaTime);
  }

  // Delay for ball launch
  IEnumerator WaitLaunch()
  {
    yield return new WaitForSeconds(2.0f); // Waiting 2 seconds 
    LaunchBall(); // Launching the ball
  }
}
