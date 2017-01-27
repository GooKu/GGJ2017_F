using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField]
    Text mainText;

    [SerializeField]
    Text displayText;

    void Awake()
    {
        this.mainText.enabled = this.enabled;
        this.displayText.enabled = this.enabled;

        var manager = LevelManager.Singleton;
        if (manager != null)
        {
            this.displayText.text = string.Format("{0}/ {1}", manager.CurrentLevel, manager.TotalLevels - 2);
        }
    }

    void OnEnable()
    {
        this.mainText.enabled = true;
        this.displayText.enabled = true;
    }

    void OnDisable()
    {
        this.mainText.enabled = false;
        this.displayText.enabled = false;
    }
}
