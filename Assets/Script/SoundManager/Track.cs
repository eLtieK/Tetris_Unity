using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public AudioSource track;

    private void Start()
    {
        if (gameObject.tag == "Track")
        {
            track.volume = 0.2f;
            track.Play();
            GameObject[] musicObject = GameObject.FindGameObjectsWithTag("Track");
            if (musicObject.Length > 1) { Destroy(this.gameObject); }
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
