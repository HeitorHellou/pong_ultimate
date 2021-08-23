using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
  [SerializeField] float speed; // ball speed

  Rigidbody rb;

  private Vector3 lastFrameVelocity;

  private void Start()
  {
    rb = GetComponent<Rigidbody>(); // Rigid body link
    StartCoroutine("WaitLaunch"); // Launching the ball
  }

  private void FixedUpdate()
  {
    lastFrameVelocity = rb.velocity;
  }

  public void LaunchBall()
  {
    float dx = Random.Range(0, 2) == 0 ? -1 : 1; // Random x value to determine which side the ball will go
    float dy = Random.Range(0, 2) == 0 ? -1 : 1; // Where the ball will launch in the Y axis

    rb.velocity = new Vector3(dx, dy, 0.0f) * speed; // Launching the ball
    //rb.velocity = new Vector3(-1, 0.0f, 0.0f) * speed; // TEST
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
      //ChangeBallBounce(Vector3.Reflect(rb.velocity.normalized, collision.contacts[0].normal), collision.GetContact(0).point.y - collision.transform.position.y);
      ChangeBallBounce(collision.contacts[0].normal, collision.GetContact(0).point.y - collision.transform.position.y);
    }

    CollisionDetected(collision.contacts[0].normal);
  }

  public Vector3 CollisionDetected(Vector3 collisionNormal)
  {
    return Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);
  }

  // Boucing the ball back relative to distance from paddle center
  public void ChangeBallBounce(Vector3 collisionNormal, float y)
  {
    var ballVelocity = lastFrameVelocity.magnitude;
    var bounceDirection = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);
    Vector3 direction;

    if (y > 0.1)
    {
      if (y < 0.483)
        direction = Vector3.Lerp(bounceDirection, new Vector3(bounceDirection.x, bounceDirection.y + 0.5f, bounceDirection.z), 0.3f);
      else if (y < 0.866)
        direction = Vector3.Lerp(bounceDirection, new Vector3(bounceDirection.x, bounceDirection.y + 1f, bounceDirection.z), 0.3f);
      else
        direction = Vector3.Lerp(bounceDirection, new Vector3(bounceDirection.x, bounceDirection.y + 2f, bounceDirection.z), 0.3f);
    }
    else if (y < -0.1)
    {
      if (y > -0.483)
        direction = Vector3.Lerp(bounceDirection, new Vector3(bounceDirection.x, bounceDirection.y -0.5f, bounceDirection.z), 0.3f);
      else if (y > -0.866)
        direction = Vector3.Lerp(bounceDirection, new Vector3(bounceDirection.x, bounceDirection.y -1f, bounceDirection.z), 0.3f);
      else
        direction = Vector3.Lerp(bounceDirection, new Vector3(bounceDirection.x, bounceDirection.y -2f, bounceDirection.z), 0.3f);
    }
    else
      direction = Vector3.Lerp(bounceDirection, new Vector3(bounceDirection.x, 0.0f, bounceDirection.z), 0.3f);

    Debug.Log("bounce direction: " + direction);

    rb.velocity = direction * speed;
  }

  // Delay for ball launch
  IEnumerator WaitLaunch()
  {
    yield return new WaitForSeconds(2.0f); // Waiting 2 seconds 
    LaunchBall(); // Launching the ball
  }
}
