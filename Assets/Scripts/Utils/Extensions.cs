using System;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions {

    public static TComponent GetOrAddComponent<TComponent>(this GameObject gameObject)
            where TComponent : Component
    {
        TComponent component = gameObject.GetComponent<TComponent>();
        if (component == null)
        {
            component = gameObject.AddComponent<TComponent>();
        }
        return component;
    }

    public static float AreaFromRadius(this float rad)
    {
        return rad * rad * Mathf.PI;
    }

    public static TComponent[] GetComponentFromChildrenOnly<TComponent>(this GameObject gameObject)
            where TComponent : Component
    {
        var array = gameObject.GetComponentsInChildren<TComponent>();
        if (array.Length <= 1)
        {
            return null;
        }
        else
        {
            var returnValue = new TComponent[array.Length - 1];

            // This is used to remove the parent from available spawnPoints.
            var availableIndex = 0;
            foreach (var t in array)
            {
                if (t.gameObject.GetInstanceID() != gameObject.GetInstanceID())
                {
                    if (availableIndex >= returnValue.Length) break;
                    returnValue[availableIndex] = t;
                    availableIndex++;
                }
            }

            return returnValue;
        }
    }

    public static void SetLayer(this GameObject gameObject, int layer,
        bool includeChildren = true)
    {
        gameObject.layer = layer;
        if (includeChildren)
        {
            foreach (Transform transform in
                gameObject.transform.GetComponentsInChildren<Transform>())
            {
                transform.gameObject.layer = layer;
            }
        }
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (T item in source)
            action(item);
    }

    public static T Throw<T>(this Exception e)
    {
        throw e;
    }
}
