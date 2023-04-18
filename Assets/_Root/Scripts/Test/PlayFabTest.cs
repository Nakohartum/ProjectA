using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayFabTest : MonoBehaviour
{
    
    [SerializeField] private Button _connectToPlayFabServer;
    [SerializeField] private TextMeshProUGUI _playFabLabel;
    private void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = "7DEE2";
        }
    }

    private void OnEnable()
    {
        _connectToPlayFabServer.onClick.AddListener(Connect);
    }

    private void OnDisable()
    {
        _connectToPlayFabServer.onClick.RemoveAllListeners();
    }

    private void Connect()
    {
        var request = new LoginWithCustomIDRequest()
        {
            CustomId = "GeekBrainsLesson3", 
            CreateAccount = true
        };
        
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made successful API call");
        _playFabLabel.text = "Congratulations, you made successful API call";
        _playFabLabel.color = Color.green;
    }

    private void OnLoginFailure(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.Log($"Something went wrong: {errorMessage}");
        _playFabLabel.text = $"Something went wrong: {errorMessage}";
        _playFabLabel.color = Color.red;
    }
}
