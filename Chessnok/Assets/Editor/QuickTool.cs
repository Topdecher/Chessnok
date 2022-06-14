using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class QuickTool : EditorWindow
{
    [MenuItem("QuickTool/Open _%#T")]
    public static void ShowWindow()
    {
        // Opens the window, otherwise focuses it if it's already open.
        var window = GetWindow<QuickTool>();

        // Adds a title to the window.
        window.titleContent = new GUIContent("QuickTool");

        // Sets a minimum size to the window.
        window.minSize = new Vector2(280, 50);
    }
}