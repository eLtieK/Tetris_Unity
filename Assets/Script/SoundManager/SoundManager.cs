using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource line;
    public AudioSource move;
    public AudioSource hit;
    private void Start()
    {
        move.volume = 0.2f;
    }
    public void Line() { line.Play(); }
    public void Move() { move.pitch = 1.2f;  move.Play(); }
    public void Change() { move.pitch = 1.3f; move.Play(); }
    public void Hit() { hit.Play(); }
}
