using System;
using UnityEngine;

/// <summary>友情コンボの発動に必要な情報をまとめたクラス。</summary>
[Serializable]
public class FriendComboContext
{
    [field: SerializeField] public Vector2 activatorPosition { get; private set; }//自分の座標
    [field: SerializeField] public Vector2 toucherPosition { get; private set; }//触れた相手の座標
    [field: SerializeField] public Vector2 touchDirection { get; private set; }//発動した方向
    [field: SerializeField] public float baseAttackPower { get; private set; }//攻撃バフ

    public FriendComboContext
        (
            Vector2 activatorPosition,
            Vector2 toucherPosition,
            Vector2 touchDirection,
            float baseAttackPower
        )
    {
        this.activatorPosition = activatorPosition;
        this.toucherPosition = toucherPosition;
        this.touchDirection = touchDirection.sqrMagnitude > 0f ? touchDirection.normalized : Vector2.zero;
        this.baseAttackPower = Mathf.Max(baseAttackPower, 0f);
    }
}
