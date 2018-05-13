namespace FaderAsync
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class FaderAsync
    {
        public static IEnumerator FadeIn(this Renderer r, float fadeIn, float maxA = 1)
        {
            if (r == null) yield break;
            float frequency = 1 / fadeIn;
            Color c = r.material.color;
            for (float f = c.a; f <= 1 + frequency; f += frequency)
            {
                if (f >= maxA)
                {
                    c.a = maxA;
                    r.material.color = c;
                    break;
                }
                c.a = f;
                r.material.color = c;
                yield return null;
            }
        }

        public static IEnumerator FadeOut(this Renderer r, float fadeOut, float minA = 0)
        {
            if (r == null) yield break;
            float frequency = 1 / fadeOut;
            Color c = r.material.color;
            for (float f = c.a; f >= -frequency; f -= frequency)
            {
                if (f <= minA)
                {
                    c.a = minA;
                    r.material.color = c;
                    break;
                }
                c.a = f;
                r.material.color = c;
                yield return null;
            }
        }
    }
}
