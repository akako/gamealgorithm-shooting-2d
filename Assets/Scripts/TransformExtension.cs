using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TransformExtension
{
    public static class TransformExtension
    {
        public static void LookAt2D(this Transform transform, Transform target)
        {
            var current = transform.position;
            var direction = target.position - current;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}