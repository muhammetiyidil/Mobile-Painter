using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZPositionProvider : MonoBehaviour
{
    private float zPosition;
    public bool zPosSaved = true;

    private void Start()
    {
        if (PlayerPrefs.GetFloat("zPosition") != 0)
        {
            zPosition = PlayerPrefs.GetFloat("zPosition");
        }
        else
        {
            zPosition = 99.99f;
            PlayerPrefs.SetFloat("zPosition", zPosition);
        }
    }

    public float ReceiveZPosition()
    {
        if(zPosSaved)
        {
            zPosition = PlayerPrefs.GetFloat("zPosition") - 0.1f;
            PlayerPrefs.SetFloat("zPosition", zPosition);
            return zPosition;
        }
        return zPosition;
        
    }
    public void ZPosReset()
    {
        zPosition = 99.99f;
        PlayerPrefs.SetFloat("zPosition", 99.99f);
    }
}
