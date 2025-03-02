using UnityEngine;

[CreateAssetMenu(fileName = "DialogScriptableObj", menuName = "Scriptable Objects/DialogScriptableObj")]
public class DialogScriptableObj : ScriptableObject
{
    public Sprite Background, Character, TextImage;
    
    [SerializeField] string Name;
    [TextArea(4, 10)]
    [SerializeField] string Dialog;

    public float writtingSpeed = 0.5f;
    public AudioClip audio;
    public bool loopAudio = true;
    public bool fadeOut;
    
    public string GetDialog()
    {
        return Dialog;
    }

    public string GetName()
    {
        return Name;
    }
}
