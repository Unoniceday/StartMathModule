using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchASourse : MonoBehaviour
{
    public SceneSound SceneSounds;
    public MoveItem[] MoveItems;
    public MatchPosItem[] MatchPosItems;
    public OtherAnimObj OtherAnimObjs;
}

[System.Serializable]
public class SceneSound
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
public class MoveItem
{
    [Header("拖曳物件的圖片/位置")]
    public Sprite MoveItemSprite;
    public Vector2 MoveItemPosition;
}

[System.Serializable]
public class MatchPosItem
{
    [Header("感應區的圖片/位置")]
    public Sprite MatchPosItemSprite;
    public Vector2 MatchPosItemPosition;
    [Header("正確物件圖片名字/產生的正確動畫prefab/產生位置(相對母物件)")]
    public Sprite CorrectMoveItemSpriteName;
    public GameObject CorrectObj;
    public Vector2 CorrectObjPos;
}

[System.Serializable]
public class OtherAnimObj
{
    [Header("感應區的開關/圖片/位置")]
    public bool OtherAnimObjOnOff;
    public GameObject OtherAnimObjPrefab;
    public Vector2 OtherAnimObjPosition;

}

