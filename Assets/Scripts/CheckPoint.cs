using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] int addedTime = 10;
    [SerializeField] AudioClip itemClip;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            //Reproducir el audio de los items
            AudioSource.PlayClipAtPoint(itemClip, transform.position);
            
            GameManager.Instance.TimeLeft += addedTime;
            Destroy(gameObject, 0.1F);
        }
    }
}
