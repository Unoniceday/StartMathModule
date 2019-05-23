using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchASourse : MonoBehaviour
{
    public MoveItem[] MoveItems;
    public MatchPosItem[] MatchPosItems;
}


[System.Serializable]
public class MoveItem
{
    public Sprite MoveItemSprite;
    public Vector2 MoveItemPosition;
}

[System.Serializable]
public class MatchPosItem
{
    public Sprite MatchPosItemSprite;
    public Vector2 MatchPosItemPosition;

}
