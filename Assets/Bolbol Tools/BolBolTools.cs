using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BolBolUtility
{
    namespace BolBolEditorTools
    {
        #if UNITY_EDITOR
        public class BolBolTools
        {
            /// <summary>
            /// use angle in radian
            /// </summary>
            /// <param name="lineWidth"></param>
            /// <param name="from"></param>
            /// <param name="to"></param>
            /// <param name="arrowLenght"></param>
            /// <param name="step"></param>
            /// <param name="angle"></param>            
            public static void DrawArrowLines(float lineWidth, Vector3 from, Vector3 to, float arrowLenght, float step, float angle)
            {
                Vector3 direction = (to - from).normalized;
                float distance = Vector3.Distance(from, to);
                for (float f = 0.3f; f < distance - 0.15f; f += step)
                {
                    Handles.DrawAAPolyLine(lineWidth, from + direction * f, from + (direction * (f - angle)) + Quaternion.AngleAxis(-45, direction) * Vector3.Cross(from, to).normalized * arrowLenght);
                    Handles.DrawAAPolyLine(lineWidth, from + direction * f, from + (direction * (f - angle)) + Quaternion.AngleAxis(45, direction) * Vector3.Cross(from, to).normalized * arrowLenght);
                }
            }

            public static void DrawArrowLines(float lineWidth, Vector3 from, Vector3 to, float arrowLenght, float step, float angle , float StraightLineWidth)
            {
                Vector3 direction = (to - from).normalized;
                float distance = Vector3.Distance(from, to);
                for (float f = 0.3f; f < distance - 0.15f; f += step)
                {
                    Handles.DrawAAPolyLine(lineWidth, from + direction * f, from + (direction * (f - angle)) + Quaternion.AngleAxis(-45, direction) * Vector3.Cross(from, to).normalized * arrowLenght);
                    Handles.DrawAAPolyLine(lineWidth, from + direction * f, from + (direction * (f - angle)) + Quaternion.AngleAxis(45, direction) * Vector3.Cross(from, to).normalized * arrowLenght);
                }

                Handles.DrawAAPolyLine(StraightLineWidth, from, to);
            }

            public static void DrawBoxColliderGizmo(MonoBehaviour BoxColliderObj, Color color , bool isWireCube)
            {
                Gizmos.color = color;

                BoxCollider2D boxCollider = BoxColliderObj.GetComponent<BoxCollider2D>();
                if (!boxCollider || !boxCollider.enabled) return;
                Vector2 Position = (Vector2)BoxColliderObj.transform.position + new Vector2(boxCollider.offset.x * BoxColliderObj.transform.localScale.x,
                    boxCollider.offset.y * BoxColliderObj.transform.localScale.y);
                Vector2 size = new Vector2(boxCollider.size.x * BoxColliderObj.transform.localScale.x,
                    boxCollider.size.y * BoxColliderObj.transform.localScale.y);
                if (isWireCube)
                {
                    Gizmos.DrawWireCube(Position, size);
                }
                else
                {
                    Gizmos.DrawCube(Position, size);
                }
            }

            public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = 5000)
            {
                if (color == null) color = Color.white;
                return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
            }

            public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
            {
                GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
                Transform transform = gameObject.transform;
                transform.SetParent(parent, false);
                transform.localPosition = localPosition;
                TextMesh textMesh = gameObject.GetComponent<TextMesh>();
                textMesh.anchor = textAnchor;
                textMesh.alignment = textAlignment;
                textMesh.text = text;
                textMesh.fontSize = fontSize;
                textMesh.color = color;
                textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
                return textMesh;
            }
        }
#endif
    }
 
}




