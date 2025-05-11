using UnityEditor;
using UnityEngine;

namespace noeyToolkit
{
    public class MirrorObjectLogic
    {
        public static void MirrorSelectedObjects(GameObject[] targets, Axis axis)
        {
            if(targets == null || targets.Length == 0)
            {
                Debug.LogError("[Mirror] No objects selected");
                return;
            }

            foreach(GameObject original in targets)
            {
                Transform transform = original.transform;

                GameObject mirrored = Object.Instantiate(original);
                mirrored.name = original.name + "_Mirrored";
                Undo.RegisterCreatedObjectUndo(mirrored, "Mirror Object");

                mirrored.transform.parent = transform.parent;
                mirrored.transform.position = GetMirroredPosition(transform.position, axis);
                mirrored.transform.rotation = GetMirroredRotation(transform.rotation, axis);
                mirrored.transform.localScale = GetMirroredScale(transform.localScale, axis);
            }
        }

        private static Vector3 GetMirroredPosition(Vector3 original, Axis axis)
        {
            switch (axis)
            {
                case Axis.X: return new Vector3(-original.x, original.y, original.z);
                case Axis.Y: return new Vector3(original.x, -original.y, original.z);
                case Axis.Z: return new Vector3(original.x, original.y, -original.z);
            }

            return original;

        }

        private static Quaternion GetMirroredRotation(Quaternion original, Axis axis)
        {
            Vector3 euler = original.eulerAngles;

            switch (axis)
            {
                case Axis.X:
                    euler.y = (360 - euler.y) % 360;
                    euler.z = (360 - euler.z) % 360;
                    break;
                case Axis.Y:
                    euler.x = (360 - euler.x) % 360;
                    euler.z = (360 - euler.z) % 360;
                    break;
                case Axis.Z:
                    euler.x = (360 - euler.x) % 360;
                    euler.y = (360 - euler.y) % 360;
                    break;
            }

            return Quaternion.Euler(euler);
        }

        private static Vector3 GetMirroredScale(Vector3 original, Axis axis)
        {
            switch (axis)
            {
                case Axis.X: original.x *= -1; break;
                case Axis.Y: original.y *= -1; break;
                case Axis.Z: original.z *= -1; break;
            }
            return original;
        }
    }
}
