using System;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Root.Scripts.Test
{
    public class CharacterSelectorContainer : MonoBehaviour
    {
        [SerializeField] private GameObject _nullCharacter;
        [SerializeField] private GameObject _hasCharacter;
        [SerializeField] private TMP_Text _nameLabel;
        [SerializeField] private TMP_Text _levelLabel;
        [SerializeField] private TMP_Text _goldLabel;
        [SerializeField] private TMP_Text _healthLabel;
        [SerializeField] private TMP_Text _damageLabel;
        [SerializeField] private Button _createCharacterButton;
        [SerializeField] private Button _selectCharacterButton;
        private CreateCharacterWindow _createCharacterObject;

        private void OnEnable()
        {
            _createCharacterButton.onClick.AddListener(CreateCharacter);
            _selectCharacterButton.onClick.AddListener(SelectCharacter);
        }

        private void OnDisable()
        {
            _createCharacterButton.onClick.RemoveAllListeners();
            _selectCharacterButton.onClick.RemoveAllListeners();
        }

        private void SelectCharacter()
        {
            
        }

        private void CreateCharacter()
        {
            _createCharacterObject.gameObject.SetActive(true);
        }

        public void Initialize(CharacterResult character, CreateCharacterWindow createCharacterObject)
        {

            if (character != null)
            {
                PlayFabClientAPI.GetCharacterStatistics(new GetCharacterStatisticsRequest()
                {
                    CharacterId = character.CharacterId
                }, result =>
                {
                    _nameLabel.text = $"{character.CharacterName}";
                    _levelLabel.text = $"Level: {result.CharacterStatistics["Level"]}";
                    _goldLabel.text = $"Gold: {result.CharacterStatistics["Gold"]}";
                    _healthLabel.text = $"HP: {result.CharacterStatistics["Health"]}";
                    _damageLabel.text = $"Damage: {result.CharacterStatistics["Damage"]}";
                    SetCharacter();
                }, error =>
                {
                    Debug.Log(error.GenerateErrorReport());
                } );
            }
            _createCharacterObject = createCharacterObject;
        }
        
        public void SetNullCharacter()
        {
            _nullCharacter.SetActive(true);
            _hasCharacter.SetActive(false);
        }
        
        public void SetCharacter()
        {
            _nullCharacter.SetActive(false);
            _hasCharacter.SetActive(true);
        }
    }
}