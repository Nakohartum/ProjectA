using UnityEngine;


namespace _Root.Scripts.Models
{
    [CreateAssetMenu(fileName = nameof(PlayerInformationObject), menuName = "Models/"+nameof(PlayerInformationObject), order = 0)]
    public class PlayerInformationObject : ScriptableObject
    {
        public PlayerInformation PlayerInformation;
    }
}