using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Root.Scripts.Test
{
    public class CreateCharacterWindow : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _nameInput;
        [SerializeField] private Button _createButton;
        [SerializeField] private CharacterSelectorManager _characterSelectorManager;

        private void OnEnable()
        {
            _createButton.onClick.AddListener(CreateCharacter);
        }

        private void CreateCharacter()
        {
            PlayFabClientAPI.GrantCharacterToUser(new GrantCharacterToUserRequest()
            {
                CharacterName = _nameInput.text,
                ItemId = "character_token"
            }, result =>
            {
                UpdateCharacterStatistics(result.CharacterId);
                Debug.Log("Success");
            }, error =>
            {
                Debug.Log(error.ErrorMessage);
            });
        }

        private void UpdateCharacterStatistics(string characterID)
        {
            PlayFabClientAPI.UpdateCharacterStatistics(new UpdateCharacterStatisticsRequest()
            {
                CharacterId = characterID,
                CharacterStatistics = new Dictionary<string, int>()
                {
                    {"Gold", 0},
                    {"Level", 1},
                    {"Health", 100},
                    {"Damage", 3}
                }
            }, result =>
            {
                gameObject.SetActive(false);
                _characterSelectorManager.UpdateCharactersInfo();
            }, error =>
            {
                Debug.Log(error.GenerateErrorReport());
            });
        }
    }
}