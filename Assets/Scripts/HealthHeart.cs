using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHeart : MonoBehaviour
{
    public Sprite full, half, empty;
    Image heartImage;

    private void Awake()
    {
        heartImage = GetComponent<Image>();
    }

    public void SetHeartImage(HeartState state){
        switch(state){
            case HeartState.Empty:
                heartImage.sprite = empty;
                break;
            case HeartState.Half:
                heartImage.sprite = half;
                break;
            case HeartState.Full:
                heartImage.sprite = full;
                break;
        }
    }
}

public enum  HeartState
{
    Empty = 0,
    Half = 1,
    Full = 2
}