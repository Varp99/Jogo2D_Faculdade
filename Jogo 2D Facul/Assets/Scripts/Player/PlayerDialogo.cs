using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDialogo : MonoBehaviour
{
    TextMeshProUGUI textMeshPro;
    public Image image;

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.enabled = false;
        image.enabled = false;
    }

    public void UpdateText(string text)
    {
        if (!textMeshPro.enabled && !image.enabled)
        {
            image.enabled = true;
            textMeshPro.enabled = true;
            textMeshPro.SetText(text);
            StartCoroutine(TextTime());
        }
    }

    IEnumerator TextTime()
    {
        yield return new WaitForSeconds(3f);
        if (textMeshPro.enabled && image.enabled)
        {
            textMeshPro.enabled = false;
            image.enabled = false;
        }
    }
}
