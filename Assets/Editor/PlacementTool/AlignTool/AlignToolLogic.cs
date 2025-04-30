using UnityEditor;
using UnityEngine;

namespace noeyToolkit
{
    public class AlignToolLogic
    {
        public static void AlignObjects(GameObject[] targets, float linearSpacing, bool reverse, AlignType type, AlignAxis axis, AlignOrigin origin, int rows = 1, int cols = 1, Vector2 spacing = default)
        {
            if (targets == null || targets.Length == 0) return;

            foreach (var target in targets)
            {
                Undo.RecordObject(target.transform, "Align Objects");
            }
            switch (type)
            {
                case AlignType.Linear:
                    ApplyLinearAlignment(targets, axis, origin, linearSpacing, reverse);
                    break;
                case AlignType.Center:
                    MoveToCenter(targets);
                    break;
                case AlignType.Grid:
                    ArrangeInGrid(targets, rows, cols, spacing);
                    break;
            }
        }

        private static void ApplyLinearAlignment(GameObject[] objects, AlignAxis axis, AlignOrigin origin, float spacing, bool reverse)
        {
            if (objects.Length <= 1) return;

            // 정렬 기준이 FirstSelected이면 첫 번째 객체의 위치를 기준으로 설정, 그렇지 않으면 평균 위치로 설정
            Vector3 reference = origin == AlignOrigin.FirstSelected
                ? objects[0].transform.position
                : GetAveragePosition(objects);

            // 각 객체의 위치를 계산하여 정렬
            for (int i = 0; i < objects.Length; i++)
            {
                Vector3 pos = reference;

                if (reverse)
                {
                    switch (axis)
                    {
                        case AlignAxis.X: pos.x -= spacing * i; break;
                        case AlignAxis.Y: pos.y -= spacing * i; break;
                        case AlignAxis.Z: pos.z -= spacing * i; break;
                    }
                }
                else
                {
                    switch (axis)
                    {
                        case AlignAxis.X: pos.x += spacing * i; break;
                        case AlignAxis.Y: pos.y += spacing * i; break;
                        case AlignAxis.Z: pos.z += spacing * i; break;
                    }
                }

                // 객체의 위치를 설정
                objects[i].transform.position = pos;
            }
        }

        // 선택된 객체들을 중앙으로 이동
        private static void MoveToCenter(GameObject[] objects)
        {
            Vector3 center = GetAveragePosition(objects);
            foreach (var obj in objects)
                obj.transform.position = center;
        }

        // Grid 정렬을 적용
        private static void ArrangeInGrid(GameObject[] objects, int rows, int cols, Vector2 spacing)
        {
            Vector3 start = objects[0].transform.position;
            int col = 0, row = 0;

            for (int i = 0; i < objects.Length; i++)
            {
                var obj = objects[i];
                Vector3 offset = new Vector3(col * spacing.x, 0f, -row * spacing.y);
                obj.transform.position = start + offset;

                col++;
                if (col >= cols)
                {
                    col = 0;
                    row++;
                }
            }
        }

        // 객체들의 평균 위치를 계산
        private static Vector3 GetAveragePosition(GameObject[] objects)
        {
            Vector3 sum = Vector3.zero;
            foreach (var obj in objects)
                sum += obj.transform.position;

            return sum / objects.Length;
        }
    }
}