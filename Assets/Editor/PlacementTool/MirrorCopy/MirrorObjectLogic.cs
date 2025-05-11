using UnityEditor;
using UnityEngine;

namespace noeyToolkit
{
    public class MirrorObjectLogic
    {
        public static void MirrorSelectedObjects(GameObject[] targets, Axis axis, MirrorPivotMode pivotMode)
        {
            if(targets == null || targets.Length == 0)
            {
                Debug.LogError("[Mirror] No objects selected");
                return;
            }

            bool usePivot = pivotMode != MirrorPivotMode.IndividualOrigins;
            Vector3 pivot = Vector3.zero;
            
            if(pivotMode == MirrorPivotMode.SelectionCenter)
            {
                pivot = GetSelectionCenter(targets);
            }

            foreach (GameObject original in targets)
            {
                Transform transform = original.transform;

                GameObject mirrored = Object.Instantiate(original);
                mirrored.name = original.name + "_Mirrored";
                Undo.RegisterCreatedObjectUndo(mirrored, "Mirror Object");

                mirrored.transform.parent = transform.parent;
                mirrored.transform.position = GetMirroredPosition(transform.position, axis, pivot, usePivot);
                mirrored.transform.rotation = GetMirroredRotation(transform.rotation, axis);
                mirrored.transform.localScale = GetMirroredScale(transform.localScale, axis);
            }
        }

        private static Vector3 GetSelectionCenter(GameObject[] targets)
        {
            Vector3 center = Vector3.zero;

            foreach(GameObject obj in targets)
            {
                center += obj.transform.position;
            }

            return center / targets.Length;
        }

        private static Vector3 GetMirroredPosition(Vector3 original, Axis axis, Vector3 pivot, bool usePivot)
        {
            Vector3 offset = usePivot ? original - pivot : original;

            switch (axis)
            {
                case Axis.X: offset.x *= -1; break;
                case Axis.Y: offset.y *= -1; break;
                case Axis.Z: offset.z *= -1; break;
            }

            return usePivot ? pivot + offset : offset;
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
