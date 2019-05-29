using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchBSourse : MonoBehaviour {

    public SceneSound_matchB SceneSounds;
    public MoveItem_matchB[] MoveItems;
    public MatchPosItem_matchB[] MatchPosItems;
    public OtherAnimObj_matchB OtherAnimObjs;
}

[System.Serializable]
public class SceneSound_matchB
{
    [Header("背景音樂")]
    public bool BackgroundMusicOnOff;
    public AudioClip BackgroundMusic;

    [Header("播放提示音效")]
    public bool TipSoundOnOff;
    public AudioClip TipSound;

    [Header("正確音效")]
    public bool CorrectSoundOnOff;
    public AudioClip CorrectSound;
}

[System.Serializable]
public class MoveItem_matchB
{
    [Header("拖曳物件的圖片/位置")]
    public Sprite MoveItemSprite;
    public Vector2 MoveItemPosition;
}


[System.Serializable]
public class MatchPosItem_matchB
{
    [Header("感應區的圖片/位置/是否僅使用collider(不顯示圖片)")]
    public Sprite MatchPosItemSprite;
    public Vector2 MatchPosItemPosition;
    public bool IsOnlyCollider;
    [Header("正確物件圖片名字/產生的正確動畫prefab/產生位置(相對母物件-拖曳物件)")]
    public CorrectMoveItem[] CorrectMoveItems;


    [System.Serializable]
    public class CorrectMoveItem
    {
        public Sprite CorrectMoveItemSpriteName;
        public GameObject CorrectObj;
        public Vector2 CorrectObjPos;
    }
}


[System.Serializable]
public class OtherAnimObj_matchB
{
    [Header("額外動畫物件的開關/圖片/位置")]
    public bool OtherAnimObjOnOff;
    public GameObject OtherAnimObjPrefab;
    public Vector2 OtherAnimObjPosition;

}
