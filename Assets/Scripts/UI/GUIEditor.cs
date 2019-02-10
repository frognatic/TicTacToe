using UnityEditor;
using UnityEngine;

public class GUIEditor : MonoBehaviour
{
    [MenuItem("GUIEditor/Anchors to Corners")]
    private static void AnchorsToCorners()
    {
        var selectedTransforms = Selection.GetTransforms(SelectionMode.TopLevel);
        foreach (var activeTransform in selectedTransforms)
        {
            var t = activeTransform as RectTransform;
            var pt = activeTransform.parent as RectTransform;

            if (t == null || pt == null)
            {
                return;
            }

            var newAnchorsMin = new Vector2(t.anchorMin.x + t.offsetMin.x / pt.rect.width,
                t.anchorMin.y + t.offsetMin.y / pt.rect.height);
            var newAnchorsMax = new Vector2(t.anchorMax.x + t.offsetMax.x / pt.rect.width,
                t.anchorMax.y + t.offsetMax.y / pt.rect.height);

            t.anchorMin = newAnchorsMin;
            t.anchorMax = newAnchorsMax;
            t.offsetMin = t.offsetMax = new Vector2(0, 0);
        }
    }
}