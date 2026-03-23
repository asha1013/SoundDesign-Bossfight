using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton : MonoBehaviour
{
    [SerializeField] GameObject panel;

    public AK.Wwise.Event buttonclick;
    public void ExitCredits()
    {
        buttonclick.Post(gameObject);
        panel.SetActive(false);
    }
}
