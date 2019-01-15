using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimSupportSwitcher : MonoBehaviour
{

    public Sprite picOn;
    public Sprite picOff;

    private bool supportIsOn = true;

    public Image picture;



    public void switchPic()
    {
        supportIsOn = !supportIsOn;
        picture.sprite = supportIsOn ? picOn : picOff;
    }
}