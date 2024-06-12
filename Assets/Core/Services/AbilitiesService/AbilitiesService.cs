// using System;
// using System.Collections.Generic;
// using System.Linq;
// using BansheeGz.BGDatabase;
// using Core.Components;
// using Core.EcsCommon.ValueSlotComponents;
// using Core.Generated;
// using Core.Systems;
// using Reflex;
//
// namespace Core.Services
// {
//     public class AbilitiesService : IInitializableService, ISerializableService
//     {
//         public event Action OnChange;
//
//         private ServiceData _serviceData = new();
//         private PlayerDataService _playerDataService;
//         private GemService _gemService;
//         private readonly List<AbilitiesViewData> _list = new();
//         private readonly Dictionary<ValueEnum, float> _tempValuesStorage = new();
//         private readonly Dictionary<BGId, int> _abilitiesCurrentLevel = new();
//         private readonly Dictionary<BGId, int[]> _abilityLevelCosts = new();
//         private readonly HashSet<SlotTagEnum> _tempTagsStorage = new();
//         private EcsEngine _ecsEngine;
//         private bool _dirtyCache = true;
//
//         public AbilitiesSlotComponent CurrentSlot { get; private set; }
//
//         public AbilitiesService()
//         {
//             Abilities.ForEachEntity(model =>
//             {
//                 var rawData = model.Costs;
//
//                 if (string.IsNullOrEmpty(rawData))
//                 {
//                     _abilityLevelCosts[model.Id] = Array.Empty<int>();
//                 }
//                 else
//                 {
//                     var strings = rawData.Trim().Split("\n");
//                     var costs = strings.Select(int.Parse).ToArray();
//                     _abilityLevelCosts[model.Id] = costs;
//                 }
//
//                 GetCurrentLevel(model);
//             });
//         }
//
//         public void InitializeService(Context context)
//         {
//             _playerDataService = context.Resolve<PlayerDataService>();
//             _playerDataService.RegisterService(this);
//
//             _gemService = context.Resolve<GemService>();
//             _ecsEngine = context.Resolve<EcsEngine>();
//
//             _dirtyCache = false;
//             CalculateSlot();
//         }
//
//         public List<AbilitiesViewData> GetItemViewsData()
//         {
//             _list.Clear();
//
//             Abilities.ForEachEntity(model =>
//             {
//                 if (model == Abilities.triple_jump && GetCurrentLevel(Abilities.double_jump) == 0)
//                     return;
//
//                 _list.Add(BuildViewData(model));
//             });
//
//             return _list;
//         }
//
//         public void TryBuyAbility(Abilities model)
//         {
//             var playerData = _serviceData.abilities[model.name];
//             var currentLevel = GetCurrentLevel(model);
//
//             if (currentLevel >= _abilityLevelCosts[model.Id].Length)
//                 return;
//
//             var price = _abilityLevelCosts[model.Id][GetCurrentLevel(model)];
//
//             if (!_gemService.TryChangeValue(-price))
//                 return;
//
//             playerData.totalExperience += price;
//             _serviceData.abilities[model.name] = playerData;
//             _playerDataService.SetDirty(this);
//
//             _dirtyCache = true;
//             var newLevel = GetCurrentLevel(model);
//             _dirtyCache = false;
//
//             if (currentLevel == newLevel)
//                 return;
//
//             CalculateSlot();
//             _ecsEngine.MakeTicksToUpdateSlotValues();
//         }
//
//         public int GetCurrentLevel(Abilities model)
//         {
//             if (!_dirtyCache)
//                 return _abilitiesCurrentLevel[model.Id];
//
//             var totalExperience = _serviceData.abilities[model.name].totalExperience;
//             var levelCosts = _abilityLevelCosts[model.Id];
//             int level = 0;
//             while (level < levelCosts.Length && totalExperience >= levelCosts[level])
//                 totalExperience -= levelCosts[level++];
//
//             _abilitiesCurrentLevel[model.Id] = level;
//             return level;
//         }
//
//         private AbilitiesViewData BuildViewData(Abilities model)
//         {
//             int level = GetCurrentLevel(model);
//             var costs = _abilityLevelCosts[model.Id];
//             int nextLevelCost = costs.Length > level ? costs[level] : 0;
//
//             return new()
//             {
//                 model = model,
//                 icon = model.Icon,
//                 localizationKey = model.name,
//                 level = level,
//                 nextLevelCost = nextLevelCost,
//                 isMaxLevel = level == _abilityLevelCosts[model.Id].Length,
//             };
//         }
//
//         private void CalculateSlot()
//         {
//             AbilitiesSlotComponent slot = new();
//             _tempValuesStorage.Clear();
//             _tempTagsStorage.Clear();
//
//             var models = _serviceData.abilities
//                 .Where(i => i.Value.totalExperience > 0)
//                 .Select(i => Abilities.GetEntity(i.Key));
//
//             foreach (var model in models)
//             {
//                 int currentLevel = GetCurrentLevel(model);
//                 foreach (var data in SlotUtility.CalculateValues(model.Values))
//                 {
//                     float value = data.value * currentLevel;
//                     if (_tempValuesStorage.ContainsKey(data.valueEnum))
//                         _tempValuesStorage[data.valueEnum] += value;
//                     else
//                         _tempValuesStorage.Add(data.valueEnum, value);
//                 }
//
//                 var tags = model.Tags ?? (IReadOnlyList<SlotTagEnum>)Array.Empty<SlotTagEnum>();
//                 for (int a = 0, aMax = tags.Count; a < aMax; a++)
//                     _tempTagsStorage.Add(tags[a]);
//             }
//
//             slot.values = _tempValuesStorage
//                 .Select(pair => new ValueData { valueEnum = pair.Key, value = pair.Value })
//                 .ToArray();
//
//             slot.tags = _tempTagsStorage.ToArray();
//             CurrentSlot = slot;
//             OnChange?.Invoke();
//         }
//
//         public void SaveData() => _playerDataService.SerializeData(this, _serviceData);
//
//         public void ReloadData()
//         {
//             var defaultAbilities = _serviceData.abilities;
//             _serviceData = _playerDataService.DeserializeData<ServiceData>(this);
//
//             foreach (var key in defaultAbilities.Keys)
//                 if (!_serviceData.abilities.ContainsKey(key))
//                     _serviceData.abilities.Add(key, defaultAbilities[key]);
//
//             _dirtyCache = true;
//             Abilities.ForEachEntity(model => GetCurrentLevel(model));
//             _dirtyCache = false;
//
//             CalculateSlot();
//             _ecsEngine.MakeTicksToUpdateSlotValues();
//             OnChange?.Invoke();
//         }
//
//         private class ServiceData
//         {
//             public Dictionary<string, Data> abilities;
//
//             public ServiceData() => abilities = Abilities.FindEntities(_ => true)
//                 .ToDictionary(i => i.name, _ => new Data());
//         }
//
//         private struct Data
//         {
//             public int totalExperience;
//         }
//     }
// }