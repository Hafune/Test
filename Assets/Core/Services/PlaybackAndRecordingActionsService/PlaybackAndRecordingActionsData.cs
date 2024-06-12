using System.Collections.Generic;
using UnityEngine;

namespace Core.Services
{
    [CreateAssetMenu(menuName = "Game Config/" + nameof(PlaybackAndRecordingActionsData))]
    public class PlaybackAndRecordingActionsData : ScriptableObject
    {
        [SerializeField] public List<Command> Commands;

        public struct Command
        {
        }
    }
}