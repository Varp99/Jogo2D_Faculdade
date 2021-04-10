using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HearthContainer : MonoBehaviour
{
    public HearthContainer next;

    [Range(0, 1)] float fill;
    [SerializeField] Image fillImage;

    public void SetHearts(float count)
    {
        fill = count;
        fillImage.fillAmount = fill;
        count--;

        if (next != null)
        {
            next.SetHearts(count);
        }
        Debug.Log(count);
    }
}