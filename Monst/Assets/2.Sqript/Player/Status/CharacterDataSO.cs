using UnityEngine;

/// <summary>キャラクターのマスターデータ。</summary>
[CreateAssetMenu(fileName = "CharacterData", menuName = "Game/CharacterData")]
public class CharacterDataSO : ScriptableObject
{
    //---最低値の保証---
    private const int MinMaxHp = 1;
    private const float MinStatusValue = 0f;

    // --- 基本情報 ---
    [field: SerializeField] public int characterId { get; private set; }
    [field: SerializeField] public string characterName { get; private set; }

    // --- 基本ステータス ---
    [field: SerializeField] public int maxHp { get; private set; } = 1000;
    [field: SerializeField] public float attackPower { get; private set; } = 100f;
    [field: SerializeField] public float speed { get; private set; } = 30f;

    // --- 見た目用アセット ---
    [field: SerializeField] public Sprite ballSprite { get; private set; }
    [field: SerializeField] public Sprite iconSprite { get; private set; }
    [field: SerializeField] public Sprite ssCutInSprite { get; private set; }

    private void OnValidate()
    {
        //不正値が入力されないよう補正する
        if (maxHp < MinMaxHp)maxHp = MinMaxHp;

        if (attackPower < MinStatusValue) attackPower = MinStatusValue;

        if (speed < MinStatusValue) speed = MinStatusValue;
    }
}