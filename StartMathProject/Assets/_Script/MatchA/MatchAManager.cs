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

    //生成的物件
    List<GameObject> MoveItemObj= new List<GameObject>();
    List<GameObject> MatchPosItemObj=new List<GameObject>();

    MatchASourse MatchASourse;

    MoveItem[] m_MoveItems;
    MatchPosItem[] m_MatchPosItems;
    SceneSound m_SceneSound;
    OtherAnimObj m_otherAnimObj;

    GameObject m_OtherAnimObj = null;


    void Start () {
        //連接MatchASourse
        MatchASourse = GetComponent<MatchASourse>();
        m_MoveItems = MatchASourse.MoveItems;
        m_MatchPosItems = MatchASourse.MatchPosItems;
        m_SceneSound = MatchASourse.SceneSounds;
        m_otherAnimObj = MatchASourse.OtherAnimObjs;

        //判斷播放背景音
        PlaySceneMusic(m_SceneSound.BackgroundMusic, m_SceneSound.BackgroundMusicOnOff);
        //判斷播放開頭提示音效
        PlaySceneSound(m_SceneSound.TipSound, m_SceneSound.TipSoundOnOff);

        //生成場景物件
        InstanceMoveItem(m_MoveItems, moveItmeObj);
        InstanceMatchPosItem(m_MatchPosItems, matchPosItemObj);
        InstanceOtherAnimObj(m_otherAnimObj);

        //點擊check按鈕事件
        GameEventSystem.Instance.OnPushCheckBtn += CheckPass;
    }
	
    /// <summary>
    /// 播放背景音
    /// </summary>
    void PlaySceneMusic(AudioClip audioclip,bool isPlay)
    {
        if (isPlay == true)
        {
            AudioSource audioSound = gameObject.AddComponent<AudioSource>();
            audioSound.clip = audioclip;
            audioSound.Stop();
            audioSound.Play();
        }
    }

    /// <summary>
    /// 撥放音效
    /// </summary>
    void PlaySceneSound(AudioClip audioclip, bool isPlay)
    {
        if (isPlay == true)
        {
            AudioSource audioSound = gameObject.AddComponent<AudioSource>();
            audioSound.clip = audioclip;
            audioSound.Stop();
            audioSound.Play();
        }
    }

    /// <summary>
    /// (editor)生成移動物件
    /// </summary>
    /// <param name="_moveItems"></param>
    public void InstanceMoveItem(MoveItem[] _moveItems, GameObject objPrefab)
    {
        for(int i = 0; i < _moveItems.Length; i++)
        {
            MoveItemObj.Add(Instantiate(objPrefab));
            MoveItemObj[i].GetComponent<SpriteRenderer>().sprite = _moveItems[i].MoveItemSprite;
            MoveItemObj[i].transform.position = _moveItems[i].MoveItemPosition;
        }
    }

    /// <summary>
    /// (editor)生成連接位置物件
    /// </summary>
    /// <param name="_matchPosItems"></param>
    public void InstanceMatchPosItem(MatchPosItem[] _matchPosItems,GameObject objPrefab)
    {
        for (int i = 0; i < _matchPosItems.Length; i++)
        {
            MatchPosItemObj.Add(Instantiate(objPrefab));
            MatchPosItemObj[i].GetComponent<SpriteRenderer>().sprite = _matchPosItems[i].MatchPosItemSprite;
            MatchPosItemObj[i].transform.position = _matchPosItems[i].MatchPosItemPosition;
            //生成時給予要配對的圖片名稱
            MatchPosItemObj[i].GetComponent<MatchPosItemControl_matchA>().CorrectColliderObjName = _matchPosItems[i].CorrectMoveItemSpriteName.name;
            //生成時給予正確要生成的物件
            MatchPosItemObj[i].GetComponent<MatchPosItemControl_matchA>().CorrectObj = _matchPosItems[i].CorrectObj;
            //生成時給予正確物件生成位置
            MatchPosItemObj[i].GetComponent<MatchPosItemControl_matchA>().CorrectObjPos = _matchPosItems[i].CorrectObjPos;
        }
    }


    /// <summary>
    /// 生成額外動畫物件
    /// </summary>
    /// <param name="_otherAnimObj"></param>
    /// <param name="objPrefab"></param>
    public void InstanceOtherAnimObj(OtherAnimObj _otherAnimObj)
    {
        if (_otherAnimObj.OtherAnimObjOnOff == true)
        {
            m_OtherAnimObj = Instantiate(_otherAnimObj.OtherAnimObjPrefab, _otherAnimObj.OtherAnimObjPosition, Quaternion.identity);
            m_OtherAnimObj.GetComponent<Animator>().enabled = false;
        }          
    }
    /// <summary>
    /// 播放額外動畫
    /// </summary>
    void PlayOtherAnimObjAnim()
    {
        if(m_OtherAnimObj!=null) m_OtherAnimObj.GetComponent<Animator>().enabled = true;
    }

    /// <summary>
    /// 判斷有無過關
    /// </summary>
    void CheckPass()
    {
        List<MatchPosItemControl_matchA> m_MatchPosItemControl = new List<MatchPosItemControl_matchA>();
        foreach (var matchPosItemObj in MatchPosItemObj)
        {
            m_MatchPosItemControl.Add(matchPosItemObj.GetComponent<MatchPosItemControl_matchA>());
        }

        for (int i = 0 ; i< m_MatchPosItemControl.Count; i++)
        {
            //判斷所有的感應區內是否都有物件或超出數量
            if (m_MatchPosItemControl[i].OnCollidionrObjCount() == 0 || m_MatchPosItemControl[i].OnCollidionrObjCount() > 1)
            {
                Debug.Log("物件數量錯誤");
                return;
            }
            //判斷所有感應區物件名稱是否正確
            foreach (var onColliderObjName in m_MatchPosItemControl[i].OnCollisionObjName())
            {
                if (onColliderObjName.GetComponent<SpriteRenderer>().sprite.name != m_MatchPosItemControl[i].CorrectColliderObjName)
                {
                    Debug.Log("物件名稱錯誤");
                    return;
                }
            }
        }

        //正確
        for (int i = 0; i < m_MatchPosItemControl.Count; i++)
        {
            foreach (var onColliderObjName in m_MatchPosItemControl[i].OnCollisionObjName())
            {             
                if (onColliderObjName.GetComponent<SpriteRenderer>().sprite.name == m_MatchPosItemControl[i].CorrectColliderObjName)
                {
                    Debug.Log("正確");
                    //把感應區當成母物件
                    InstanceCorrectObj(m_MatchPosItemControl[i].CorrectObj, m_MatchPosItemControl[i].gameObject, m_MatchPosItemControl[i].CorrectObjPos);
                    //隱藏原本的sprite
                    //MatchPosItemObj[i].GetComponent<SpriteRenderer>().enabled = false;
                    MoveItemObj[i].GetComponent<SpriteRenderer>().enabled = false;
                    //判斷播放正確音效
                    PlaySceneSound(m_SceneSound.CorrectSound, m_SceneSound.CorrectSoundOnOff);
                    //播放額外動畫
                    PlayOtherAnimObjAnim();
                    //移除check按鈕事件
                    GameEventSystem.Instance.OnPushCheckBtn -= CheckPass;                   
                }
            }
        }       
    }

    /// <summary>
    /// 產生正確物件
    /// </summary>
    void InstanceCorrectObj(GameObject correctObj,GameObject parent, Vector3 correctObjVector)
    {
        GameObject obj = Instantiate(correctObj, parent.transform);
        obj.transform.localPosition = correctObjVector;
        obj.GetComponent<SpriteRenderer>().sortingOrder = 2;
    }

}
