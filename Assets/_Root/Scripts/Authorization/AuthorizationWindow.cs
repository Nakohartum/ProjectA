using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Root.Scripts.Authorization
{
    public class AuthorizationWindow : MonoBehaviour
    {
        [SerializeField] protected Button _backButton;
        [SerializeField] protected Button _acceptButton;
        [SerializeField] protected TMP_InputField _usernameField;
        [SerializeField] protected TMP_InputField _passwordField;
        [SerializeField] protected Image _loadingImage;
        [field : SerializeField] public Canvas Canvas { get; private set; }
        [HideInInspector] public Canvas PreviouslyOpened;

        protected string _username;
        protected string _password;
        protected bool _isLoading = false;

        private void Start()
        {
            InitializeComponents();
        }

        protected void StartWaiting()
        {
            _isLoading = true;
            StartCoroutine(LoadWait());
        }

        private IEnumerator LoadWait()
        {
            while (_isLoading)
            {
                _loadingImage.transform.Rotate(Vector3.forward * Time.deltaTime * 50);
                yield return null;
            }
            _loadingImage.transform.rotation = Quaternion.identity;
        }

        protected virtual void InitializeComponents()
        {
            _backButton.onClick.AddListener(GoBack);
            _usernameField.onValueChanged.AddListener(SetUsername);
            _passwordField.onValueChanged.AddListener(SetPassword);
        }

        private void SetPassword(string password)
        {
            _password = password;
        }

        private void SetUsername(string username)
        {
            _username = username;
        }

        private void GoBack()
        {
            PreviouslyOpened.enabled = true;
            Canvas.enabled = false;
        }
    }
}