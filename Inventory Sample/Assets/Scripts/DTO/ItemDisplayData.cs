using UnityEngine;

public sealed class ItemDisplayData
{
    public string Name { get; }
    public ItemGrade Grade { get; }
    public ItemType Type { get; }
    public Sprite GradeSprite { get; }
    public Sprite IconSprite { get; }

    public ItemDisplayData(string name, ItemGrade grade, ItemType type, Sprite gradeSprite, Sprite iconSprite)
    {
        Name = name;
        Grade = grade;
        Type = type;
        GradeSprite = gradeSprite;
        IconSprite = iconSprite;
    }
}