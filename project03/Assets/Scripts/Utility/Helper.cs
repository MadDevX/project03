namespace Helper
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class Helper
    {
        public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag) where T : Component
        {
            Transform[] t = parent.GetComponentsInChildren<Transform>();
            // if (!t) Debug.Log("fuckscript");
            foreach (Transform tr in t)
            {
                if (tr.tag == tag)
                {
                    return tr.GetComponent<T>();
                }
            }
            Debug.Log("fuckscript");

            return null;
        }
    }
}