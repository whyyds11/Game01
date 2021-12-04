using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    public Texture image;
    public static bool danchu = false;
    public static bool danru = false;
    static float alpha = 0f;
    public float speed;
    public static string changjing;
    private void OnGUI()
    {
        if (danchu)
        {
            alpha += speed * Time.deltaTime;
            if (alpha >= 1)
            {
                SceneManager.LoadScene(changjing);
                danchu = false;
                danru = true;
            }
        }
        if (danru)
        {
            alpha -= 0.1f * Time.deltaTime;
            if (alpha <= 0)
            {
                danru = false;
            }
        }
       GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
       GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), image);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
