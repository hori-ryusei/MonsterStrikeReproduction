using System;
using UnityEngine;

/// <summary>プレイヤーの実行時ステータスを管理する純粋なデータクラス。</summary>
[Serializable]
public class PlayerStatus
{
    private const int MinHp = 0;
    private const float MinDamageRate = 0f;

    private CharacterDataSO m_masterData;

    public const int DefaultMaxHp = 1;

    [field: SerializeField] public string characterName { get; private set; }
    [field: SerializeField] public int currentHp { get; private set; }
    [field: SerializeField] public int maxHp { get; private set; }
    [field: SerializeField] public float attackPower { get; private set; }
    [field: SerializeField] public float speed { get; private set; }

    public bool isDead => currentHp <= MinHp;//死亡フラグ
    public float hpRate => maxHp > 0 ? (float)currentHp / maxHp : 0f;//HP割合

    public PlayerStatus(CharacterDataSO _masterData)
    {
        if (_masterData == null)
        {
#if UNITY_EDITOR
            Debug.LogError("PlayerStatus: CharacterDataSO が null です。デフォルト値で初期化します。");
#endif
            characterName = string.Empty;
            maxHp = DefaultMaxHp;
            currentHp = DefaultMaxHp;
            attackPower = 0f;
            speed = 0f;
            return;
        }

        m_masterData = _masterData;
        characterName = _masterData.characterName;
        maxHp = _masterData.maxHp;
        currentHp = _masterData.maxHp;
        attackPower = _masterData.attackPower;
        speed = _masterData.speed;
    }

    /// <summary>現在のHPを範囲内に収める</summary>
    private void ClampHp()
    {
        currentHp = Mathf.Clamp(currentHp, MinHp, maxHp);
    }

    /// <summary>ダメージを受ける。実際に減少したHP量を返す。</summary>
    public int TakeDamage(int _damage)
    {
        if (_damage <= 0)
        {
            return 0;
        }

        int beforeHp = currentHp;
        currentHp -= _damage;
        ClampHp();

        return beforeHp - currentHp;
    }

    /// <summary>HPを回復する。実際に回復した量を返す。</summary>
    public int Heal(int _amount)
    {
        if (_amount <= 0)
        {
            return 0;
        }

        int beforeHp = currentHp;
        currentHp += _amount;
        ClampHp();

        return currentHp - beforeHp;
    }

    /// <summary>攻撃力と倍率から与ダメージを計算する。</summary>
    public int CalculateAttackDamage(float _damageRate = 1f)
    {
        float rate = Mathf.Max(_damageRate, MinDamageRate);
        return Mathf.RoundToInt(attackPower * rate);
    }

    /// <summary>HPを最大値まで全回復する。</summary>
    public void ResetHp()
    {
        currentHp = maxHp;
    }

    /// <summary>マスターデータの値でステータスを再初期化する。</summary>
    public void ResetStatus()
    {
        if (m_masterData == null)
        {
            return;
        }

        characterName = m_masterData.characterName;
        maxHp = m_masterData.maxHp;
        currentHp = m_masterData.maxHp;
        attackPower = m_masterData.attackPower;
        speed = m_masterData.speed;
    }

    /// <summary>CharacterDataSO から PlayerStatus を生成するファクトリ。</summary>
    public static PlayerStatus Create(CharacterDataSO _masterData)
    {
        return new PlayerStatus(_masterData);
    }
}