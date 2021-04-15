using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerDialogo : MonoBehaviour
{
    TextMeshProUGUI textMeshPro;

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText(string text)
    {
        if (!textMeshPro.enabled)
        {
            textMeshPro.enabled = true;
            textMeshPro.SetText(text);
            StartCoroutine(TextTime());
        }
    }

    IEnumerator TextTime()
    {
        yield return new WaitForSeconds(3f);
        if (textMeshPro.enabled)
        {
            textMeshPro.enabled = false;
        }
    }
}
