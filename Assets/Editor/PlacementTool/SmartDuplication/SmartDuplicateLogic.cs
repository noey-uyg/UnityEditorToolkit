using UnityEditor;
using UnityEngine;

namespace noeyToolkit
{
    public static class SmartDuplicateLogic
    {
        // 복제 작업 처리
        public static void Duplicate(int count, NumberingFormat format, Vector3 pos, Vector3 rot, Vector3 scale)
        {
            var selected = Selection.gameObjects;

            foreach (var obj in selected)
            {
                for (int i = 1; i <= count; i++)
                {
                    var copy = Object.Instantiate(obj, obj.transform.parent);
                    Undo.RegisterCreatedObjectUndo(copy, "Smart Duplicate");

                    // 오프셋 적용
                    copy.transform.localPosition = obj.transform.localPosition + pos * i;
                    copy.transform.localEulerAngles = obj.transform.localEulerAngles + rot * i;
                    copy.transform.localScale = obj.transform.localScale + scale * i;

                    copy.name = GenerateName(obj.name, i, format);
                }
            }
        }

        private static string GenerateName(string baseName, int index, NumberingFormat format)
        {
            switch (format)
            {
                case NumberingFormat.Underscore:
                    return $"{baseName}_{index:D2}";
                case NumberingFormat.Dash:
                    return $"{baseName}-{index}";
                case NumberingFormat.Dot:
                    return $"{baseName}.{index:D2}";
                case NumberingFormat.Hash:
                    return $"{baseName}#{index}";
                case NumberingFormat.LeadingZeros3:
                    return $"{baseName}{index:D3}";
                default:
                    return $"{baseName}({index})";
            }
        }
    }

}
