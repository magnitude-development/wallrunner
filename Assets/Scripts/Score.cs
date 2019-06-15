using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Time time;

    void Update() {
        Debug.Log(Time.timeSinceLevelLoad);
    }
}