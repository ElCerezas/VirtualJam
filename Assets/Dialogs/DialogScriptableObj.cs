using UnityEngine;

[CreateAssetMenu(fileName = "DialogScriptableObj", menuName = "Scriptable Objects/DialogScriptableObj")]
public class DialogScriptableObj : ScriptableObject
{
    [SerializeField] Sprite Background, Character;
    [TextArea(4, 10)]
    [SerializeField] string Dialog;
    [SerializeField] string Name;
    [SerializeField] DialogScriptableObj nextDialog;


    public string GetDialog()
    {
        return Dialog;
    }
    public DialogScriptableObj NextDialog()
    {
        return nextDialog;
    }
    public string GetName()
    {
        return Name;
    }
    public Sprite GetBackground()
    {
        return Background;
    }
    public Sprite GetCharacter()
    {
        return Character;
    }
}
