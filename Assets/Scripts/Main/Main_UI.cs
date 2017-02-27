using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Main_UI : MonoBehaviour
{
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text lifeText;

    int currentScore = 0;

    public int Score
    {
        set
        {
            DOTween.To(() => currentScore, x =>
                {
                    currentScore = x;
                    scoreText.text = string.Format("{0}", value); 
                }, value, 1f);
        }
    }

    public int Life
    {
        set { lifeText.text = new string('■', value); }
    }
}
