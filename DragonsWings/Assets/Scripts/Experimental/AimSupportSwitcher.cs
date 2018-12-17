using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimSupportSwitcher : MonoBehaviour
{

    public Sprite picOn;
    public Sprite picOff;

    private bool supportIsOn = true;

    public GameObject picture;



    public void switchPic()
    {
        supportIsOn = !supportIsOn;

        if (supportIsOn) picture.GetComponent<Image>().sprite = picOn;
        else picture.GetComponent<Image>().sprite = picOn;
    }


}
