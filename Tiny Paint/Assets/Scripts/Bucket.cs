using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    [SerializeField] private ZPositionProvider zPos;
    [SerializeField] private GameManagement gameManagement;
    [SerializeField] private AudioSource audioSource;
    public GameObject paint;

    public void Paint()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
        Vector3 newPosition = paint.transform.position;
        newPosition.z = zPos.ReceiveZPosition();
        paint.tag = "Bucket";
        GameObject paintObj= Instantiate(paint, newPosition, Quaternion.identity);
        gameManagement.SaveBuckets(paintObj, newPosition.z);
    }

    public void SelectColor(GameObject gameObject)
    {
        this.paint = gameObject;
    }
}
