using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISettingsMenu : MonoBehaviour
{

    public void OnClickOpenSettings()
    {
        gameObject.SetActive(true);
    }

    public void OnClickCloseSettings()
    {
        gameObject.SetActive(false);
    }
}
