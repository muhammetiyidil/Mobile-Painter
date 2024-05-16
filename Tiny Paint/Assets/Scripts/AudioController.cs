using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceDrawing;
    [SerializeField] private AudioSource audioSourceBackground;
    [SerializeField] private AudioSource audioSourceShape;

    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip drawingSound;
    [SerializeField] private AudioClip shapeSound;

    private bool isDrawing = false;

    private void Start()
    {
        audioSourceBackground.clip = backgroundMusic;
        audioSourceDrawing.clip = drawingSound;
        audioSourceShape.clip = shapeSound;

        audioSourceBackground.loop = true;
        audioSourceBackground.volume = 0.2f;
        audioSourceBackground.Play();
    }

    public void PlayDrawingSound()
    {
        if (!isDrawing)
        {
            audioSourceDrawing.loop = true;
            audioSourceDrawing.Play();
            isDrawing = true;
            //Debug.Log("drawing sound is active");
        }
    }

    public void StopDrawingSound()
    {
        if(isDrawing)
        {
            audioSourceDrawing.Pause();
            isDrawing = false;
        }
    }

    public void PlayShapeSound()
    {
        if (audioSourceShape != null && shapeSound != null)
        {
            audioSourceShape.PlayOneShot(shapeSound);
        }
    }
}
