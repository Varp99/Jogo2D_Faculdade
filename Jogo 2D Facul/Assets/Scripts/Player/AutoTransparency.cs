using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTransparency : MonoBehaviour
{
    [Range(0, 1)]
    public float transparency;
    public Renderer targetRenderer;
    public LayerMask layerMask;
    public float fadeDuration;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (layerMask == (layerMask | (1 << collision.gameObject.layer)))
        {
            SetTransparency(transparency);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (layerMask == (layerMask | (1 << collision.gameObject.layer)))
        {
            SetTransparency(1);
        }
    }

    private void SetTransparency(float alpha)
    {
        StopCoroutine("FadeCoroutine");
        StartCoroutine("FadeCoroutine", alpha);
    }

    private IEnumerator FadeCoroutine(float fadeTo)
    {
        float timer = 0f;
        Color currentColor = targetRenderer.material.color;
        float startAlpha = targetRenderer.material.color.a;

        while(timer < 1)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime / fadeDuration;
            currentColor.a = Mathf.Lerp(startAlpha, fadeTo, timer);
            targetRenderer.material.color = currentColor;
        }
    }
}
