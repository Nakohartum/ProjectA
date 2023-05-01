using System;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

namespace _Root.Scripts.Authorization
{
    public class PlayFabAccountManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textField;

        private void Start()
        {
            PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetAccountSuccess, OnGetAccountFailure);
        }

        private void OnGetAccountFailure(PlayFabError error)
        {
            _textField.text = $"Error occured {error.GenerateErrorReport()}";
            _textField.color = Color.red;
        }

        private void OnGetAccountSuccess(GetAccountInfoResult result)
        {
            _textField.text = $"Player {result.AccountInfo.Username} entered the game";
            _textField.color = Color.green;
        }
    }
}