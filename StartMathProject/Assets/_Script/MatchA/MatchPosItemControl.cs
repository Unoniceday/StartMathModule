using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPosItemControl : MonoBehaviour {

    //在配對位置物件裡的移動物件數量
    [HideInInspector]
    public int ObjInItemCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "moveItem") ObjInItemCount++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "moveItem") ObjInItemCount--;
    }

}
