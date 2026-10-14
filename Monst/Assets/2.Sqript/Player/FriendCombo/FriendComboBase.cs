using System;
using UnityEngine;

/// <summary>友情コンボのベースクラス。</summary>
[Serializable]
public abstract class FriendComboBase
{
    private const float MinComboPower = 0f;

    public const float DefaultComboPower = 1f;

    [field: SerializeField] public string comboName { get; protected set; }
    [field: SerializeField] public float comboPower { get; protected set; }
    public bool isTriggered { get; private set; }

    protected FriendComboBase(string comboName, float comboPower = DefaultComboPower)
    {
        this.comboName = comboName;
        this.comboPower = Mathf.Max(comboPower, MinComboPower);
        isTriggered = false;
    }

    /// <summary>各コンボ固有の挙動。comboDamage は攻撃力と威力倍率を反映済みの値</summary>
    protected abstract void ExecuteCombo(FriendComboContext context, float comboDamage);

    /// <summary>
    /// 味方に触れられた際に外部から呼び出す発動メソッド。
    /// 発動済みの場合は何もせず false を返す。発動した場合は true を返す。
    /// </summary>
    public bool TriggerCombo(FriendComboContext context)
    {
        if (context == null || isTriggered)
        {
            return false;
        }

        isTriggered = true;

        float comboDamage = CalculateComboDamage(context.baseAttackPower, comboPower);
        ExecuteCombo(context, comboDamage);

        return true;
    }

    /// <summary>発動済みフラグをリセットする。ターン終了時などに外部から呼ぶ。</summary>
    public void ResetTrigger()
    {
        isTriggered = false;
    }

    /// <summary>実際に与えるダメージ</summary>
    public static float CalculateComboDamage(float baseAttackPower, float comboPower)
    {
        return Mathf.Max(baseAttackPower, 0f) * Mathf.Max(comboPower, MinComboPower);
    }
}