using UnityEditor;
using UnityEditor.UI;
using UnityEditor.UIElements;
using UnityEngine.UIElements;


[CustomEditor(typeof(ButtonInheritance))]
internal class CustomButtonData : ButtonEditor
{
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new();

        PropertyField customComponent = new(serializedObject.FindProperty(ButtonInheritance.CustomComponent));
        PropertyField interactibleProperty = new(serializedObject.FindProperty("m_Interactable"));


        Label tweenLabel = new("Custom");
        Label intractableLabel = new("Interactable");
        
        root.Add(tweenLabel);
        root.Add(customComponent);

        root.Add(intractableLabel);
        root.Add(interactibleProperty);

        return root;
    }
}