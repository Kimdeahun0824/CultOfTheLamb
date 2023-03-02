using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static partial class ConvenienceFunc
{
    public static void SetImageSprite(this GameObject obj_, Sprite sprite)
    {
        Image objImage = obj_.GetComponent<Image>();
        if (objImage != null)
        {
            objImage.sprite = sprite;
        }
        else
        {
            return;
        }
    }

    public static Sprite GetImageSprite(this GameObject obj_)
    {
        Sprite sprite = obj_.GetComponent<Image>().sprite;
        if (obj_.GetComponent<Image>().sprite != null)
        {
            return sprite;
        }
        else
        {
            return null;
        }
    }

    public static void SetImageFilled(this GameObject obj_, float filled)
    {
        Image objImage = obj_.GetComponent<Image>();
        if (objImage != null)
        {
            objImage.fillAmount = filled;
        }
        else
        {
            return;
        }
    }
}