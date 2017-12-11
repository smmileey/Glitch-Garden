using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTrophy : MonoBehaviour
{
    private StarDisplay _starDisplay;

    void Start()
    {
        _starDisplay = FindObjectOfType<StarDisplay>();
        if (_starDisplay == null) Debug.LogWarning("Star display not exist!");
    }

    public void ReleaseStars(int amount)
    {
        _starDisplay.AddStars(amount);
    }
}
