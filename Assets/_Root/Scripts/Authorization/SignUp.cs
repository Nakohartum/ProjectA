using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Root.Scripts.Authorization
{
    public class SignUp : AuthorizationWindow
    {
        [SerializeField] private TMP_InputField _emailField;
        private string _email;
        protected override void InitializeComponents()
        {
            base.InitializeComponents();
            _emailField.onValueChanged.AddListener(ChangeEmail);
            _acceptButton.onClick.AddListener(CreateUser);
        }

        private void ChangeEmail(string email)
        {
            _email = email;
        }

        private void CreateUser()
        {
            StartWaiting();
            PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest()
            {
                Username = _username,
                Email = _email,
                Password = _password,
            }, RegistrationSuccessful, RegistrationError);
        }

        private void RegistrationError(PlayFabError error)
        {
            var errorMessage = error.GenerateErrorReport();
            Debug.Log($"Error: {errorMessage}");
            _isLoading = false;
        }

        private void RegistrationSuccessful(RegisterPlayFabUserResult result)
        {
            Debug.Log($"Player {result.Username} successfully registered");
            _isLoading = false;
            OpenLobby();
        }
    }
}