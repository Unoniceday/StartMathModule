using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveItemControl : MonoBehaviour
{

    Vector3 m_OriginalPos;
    Vector3 m_DragStartPos;
    Vector3 m_DragEndPos;
    Vector3 m_offsetToMouse;
    MoveArea moveArea;

    GameObject m_ColliderObj;

    void Start()
    {
        m_OriginalPos = this.transform.position;
        moveArea = FindObjectOfType<MoveArea>();
        BoxCollider2D BCollider = this.gameObject.AddComponent<BoxCollider2D>();
        BCollider.isTrigger = true;
    }

    #region OnMouse
    private void OnMouseDown()
    {
        m_DragStartPos = transform.position;
        m_offsetToMouse = m_DragStartPos - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z)) + m_offsetToMouse;
        this.transform.SetParent(moveArea.transform);
    }

    private void OnMouseUp()
    {
        m_DragEndPos = transform.position;

        float moveDis = Vector3.Distance(m_DragStartPos, m_DragEndPos);
        if (moveDis < 0.2f)
        {//移動不夠回到原位
            this.transform.position = m_OriginalPos;
        }
        else if (m_ColliderObj != null && m_ColliderObj.tag == "matchPosItem")
        {//如果有碰到感應區，則移動到感應區位置
            this.transform.SetParent(m_ColliderObj.transform);
            transform.position = new Vector2(this.m_ColliderObj.transform.position.x, this.m_ColliderObj.transform.position.y + 1f);
               
        }
    }

    #endregion

    #region OnTrigger
    private void OnTriggerStay2D(Collider2D collision)
    {
        //判斷數量跟tag     
        if (collision.tag == "matchPosItem")
        {
            if (collision.GetComponent<MatchPosItemControl>().OnCollidionrObjCount() == 0)
            {
                m_ColliderObj = collision.gameObject;
            }
        }
        else
        {
            m_ColliderObj = moveArea.gameObject;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "matchPosItem")
        {
            m_ColliderObj = null;
        }
    }
    #endregion
}
