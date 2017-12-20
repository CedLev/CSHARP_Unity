using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObject/CharacterData", order = 1)]
public class CharacterSO : ScriptableObject
{
    public int hitPoint;
    public float moveSpeed;

}
