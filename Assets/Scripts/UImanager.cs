using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public static UImanager instance { get; private set; }
    void Start()
    {
        instance = this;
    }
    public Image healthBar;//角色血条
    public void UpdateHealthBar(int curAmount,int maxAmount)
    {
        healthBar.fillAmount = (float)curAmount /(float) maxAmount;
    }
}
