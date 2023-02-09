using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Esc : UI_Popup
{
    public void OnButtonClicked()
    {
        Managers.UI.ClosePopupUI();
    }


}
