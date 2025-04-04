using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public float roundTime = 60f;
    private UIManager uiManager;
    // Start is called before the first frame update
    void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (roundTime > 0)
        {
            roundTime -= Time.deltaTime;
            if (roundTime < 0)
            {
                roundTime = 0;
            }
        }
        uiManager.timeText.text = roundTime.ToString("F3");
    }
}
