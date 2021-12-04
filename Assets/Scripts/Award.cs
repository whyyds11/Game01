using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Award : MonoBehaviour
{
    public float speed = 100;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(-Vector3.forward * speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            
            AudioManager._instance.PlayCollectible();
            Destroy(this.gameObject);
        }
    }
}
