﻿namespace Helper
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class Helper
    {
        public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag) where T : Component
        {
            Transform[] t = parent.GetComponentsInChildren<Transform>();
            foreach (Transform tr in t)
            {
                if (tr.tag == tag)
                {
                    return tr.GetComponent<T>();
                }
            }

            return null;
        }
    }
}