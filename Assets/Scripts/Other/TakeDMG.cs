using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeMDG : MonoBehaviour
{
    public IEnumerator ShowHitEffect(SpriteRenderer spriteRenderer)
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.6f);
        spriteRenderer.color = originalColor;
    }
}
