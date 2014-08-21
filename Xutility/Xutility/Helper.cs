using UnityEngine;
using System.Collections;

namespace XUtility
{
/// <summary>
/// Helper.
/// </summary>
    public class Helper
    {
        /// <summary>
        /// Gets the component.
        /// </summary>
        /// <returns>The component.</returns>
        /// <param name="go">Go.</param>
        /// <param name="add">will auto add T if the go has not T, when the add is ture.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        static public T GetComponent<T>(GameObject go, bool add) where T : Component
        {
            if (go == null)
                return null;

            T t = go.GetComponent<T>();

            if (add && t == null)
                t = go.AddComponent<T>();

            return t;
        }

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="obj">Object.</param>
        static public void DestroyObject(Object obj)
        {
            if (obj == null)
                return;

            if (Application.isEditor)
                GameObject.DestroyImmediate(obj);
            else
                GameObject.Destroy(obj);
        }

        /// <summary>
        /// Destroies the children.
        /// </summary>
        /// <param name="transform">Transform.</param>
        static public void DestroyChildren(Transform transform)
        {
            if (transform == null)
                return;
            int count = transform.childCount;
            for(int i = 0; i < count; i++)
            {
                GameObject.Destroy(transform.GetChild(i).gameObject);
            }
        }

        /// <summary>
        /// Equal the specified obj1 and obj2.
        /// deep diff.
        /// </summary>
        /// <param name="obj1">Obj1.</param>
        /// <param name="obj2">Obj2.</param>
        static public bool EqualInt(int[] obj1, int[] obj2)
        {
            return Equal<int>(obj1, obj2);
        }

        /// <summary>
        /// Equals the vector3.
        /// </summary>
        /// <returns><c>true</c>, if vector3 was equaled, <c>false</c> otherwise.</returns>
        /// <param name="v1">V1.</param>
        /// <param name="v2">V2.</param>
        static public bool EqualVector3(Vector3[] v1, Vector3[] v2)
        {
            return Equal<Vector3>(v1, v2);
        }

        /// <summary>
        /// Equal the specified obj1 and obj2.
        /// </summary>
        /// <param name="obj1">Obj1.</param>
        /// <param name="obj2">Obj2.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        static public bool Equal<T>(T[] obj1, T[] obj2)
        {
            if (obj1 == null || obj2 == null)
                return obj1 == obj2;
        
            if (obj1.Length != obj2.Length)
                return false;
        
            for (int i = 0; i < obj1.Length; i++)
            {
                if (!obj1 [i].Equals(obj2 [i]))
                    return false;
            }
        
            return true;
        }

        /// <summary>
        /// Sets the active self.
        /// </summary>
        /// <param name="go">Go.</param>
        /// <param name="val">If set to <c>true</c> value.</param>
        static public void SetActiveSelf(GameObject go, bool val)
        {
            if (go == null)
                return;
            if (go.activeSelf != val)
                go.SetActive(val);
        }

        /// <summary>
        /// Sets the active.
        /// </summary>
        /// <param name="go">Go.</param>
        /// <param name="val">If set to <c>true</c> value.</param>
        static public void SetActive(GameObject go, bool val)
        {
            SetActiveSelf(go, val);
            for (int i = 0, max = go.transform.childCount; i < max; i++)
                SetActive(go.transform.GetChild(i).gameObject, val);
        }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <returns>The full name.</returns>
        /// <param name="transform">Transform.</param>
        static public string GetFullName(Transform transform)
        {
            if (transform == null)
                return "";

            string name = transform.name;

            Transform parent = transform.parent;
            while (parent != null)
            {
                name = parent.name + "/" + name;
                parent = parent.parent;
            }

            name = "/" + name;

            return name;

        }

        /// <summary>
        /// Gets the component in children.
        /// </summary>
        /// <returns>The component in children.</returns>
        /// <param name="go">Go.</param>
        /// <param name="includeInactive">include Inactive</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        static public T GetComponentInChildren<T>(GameObject go, bool includeInactive) where T : Component
        {
            if (go == null)
                return null;
            if (!includeInactive)
                return go.GetComponentInChildren<T>();

            T t = go.GetComponent<T>();

            if (t != null)
                return t;

            for (int i = 0, max = go.transform.childCount; i < max; i++)
            {
                t = GetComponentInChildren<T>(go.transform.GetChild(i).gameObject, includeInactive);
                if(t != null)
                    return t;
            }

            return null;
        }

        /// <summary>
        /// Gets the component in parent.
        /// </summary>
        /// <returns>The component in parent.</returns>
        /// <param name="go">Go.</param>
        /// <param name="includeInactive">If set to <c>true</c> include inactive.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        static public T GetComponentInParent<T>(GameObject go, bool includeInactive) where T : Component
        {
            if (go == null)
                return null;

            T t = null;

            Transform parent = go.transform;

            while(parent != null)
            {
                t = parent.GetComponent<T>();
                if(t != null)
                    return t;

                parent = parent.transform.parent;
            }
            
            return null;
        }

        /// <summary>
        /// Bezier the specified v and t.
        /// </summary>
        /// <param name="v">V.</param>
        /// <param name="t">T.</param>
        static public Vector3 Bezier(Vector3[] v, float t)
        {
            if (v == null || v.Length < 3)
                return Vector3.zero;
            return Bezier(v [0], v [1], v [2], t);
        }

        /// <summary>
        /// Bezier the specified one, two, three and t.
        /// </summary>
        /// <param name="one">One.</param>
        /// <param name="two">Two.</param>
        /// <param name="three">Three.</param>
        /// <param name="t">T.</param>
        static public Vector3 Bezier(Vector3 one, Vector3 two, Vector3 three, float t)
        {
            t = Mathf.Clamp01(t);
           return (1-t) * (1 - t) * one + 2*t*(1-t) *two + three * t * t;
        }

        /// <summary>
        /// Fixto the specified transform, parent and normalize.
        /// 如果是normalize将position ratation 和scale都将normalize.
        /// </summary>
        /// <param name="transform">Transform.</param>
        /// <param name="parent">Parent.</param>
        /// <param name="normalize">If set to <c>true</c> is normal.</param>
        static public void Fixto(Transform transform, Transform parent, bool normalize)
        {
            if (transform == null)
                return;

            if(parent != null)
                transform.parent = parent;

            if (normalize)
            {
                transform.localScale = Vector3.one;
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
            }
        }

       /// <summary>
       /// Instantiate the specified original, parent, curve and t.
       /// </summary>
       /// <param name="original">Original.</param>
       /// <param name="parent">Parent.</param>
       /// <param name="curve">Curve.</param>
       /// <param name="t">T.</param>
        static public GameObject Instantiate(Object original, Transform parent, Curve curve, float t)
        {
            if (original == null || curve == null)
                return null;

            Vector3 pos = curve.GetPoint(t);

            GameObject go = GameObject.Instantiate(original) as GameObject;
            Fixto(go.transform, parent,true);
            go.transform.localPosition = pos;

            return go;
        }

        /// <summary>
        /// Finds the child.
        /// </summary>
        /// <returns>The child.</returns>
        /// <param name="parent">Parent.</param>
        /// <param name="name">Name.</param>
        /// <param name="deep">If set to <c>true</c> deep.</param>
        static public Transform FindChild(Transform parent, string name, bool deep)
        {
            if (parent == null || string.IsNullOrEmpty(name))
                return null;

            Transform t = parent.Find(name);

            if (t != null || !deep)
                return t;

            for(int i = 0, max = parent.childCount; i < max; i++)
            {
                t = FindChild(parent.GetChild(i),name,deep);
                if(t != null)
                    return t;
            }

            return null;
        }

        /// <summary>
        /// Distance the specified a, b and ignoreY.
        /// </summary>
        /// <param name="a">The alpha component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="ignoreZ">If set to <c>true</c> ignore y.</param>
        static public float Distance(Vector3 a, Vector3 b, bool ignoreZ)
        {
            if (ignoreZ)
                a.z = b.z;

            return Vector3.Distance(a,b);
        }

       /// <summary>
       /// Hit the specified pos, nextPos, height, center and size.
       /// </summary>
       /// <param name="pos">Position.</param>
       /// <param name="nextPos">Next position.</param>
       /// <param name="height">Height.</param>
       /// <param name="center">Center.</param>
       /// <param name="size">Size.</param>
        static public bool EnterRegion(Vector3 pos, Vector3 nextPos, float height, Vector3 center, Vector3 size)
        {
            Vector3 dis = pos - center;
            
            if (dis.x >= -size.x && dis.x <= size.x)
            {
                if (dis.y <= size.y && dis.y >= (-size.y - height))
                {
                    return true;
                }
            } else
            {
                if (size.x == 0)
                {
                    if(pos.x < center.x && nextPos.x >= center.x)
                    {
                        if (dis.y <= size.y && dis.y >= (-size.y - height))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}