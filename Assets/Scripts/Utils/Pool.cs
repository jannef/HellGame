﻿using System.Collections;
using fi.tamk.hellgame.character;
using UnityEditor;
using UnityEngine;

namespace fi.tamk.hellgame.utils
{
    using UnityEngine;
    using System.Collections.Generic;
    using System.Linq;

    namespace Stairs.Utils
    {
        /// <summary>
        /// Object pooling class for the game.
        /// </summary>
        public sealed class Pool : Singleton<Pool>
        {
            public Dictionary<GameObject, HealthComponent> GameObjectToHealth = new Dictionary<GameObject, HealthComponent>();

            public HealthComponent GetHealthComponent(GameObject go)
            {
                return !GameObjectToHealth.ContainsKey(go) ? null : GameObjectToHealth[go];
            }

            /// <summary>
            /// Pooled gameobjects.
            /// </summary>
            private readonly Dictionary<string, Queue<GameObject>> _pools = new Dictionary<string, Queue<GameObject>>();

            /// <summary>
            /// Prefab library for pooled objects.
            /// </summary>
            private readonly Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();

            /// <summary>
            /// Where to parent the pooled objects in the scene.
            /// 
            /// Null means they are on top level in hierarchy.
            /// </summary>
            private Transform _parent = null;

            /// <summary>
            /// Adds objects of given prefab type to the pool.
            /// </summary>
            /// <param name="prefab">Prefab to replicate the objects from.</param>
            /// <param name="count">How many copies to initially spawn.</param>
            /// <param name="parent">Where should these objects be parentet in hierarchy.</param>
            /// <returns></returns>
            public bool AddToPool(GameObject prefab, int count, Transform parent = null)
            {
                if (prefab == null || _prefabs.ContainsKey(prefab.name) || count < 1) return false;
                _parent = parent;
                _prefabs.Add(prefab.name, prefab);
                _pools.Add(prefab.name, new Queue<GameObject>());

                for (var i = 0; i < count; i++)
                {
                    var go = GetObject(prefab, true);
                    if (_parent != null) go.transform.parent = _parent;
                    if (go != null) ReturnObject(ref go);
                }

                return true;
            }

            /// <summary>
            /// Retrieves a a game object of a type from the pool.
            /// 
            /// If pool does not have enough objects
            /// </summary>
            /// <param name="prefab">Which type of object to retrieve.</param>
            /// <param name="forceNew">Should the object be always new instance, even if there is free excisting objects.</param>
            /// <returns></returns>
            public GameObject GetObject(GameObject prefab, bool forceNew = false)
            {
                if (!_prefabs.ContainsKey(prefab.name)) return null;

                if (!forceNew && _pools[prefab.name].Count >= 1)
                {
                    var rv = _pools[prefab.name].Dequeue();
                    rv.SetActive(true);
                    return rv;
                }

                var go = Instantiate(prefab);
                go.transform.parent = _parent;
                go.name = prefab.name;
                return go;
            }

            /// <summary>
            /// Returns given object to pool.
            /// </summary>
            /// <param name="obj">Object to return.</param>
            /// <param name="destroyObject">Should the returned object be disposed instead of returned to the pool.</param>
            public void ReturnObject(ref GameObject obj, bool destroyObject = false)
            {
                if (obj == null) return;

                if (destroyObject || !_prefabs.ContainsKey(obj.name))
                {
                    Destroy(obj);
                    return;
                }

                obj.SetActive(false);
                _pools[obj.name].Enqueue(obj);
            }

            /// <summary>
            /// Destroys a pool of objects.
            /// </summary>
            /// <param name="prefab">Prefab of pool of which type of objects to destroy.</param>
            public void DestroyPool(GameObject prefab)
            {
                if (!_prefabs.ContainsKey(prefab.name)) return;

                _prefabs.Remove(prefab.name);

                for (var i = 0; i < _pools[prefab.name].Count; i++)
                {
                    var go = _pools[prefab.name].Dequeue();
                    Destroy(go);
                }

                _pools.Remove(prefab.name);
            }

            /// <summary>
            /// Destroys all object pools.
            /// </summary>
            public void DestroyAllPools()
            {
                for (var i = 0; i < _pools.Count; i++)
                {
                    var t = _prefabs.First();
                    DestroyPool(t.Value);
                }
            }

            /// <summary>
            /// Calls initialization on start.
            /// </summary>
            protected override void Awake()
            {
                base.Awake();
                Initialization();
            }

            /// <summary>
            /// Calls initialization on demand.
            /// </summary>
            public void ReInitialize()
            {
                Initialization();
            }

            /// <summary>
            /// Initialization.
            /// </summary>
            private void Initialization()
            {
                
            }

            public static void DelayedDestroyGo(GameObject whichToDestroy)
            {
                Instance.StartCoroutine(DelayedDestroyCoroutine(whichToDestroy));
            }

            private static IEnumerator DelayedDestroyCoroutine(GameObject toDestroy, float delay = 1f)
            {
                toDestroy.SetActive(false);
                yield return new WaitForSecondsRealtime(delay);
                Destroy(toDestroy);
            }
        }
    }

}
