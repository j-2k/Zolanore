using UnityEngine;
using UnityEditor;
using System.Text;

[CreateAssetMenu(menuName = "Items/Item")]
public class Item : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }
    public string ItemName;
    public Sprite Icon;

    [Range(1,100)]
    public int MaxStack = 1;

    protected static readonly StringBuilder sb = new StringBuilder();

    protected static readonly StringBuilder sbLore = new StringBuilder();

    
    private void OnValidate() //change to awake later
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }
    

    public virtual Item GetCopy()
    {
        return this;
    }

    public virtual void Destroy()
    {

    }

    public virtual string GetItemType()
    {
        return "";
    }

    public virtual string GetDescription()
    {
        return "";
    }

    public virtual string GetDescriptionLore()
    {
        return "";
    }
}
