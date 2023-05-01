using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace _Root.Scripts.Test
{
    public class CharacterSelectorManager : MonoBehaviour
    {
        [SerializeField] private CreateCharacterWindow _createCharacterWindow;
        [SerializeField] private CharacterSelectorContainer _characterSelectorContainerPrefab;
        [SerializeField] private Transform _slotsParent;
        
        private List<CharacterSelectorContainer> _slots = new List<CharacterSelectorContainer>();

        private void Start()
        {
            UpdateCharactersInfo();
        }

        public void UpdateCharactersInfo()
        {
            if (_slots.Count == 0)
            {
                var go = Instantiate(_characterSelectorContainerPrefab, _slotsParent);
                go.Initialize(null, _createCharacterWindow);
                _slots.Add(go);
                go.SetNullCharacter();
            }
            PlayFabClientAPI.GetAllUsersCharacters(new ListUsersCharactersRequest(), result =>
            {
                if (result.Characters.Count > _slots.Count)
                {
                    var difference = result.Characters.Count - _slots.Count;
                    for (int i = 0; i < difference; i++)
                    {
                        var go = Instantiate(_characterSelectorContainerPrefab, transform);
                        _slots.Add(go);
                        go.SetNullCharacter();
                    }
                }

                for (int i = 0; i < result.Characters.Count; i++)
                {
                    var character = result.Characters[i];
                    _slots[i].Initialize(character, _createCharacterWindow);
                }
            }, error =>
            {
                Debug.Log(error.GenerateErrorReport());
            });
        }
    }
}