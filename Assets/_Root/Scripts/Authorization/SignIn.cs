using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Root.Scripts.Authorization
{
    public class SignIn : AuthorizationWindow
    {
        protected override void InitializeComponents()
        {
            base.InitializeComponents();
            _acceptButton.onClick.AddListener(LoginUser);
        }

        private void LoginUser()
        {
            StartWaiting();
            PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest()
            {
                Username = _username,
                Password = _password
            }, LoginSuccessful, LoginError);
        }

        private void LoginError(PlayFabError error)
        {
            var errorMessage = error.GenerateErrorReport();
            Debug.Log($"Error: {errorMessage}");
            _isLoading = false;
        }

        private void LoginSuccessful(LoginResult result)
        {
            Debug.Log($"Player {result.PlayFabId} successfully logged in");
            _isLoading = false;
            OpenLobby();
        }
    }
}