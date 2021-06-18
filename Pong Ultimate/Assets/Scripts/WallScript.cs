using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
  private Vector3 screenBounds;
  private float maxY;

  void Start()
  {
    transform.localScale = new Vector3(Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height, 1f, 1f);

    screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    maxY = screenBounds.y;

    if (gameObject.tag == "North Wall")
      transform.position = new Vector3(transform.position.x, maxY + 0.5f, transform.position.z);
    if (gameObject.tag == "South Wall")
      transform.position = new Vector3(transform.position.x, (maxY * -1) + 1.5f, transform.position.z);
  }
}
