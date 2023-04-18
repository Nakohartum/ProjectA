using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Root.Scripts.Authorization
{
    public class MainWindow : MonoBehaviour
    {
        [SerializeField] private Canvas _optionsCanvas;
        [SerializeField] private SignIn _signInCanvas;
        [SerializeField] private SignUp _signUpCanvas;
        [SerializeField] private Button _signInButton;
        [SerializeField] private Button _signUpButton;

        private void OnEnable()
        {
            _signInButton.onClick.AddListener(OpenSignIn);
            _signUpButton.onClick.AddListener(OpenSignUp);
        }

        private void OpenSignUp()
        {
            var previouslyOpened = GetComponent<Canvas>();
            _signInCanvas.Canvas.enabled = true;
            _signInCanvas.PreviouslyOpened = previouslyOpened;
            previouslyOpened.enabled = false;
        }

        private void OpenSignIn()
        {
            var previouslyOpened = GetComponent<Canvas>();
            _signUpCanvas.Canvas.enabled = true;
            _signUpCanvas.PreviouslyOpened = previouslyOpened;
            previouslyOpened.enabled = false;
        }
    }
}