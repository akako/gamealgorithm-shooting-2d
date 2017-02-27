using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Item_Straight : Main_Item_Base
{
    public override void Affect()
    {
        Main_SoundManager.Instance.powerup.Play();
        Main_SceneController.Instance.player.StraightShotLevel++;
    }
}
