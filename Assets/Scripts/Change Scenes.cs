using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //FdsController fds = collision.gameObject.GetComponent<FdsController>();
        //if (fds != null)
        {
            Fade.danchu = true;
            Fade.changjing = "kongchangjing";
        }
    }
}
