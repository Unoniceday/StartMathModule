using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PublicUIComp : MonoBehaviour {

    [SerializeField]
    Button CheckBtn;

    [SerializeField]
    Button MuteBtn;
	
	void Awake () {
        CheckBtn.onClick.AddListener(delegate { if (GameEventSystem.Instance.OnPushCheckBtn != null) GameEventSystem.Instance.OnPushCheckBtn(); });
        MuteBtn.onClick.AddListener(delegate { if (GameEventSystem.Instance.OnPushMuteBtn != null) GameEventSystem.Instance.OnPushMuteBtn(); });
    }

}
