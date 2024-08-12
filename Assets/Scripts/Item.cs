
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName ="Item/Create New Item")]

[System.Serializable]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int price;
    public Sprite icon;
    public Transform prefab;
    

}
