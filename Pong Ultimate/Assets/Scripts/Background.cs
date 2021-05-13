using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
  private void Awake()
  {
    transform.localScale = new Vector3(Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height, 0.1f, Camera.main.orthographicSize * 2.0f); // Setting up the background to cover whole screen
  }
}
