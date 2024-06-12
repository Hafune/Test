using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using VInspector.Libs;
#endif

namespace Core.Services
{
    [CreateAssetMenu(menuName = "Game Config/" + nameof(EntitiesTable))]
    public class EntitiesTable : ScriptableObject
    {
        [SerializeField, ReadOnly] private int[] IDs;
#if UNITY_EDITOR
        [SerializeField] private GameObject[] Assets = Array.Empty<GameObject>();

        private void OnValidate()
        {
            var set = new HashSet<GameObject>();
            Assets = Assets.Where(set.Add).OrderBy(i => i.name).ToArray();
            IDs = Assets
                .Select(i => Animator.StringToHash(i.GetGuid()))
                .ToArray();
        }
#endif

        public bool Contains(int id) => IDs.Contains(id);
    }
}