using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniWebViewHandler : MonoBehaviour
{
    [SerializeField] private GameObject _webViewObj;
    [SerializeField] private GameObject _closeWebViewBtn;

    public void OnClickOpenWebView()
    {
        _webViewObj.SetActive(true);
        _webViewObj.GetComponent<SampleWebView>().OpenWebView();
        _closeWebViewBtn.SetActive(true);
    }

    public void OnClickCloseWebView()
    {
        _webViewObj.SetActive(false);
        _closeWebViewBtn.SetActive(false);

    }
}
