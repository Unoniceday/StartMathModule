using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveItemControl : MonoBehaviour
{

    Vector3 m_OriginalPos;
    Vector3 m_DragStartPos;
    Vector3 m_DragEndPos;
    Vector3 m_offsetToMouse;
    bool m_isUseMouse;

    void Start()
    {
        m_isUseMouse = true;
        m_OriginalPos = this.transform.position;
    }

    #region OnMouse
    private void OnMouseDown()
    {
        m_isUseMouse = true;
        m_DragStartPos = transform.position;
        m_offsetToMouse = m_DragStartPos - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z)) + m_offsetToMouse;
    }

    private void OnMouseUp()
    {
        m_isUseMouse = false;
        m_DragEndPos = transform.position;

        float moveDis = Vector3.Distance(m_DragStartPos, m_DragEndPos);
        if (moveDis < 0.2f) this.transform.position = m_OriginalPos;
    }

    #endregion

    private void OnTriggerStay2D(Collider2D collision)
    {
        MatchPosItemControl matchPosItemControl = collision.GetComponent<MatchPosItemControl>();
        if (matchPosItemControl != null)
        {
            if (matchPosItemControl.ObjInItemCount > 1)
            {
                return;
            }
        }
           

        if (!m_isUseMouse && collision.tag == "matchPosItem")
        {
            this.transform.position = collision.transform.position;
        }
        m_isUseMouse = true;

    }


}
