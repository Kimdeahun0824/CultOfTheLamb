using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBtn : MonoBehaviour
{
    public void OnClick()
    {
        ConvenienceFunc.LoadScene("02. StageScene");
    }
}
