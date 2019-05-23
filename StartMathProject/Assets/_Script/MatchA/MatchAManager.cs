using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MatchASourse))]
public class MatchAManager : MonoBehaviour {

    [SerializeField]
    [Header("移動物件Prefab")]
    GameObject moveItmeObj;

    [SerializeField]
    [Header("配對物件prefab")]
    GameObject matchPosItemObj;

    MatchASourse MatchASourse;

    MoveItem[] moveItems;
    MatchPosItem[] matchPosItems;

    void Start () {
        MatchASourse = GetComponent<MatchASourse>();
        moveItems = MatchASourse.MoveItems;
        matchPosItems = MatchASourse.MatchPosItems;

        InstanceMoveItem(moveItems, moveItmeObj);
        InstanceMatchPosItem(matchPosItems, matchPosItemObj);
    }
	
	void Update () {
		
	}

    /// <summary>
    /// 生成移動物件
    /// </summary>
    /// <param name="_moveItems"></param>
    public void InstanceMoveItem(MoveItem[] _moveItems, GameObject objPrefab)
    {
        foreach (var item in _moveItems)
        {
            GameObject obj = Instantiate(objPrefab);
            obj.GetComponent<SpriteRenderer>().sprite = item.MoveItemSprite;
            obj.transform.position = item.MoveItemPosition;
        }
    }

    /// <summary>
    /// 生成連接位置物件
    /// </summary>
    /// <param name="_matchPosItems"></param>
    public void InstanceMatchPosItem(MatchPosItem[] _matchPosItems,GameObject objPrefab)
    {
        foreach (var item in _matchPosItems)
        {
            GameObject obj = Instantiate(objPrefab);
            obj.GetComponent<SpriteRenderer>().sprite = item.MatchPosItemSprite;
            obj.transform.position = item.MatchPosItemPosition;
        }
    }
}
