using System;
using System.Collections.Generic;
using BansheeGz.BGDatabase;
using Core.Generated;
//
// //=============================================================
// //||                   Generated by BansheeGz Code Generator ||
// //=============================================================
//
// #pragma warning disable 414
//
// public partial class Weapons : BGEntity, IDatabaseSlotData, IDropItem
// {
//
// 	public class Factory : BGEntity.EntityFactory
// 	{
// 		public BGEntity NewEntity(BGMetaEntity meta) => new Weapons(meta);
// 		public BGEntity NewEntity(BGMetaEntity meta, BGId id) => new Weapons(meta, id);
// 	}
//
// 	public static class __Names
// 	{
// 		public const string Meta = "Weapons";
// 		public const string name = "name";
// 		public const string Type = "Type";
// 		public const string Icon = "Icon";
// 		public const string SpecialIcon = "SpecialIcon";
// 		public const string Values = "Values";
// 		public const string Tags = "Tags";
// 		public const string Level = "Level";
// 		public const string globalId = "globalId";
// 	}
// 	private static BansheeGz.BGDatabase.BGMetaRow _metaDefault;
// 	public static BansheeGz.BGDatabase.BGMetaRow MetaDefault => _metaDefault ?? (_metaDefault = BGCodeGenUtils.GetMeta<BansheeGz.BGDatabase.BGMetaRow>(new BGId(5100874084694221151UL,2450235158082026898UL), () => _metaDefault = null));
// 	public static BansheeGz.BGDatabase.BGRepoEvents Events => BGRepo.I.Events;
// 	public static int CountEntities => MetaDefault.CountEntities;
// 	public System.String name => _name[Index];
// 	public Core.Services.WeaponTypeEnum Type => (Core.Services.WeaponTypeEnum) _Type.GetStoredValue(Index);
// 	public UnityEngine.Sprite Icon => _Icon[Index];
// 	public UnityEngine.Sprite SpecialIcon => _SpecialIcon[Index];
// 	public System.Collections.Hashtable Values => _Values[Index];
// 	public List<Core.Generated.SlotTagEnum> Tags => BGCodeGenUtils.EnumListGet<Core.Generated.SlotTagEnum>(_Tags, Index);
// 	public System.Int32 Level => _Level[Index];
// 	public System.Int32 globalId
// 	{
// 		get => _globalId[Index];
// 		set => _globalId[Index] = value;
// 	}
// 	private static BansheeGz.BGDatabase.BGFieldEntityName _ufle12jhs77_name;
// 	public static BansheeGz.BGDatabase.BGFieldEntityName _name => _ufle12jhs77_name ?? (_ufle12jhs77_name = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldEntityName>(MetaDefault, new BGId(5631246522076597128UL, 15522868708216205220UL), () => _ufle12jhs77_name = null));
// 	private static BansheeGz.BGDatabase.BGFieldEnum _ufle12jhs77_Type;
// 	public static BansheeGz.BGDatabase.BGFieldEnum _Type => _ufle12jhs77_Type ?? (_ufle12jhs77_Type = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldEnum>(MetaDefault, new BGId(4624456034158857494UL, 8358031033366197128UL), () => _ufle12jhs77_Type = null));
// 	private static BansheeGz.BGDatabase.BGFieldUnitySprite _ufle12jhs77_Icon;
// 	public static BansheeGz.BGDatabase.BGFieldUnitySprite _Icon => _ufle12jhs77_Icon ?? (_ufle12jhs77_Icon = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldUnitySprite>(MetaDefault, new BGId(5323750176243218211UL, 6467175365301980579UL), () => _ufle12jhs77_Icon = null));
// 	private static BansheeGz.BGDatabase.BGFieldUnitySprite _ufle12jhs77_SpecialIcon;
// 	public static BansheeGz.BGDatabase.BGFieldUnitySprite _SpecialIcon => _ufle12jhs77_SpecialIcon ?? (_ufle12jhs77_SpecialIcon = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldUnitySprite>(MetaDefault, new BGId(5332135195279088561UL, 3018924968505315506UL), () => _ufle12jhs77_SpecialIcon = null));
// 	private static BansheeGz.BGDatabase.BGFieldHashtable _ufle12jhs77_Values;
// 	public static BansheeGz.BGDatabase.BGFieldHashtable _Values => _ufle12jhs77_Values ?? (_ufle12jhs77_Values = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldHashtable>(MetaDefault, new BGId(5012650057969951114UL, 9929145133617743749UL), () => _ufle12jhs77_Values = null));
// 	private static BansheeGz.BGDatabase.BGFieldEnumList _ufle12jhs77_Tags;
// 	public static BansheeGz.BGDatabase.BGFieldEnumList _Tags => _ufle12jhs77_Tags ?? (_ufle12jhs77_Tags = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldEnumList>(MetaDefault, new BGId(5238680808923168449UL, 298015721356797887UL), () => _ufle12jhs77_Tags = null));
// 	private static BansheeGz.BGDatabase.BGFieldInt _ufle12jhs77_Level;
// 	public static BansheeGz.BGDatabase.BGFieldInt _Level => _ufle12jhs77_Level ?? (_ufle12jhs77_Level = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldInt>(MetaDefault, new BGId(4693702786174911367UL, 7294850556549603248UL), () => _ufle12jhs77_Level = null));
// 	private static BansheeGz.BGDatabase.BGFieldInt _ufle12jhs77_globalId;
// 	public static BansheeGz.BGDatabase.BGFieldInt _globalId => _ufle12jhs77_globalId ?? (_ufle12jhs77_globalId = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldInt>(MetaDefault, new BGId(4772204531406849510UL, 10024355313087412402UL), () => _ufle12jhs77_globalId = null));
// 	private static readonly Weapons.Factory _factory0_PFS = new Weapons.Factory();
// 	private static readonly Enhancements.Factory _factory1_PFS = new Enhancements.Factory();
// 	private static readonly Abilities.Factory _factory2_PFS = new Abilities.Factory();
// 	private static readonly ItemDatabase.Factory _factory3_PFS = new ItemDatabase.Factory();
// 	private Weapons() : base(MetaDefault) {}
// 	private Weapons(BGId id) : base(MetaDefault, id) {}
// 	private Weapons(BGMetaEntity meta) : base(meta) {}
// 	private Weapons(BGMetaEntity meta, BGId id) : base(meta, id) {}
// 	public static Weapons FindEntity(Predicate<Weapons> filter) => BGCodeGenUtils.FindEntity(MetaDefault, filter);
// 	public static List<Weapons> FindEntities(Predicate<Weapons> filter, List<Weapons> result=null, Comparison<Weapons> sort=null) => BGCodeGenUtils.FindEntities(MetaDefault, filter, result, sort);
// 	public static void ForEachEntity(Action<Weapons> action, Predicate<Weapons> filter=null, Comparison<Weapons> sort=null) => BGCodeGenUtils.ForEachEntity(MetaDefault, action, filter, sort);
// 	public static Weapons GetEntity(BGId entityId) => (Weapons) MetaDefault.GetEntity(entityId);
// 	public static Weapons GetEntity(int index) => (Weapons) MetaDefault[index];
// 	public static Weapons GetEntity(string entityName) => (Weapons) MetaDefault.GetEntity(entityName);
// 	public static Weapons NewEntity() => (Weapons) MetaDefault.NewEntity();
// 	public static Weapons NewEntity(BGId entityId) => (Weapons) MetaDefault.NewEntity(entityId);
// 	public static Weapons NewEntity(Action<Weapons> callback) => (Weapons) MetaDefault.NewEntity(new BGMetaEntity.NewEntityContext(entity => callback((Weapons)entity)));
// }
//
// public partial class Enhancements : BGEntity, IDatabaseSlotData, IDropItem
// {
//
// 	public class Factory : BGEntity.EntityFactory
// 	{
// 		public BGEntity NewEntity(BGMetaEntity meta) => new Enhancements(meta);
// 		public BGEntity NewEntity(BGMetaEntity meta, BGId id) => new Enhancements(meta, id);
// 	}
//
// 	public static class __Names
// 	{
// 		public const string Meta = "Enhancements";
// 		public const string name = "name";
// 		public const string Icon = "Icon";
// 		public const string EnergyCost = "EnergyCost";
// 		public const string Level = "Level";
// 		public const string Values = "Values";
// 		public const string Tags = "Tags";
// 		public const string globalId = "globalId";
// 	}
// 	private static BansheeGz.BGDatabase.BGMetaRow _metaDefault;
// 	public static BansheeGz.BGDatabase.BGMetaRow MetaDefault => _metaDefault ?? (_metaDefault = BGCodeGenUtils.GetMeta<BansheeGz.BGDatabase.BGMetaRow>(new BGId(5311797283028991016UL,13798524751616915867UL), () => _metaDefault = null));
// 	public static BansheeGz.BGDatabase.BGRepoEvents Events => BGRepo.I.Events;
// 	public static int CountEntities => MetaDefault.CountEntities;
// 	public System.String name => _name[Index];
// 	public UnityEngine.Sprite Icon => _Icon[Index];
// 	public System.Int32 EnergyCost => _EnergyCost[Index];
// 	public System.Int32 Level => _Level[Index];
// 	public System.Collections.Hashtable Values => _Values[Index];
// 	public List<Core.Generated.SlotTagEnum> Tags => BGCodeGenUtils.EnumListGet<Core.Generated.SlotTagEnum>(_Tags, Index);
// 	public System.Int32 globalId
// 	{
// 		get => _globalId[Index];
// 		set => _globalId[Index] = value;
// 	}
// 	private static BansheeGz.BGDatabase.BGFieldEntityName _ufle12jhs77_name;
// 	public static BansheeGz.BGDatabase.BGFieldEntityName _name => _ufle12jhs77_name ?? (_ufle12jhs77_name = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldEntityName>(MetaDefault, new BGId(4891210302576983717UL, 9178717260282236044UL), () => _ufle12jhs77_name = null));
// 	private static BansheeGz.BGDatabase.BGFieldUnitySprite _ufle12jhs77_Icon;
// 	public static BansheeGz.BGDatabase.BGFieldUnitySprite _Icon => _ufle12jhs77_Icon ?? (_ufle12jhs77_Icon = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldUnitySprite>(MetaDefault, new BGId(5655613196492220254UL, 4137918995121016745UL), () => _ufle12jhs77_Icon = null));
// 	private static BansheeGz.BGDatabase.BGFieldInt _ufle12jhs77_EnergyCost;
// 	public static BansheeGz.BGDatabase.BGFieldInt _EnergyCost => _ufle12jhs77_EnergyCost ?? (_ufle12jhs77_EnergyCost = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldInt>(MetaDefault, new BGId(5570966353676347327UL, 12641788828565861007UL), () => _ufle12jhs77_EnergyCost = null));
// 	private static BansheeGz.BGDatabase.BGFieldInt _ufle12jhs77_Level;
// 	public static BansheeGz.BGDatabase.BGFieldInt _Level => _ufle12jhs77_Level ?? (_ufle12jhs77_Level = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldInt>(MetaDefault, new BGId(4874578242648339167UL, 10659118755854882726UL), () => _ufle12jhs77_Level = null));
// 	private static BansheeGz.BGDatabase.BGFieldHashtable _ufle12jhs77_Values;
// 	public static BansheeGz.BGDatabase.BGFieldHashtable _Values => _ufle12jhs77_Values ?? (_ufle12jhs77_Values = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldHashtable>(MetaDefault, new BGId(5284717361938837096UL, 18274741567657408897UL), () => _ufle12jhs77_Values = null));
// 	private static BansheeGz.BGDatabase.BGFieldEnumList _ufle12jhs77_Tags;
// 	public static BansheeGz.BGDatabase.BGFieldEnumList _Tags => _ufle12jhs77_Tags ?? (_ufle12jhs77_Tags = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldEnumList>(MetaDefault, new BGId(5493746687490970407UL, 10916461481433063359UL), () => _ufle12jhs77_Tags = null));
// 	private static BansheeGz.BGDatabase.BGFieldInt _ufle12jhs77_globalId;
// 	public static BansheeGz.BGDatabase.BGFieldInt _globalId => _ufle12jhs77_globalId ?? (_ufle12jhs77_globalId = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldInt>(MetaDefault, new BGId(4636110937398443375UL, 11432956360940895875UL), () => _ufle12jhs77_globalId = null));
// 	private static readonly Weapons.Factory _factory0_PFS = new Weapons.Factory();
// 	private static readonly Enhancements.Factory _factory1_PFS = new Enhancements.Factory();
// 	private static readonly Abilities.Factory _factory2_PFS = new Abilities.Factory();
// 	private static readonly ItemDatabase.Factory _factory3_PFS = new ItemDatabase.Factory();
// 	private Enhancements() : base(MetaDefault) {}
// 	private Enhancements(BGId id) : base(MetaDefault, id) {}
// 	private Enhancements(BGMetaEntity meta) : base(meta) {}
// 	private Enhancements(BGMetaEntity meta, BGId id) : base(meta, id) {}
// 	public static Enhancements FindEntity(Predicate<Enhancements> filter) => BGCodeGenUtils.FindEntity(MetaDefault, filter);
// 	public static List<Enhancements> FindEntities(Predicate<Enhancements> filter, List<Enhancements> result=null, Comparison<Enhancements> sort=null) => BGCodeGenUtils.FindEntities(MetaDefault, filter, result, sort);
// 	public static void ForEachEntity(Action<Enhancements> action, Predicate<Enhancements> filter=null, Comparison<Enhancements> sort=null) => BGCodeGenUtils.ForEachEntity(MetaDefault, action, filter, sort);
// 	public static Enhancements GetEntity(BGId entityId) => (Enhancements) MetaDefault.GetEntity(entityId);
// 	public static Enhancements GetEntity(int index) => (Enhancements) MetaDefault[index];
// 	public static Enhancements GetEntity(string entityName) => (Enhancements) MetaDefault.GetEntity(entityName);
// 	public static Enhancements NewEntity() => (Enhancements) MetaDefault.NewEntity();
// 	public static Enhancements NewEntity(BGId entityId) => (Enhancements) MetaDefault.NewEntity(entityId);
// 	public static Enhancements NewEntity(Action<Enhancements> callback) => (Enhancements) MetaDefault.NewEntity(new BGMetaEntity.NewEntityContext(entity => callback((Enhancements)entity)));
// }
//
// public partial class Abilities : BGEntity, IDatabaseSlotData
// {
//
// 	public class Factory : BGEntity.EntityFactory
// 	{
// 		public BGEntity NewEntity(BGMetaEntity meta) => new Abilities(meta);
// 		public BGEntity NewEntity(BGMetaEntity meta, BGId id) => new Abilities(meta, id);
// 	}
//
// 	public static class __Names
// 	{
// 		public const string Meta = "Abilities";
// 		public const string name = "name";
// 		public const string Icon = "Icon";
// 		public const string maxLevel = "maxLevel";
// 		public const string Tags = "Tags";
// 		public const string Values = "Values";
// 		public const string Costs = "Costs";
// 	}
// 	private static BansheeGz.BGDatabase.BGMetaRow _metaDefault;
// 	public static BansheeGz.BGDatabase.BGMetaRow MetaDefault => _metaDefault ?? (_metaDefault = BGCodeGenUtils.GetMeta<BansheeGz.BGDatabase.BGMetaRow>(new BGId(4870277312164168866UL,10746949663737581993UL), () => _metaDefault = null));
// 	public static BansheeGz.BGDatabase.BGRepoEvents Events => BGRepo.I.Events;
// 	public static int CountEntities => MetaDefault.CountEntities;
// 	public System.String name => _name[Index];
// 	public UnityEngine.Sprite Icon => _Icon[Index];
// 	public System.Int32 maxLevel => _maxLevel[Index];
// 	public List<Core.Generated.SlotTagEnum> Tags => BGCodeGenUtils.EnumListGet<Core.Generated.SlotTagEnum>(_Tags, Index);
// 	public System.Collections.Hashtable Values => _Values[Index];
// 	public System.String Costs => _Costs[Index];
// 	private static BansheeGz.BGDatabase.BGFieldEntityName _ufle12jhs77_name;
// 	public static BansheeGz.BGDatabase.BGFieldEntityName _name => _ufle12jhs77_name ?? (_ufle12jhs77_name = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldEntityName>(MetaDefault, new BGId(5461587373129509799UL, 15853105071322568872UL), () => _ufle12jhs77_name = null));
// 	private static BansheeGz.BGDatabase.BGFieldUnitySprite _ufle12jhs77_Icon;
// 	public static BansheeGz.BGDatabase.BGFieldUnitySprite _Icon => _ufle12jhs77_Icon ?? (_ufle12jhs77_Icon = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldUnitySprite>(MetaDefault, new BGId(5515193583006087215UL, 8864467802028369333UL), () => _ufle12jhs77_Icon = null));
// 	private static BansheeGz.BGDatabase.BGFieldInt _ufle12jhs77_maxLevel;
// 	public static BansheeGz.BGDatabase.BGFieldInt _maxLevel => _ufle12jhs77_maxLevel ?? (_ufle12jhs77_maxLevel = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldInt>(MetaDefault, new BGId(5636129053497782371UL, 15181118215734142873UL), () => _ufle12jhs77_maxLevel = null));
// 	private static BansheeGz.BGDatabase.BGFieldEnumList _ufle12jhs77_Tags;
// 	public static BansheeGz.BGDatabase.BGFieldEnumList _Tags => _ufle12jhs77_Tags ?? (_ufle12jhs77_Tags = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldEnumList>(MetaDefault, new BGId(5567890011783451910UL, 2278360948580558473UL), () => _ufle12jhs77_Tags = null));
// 	private static BansheeGz.BGDatabase.BGFieldHashtable _ufle12jhs77_Values;
// 	public static BansheeGz.BGDatabase.BGFieldHashtable _Values => _ufle12jhs77_Values ?? (_ufle12jhs77_Values = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldHashtable>(MetaDefault, new BGId(5026161431915566387UL, 2600206045952118435UL), () => _ufle12jhs77_Values = null));
// 	private static BansheeGz.BGDatabase.BGFieldText _ufle12jhs77_Costs;
// 	public static BansheeGz.BGDatabase.BGFieldText _Costs => _ufle12jhs77_Costs ?? (_ufle12jhs77_Costs = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldText>(MetaDefault, new BGId(5719346964713556508UL, 1160291936021224875UL), () => _ufle12jhs77_Costs = null));
// 	private static readonly Weapons.Factory _factory0_PFS = new Weapons.Factory();
// 	private static readonly Enhancements.Factory _factory1_PFS = new Enhancements.Factory();
// 	private static readonly Abilities.Factory _factory2_PFS = new Abilities.Factory();
// 	private static readonly ItemDatabase.Factory _factory3_PFS = new ItemDatabase.Factory();
// 	private Abilities() : base(MetaDefault) {}
// 	private Abilities(BGId id) : base(MetaDefault, id) {}
// 	private Abilities(BGMetaEntity meta) : base(meta) {}
// 	private Abilities(BGMetaEntity meta, BGId id) : base(meta, id) {}
// 	public static Abilities FindEntity(Predicate<Abilities> filter) => BGCodeGenUtils.FindEntity(MetaDefault, filter);
// 	public static List<Abilities> FindEntities(Predicate<Abilities> filter, List<Abilities> result=null, Comparison<Abilities> sort=null) => BGCodeGenUtils.FindEntities(MetaDefault, filter, result, sort);
// 	public static void ForEachEntity(Action<Abilities> action, Predicate<Abilities> filter=null, Comparison<Abilities> sort=null) => BGCodeGenUtils.ForEachEntity(MetaDefault, action, filter, sort);
// 	public static Abilities GetEntity(BGId entityId) => (Abilities) MetaDefault.GetEntity(entityId);
// 	public static Abilities GetEntity(int index) => (Abilities) MetaDefault[index];
// 	public static Abilities GetEntity(string entityName) => (Abilities) MetaDefault.GetEntity(entityName);
// 	public static Abilities NewEntity() => (Abilities) MetaDefault.NewEntity();
// 	public static Abilities NewEntity(BGId entityId) => (Abilities) MetaDefault.NewEntity(entityId);
// 	public static Abilities NewEntity(Action<Abilities> callback) => (Abilities) MetaDefault.NewEntity(new BGMetaEntity.NewEntityContext(entity => callback((Abilities)entity)));
// }
//
// public partial class ItemDatabase : BGEntity
// {
//
// 	public class Factory : BGEntity.EntityFactory
// 	{
// 		public BGEntity NewEntity(BGMetaEntity meta) => new ItemDatabase(meta);
// 		public BGEntity NewEntity(BGMetaEntity meta, BGId id) => new ItemDatabase(meta, id);
// 	}
//
// 	public static class __Names
// 	{
// 		public const string Meta = "ItemDatabase";
// 		public const string name = "name";
// 		public const string source = "source";
// 		public const string key = "key";
// 		public const string prefab = "prefab";
// 	}
// 	private static BansheeGz.BGDatabase.BGMetaRow _metaDefault;
// 	public static BansheeGz.BGDatabase.BGMetaRow MetaDefault => _metaDefault ?? (_metaDefault = BGCodeGenUtils.GetMeta<BansheeGz.BGDatabase.BGMetaRow>(new BGId(5330153978680031165UL,15823927557659014289UL), () => _metaDefault = null));
// 	public static BansheeGz.BGDatabase.BGRepoEvents Events => BGRepo.I.Events;
// 	public static int CountEntities => MetaDefault.CountEntities;
// 	public System.String name
// 	{
// 		get => _name[Index];
// 		set => _name[Index] = value;
// 	}
// 	public Core.ItemSourceEnum source
// 	{
// 		get => (Core.ItemSourceEnum) _source.GetStoredValue(Index);
// 		set => _source.SetStoredValue(Index, (System.Int32) value);
// 	}
// 	public System.String key
// 	{
// 		get => _key[Index];
// 		set => _key[Index] = value;
// 	}
// 	public UnityEngine.GameObject prefab => _prefab[Index];
// 	private static BansheeGz.BGDatabase.BGFieldEntityName _ufle12jhs77_name;
// 	public static BansheeGz.BGDatabase.BGFieldEntityName _name => _ufle12jhs77_name ?? (_ufle12jhs77_name = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldEntityName>(MetaDefault, new BGId(4900752273813612420UL, 4352327314638785215UL), () => _ufle12jhs77_name = null));
// 	private static BansheeGz.BGDatabase.BGFieldEnum _ufle12jhs77_source;
// 	public static BansheeGz.BGDatabase.BGFieldEnum _source => _ufle12jhs77_source ?? (_ufle12jhs77_source = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldEnum>(MetaDefault, new BGId(5717640391686052618UL, 16674885905234950791UL), () => _ufle12jhs77_source = null));
// 	private static BansheeGz.BGDatabase.BGFieldString _ufle12jhs77_key;
// 	public static BansheeGz.BGDatabase.BGFieldString _key => _ufle12jhs77_key ?? (_ufle12jhs77_key = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldString>(MetaDefault, new BGId(5518449734294766946UL, 13676917419049816242UL), () => _ufle12jhs77_key = null));
// 	private static BansheeGz.BGDatabase.BGFieldUnityPrefab _ufle12jhs77_prefab;
// 	public static BansheeGz.BGDatabase.BGFieldUnityPrefab _prefab => _ufle12jhs77_prefab ?? (_ufle12jhs77_prefab = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldUnityPrefab>(MetaDefault, new BGId(5177874428993445688UL, 2920018412171083158UL), () => _ufle12jhs77_prefab = null));
// 	private static readonly Weapons.Factory _factory0_PFS = new Weapons.Factory();
// 	private static readonly Enhancements.Factory _factory1_PFS = new Enhancements.Factory();
// 	private static readonly Abilities.Factory _factory2_PFS = new Abilities.Factory();
// 	private static readonly ItemDatabase.Factory _factory3_PFS = new ItemDatabase.Factory();
// 	private ItemDatabase() : base(MetaDefault) {}
// 	private ItemDatabase(BGId id) : base(MetaDefault, id) {}
// 	private ItemDatabase(BGMetaEntity meta) : base(meta) {}
// 	private ItemDatabase(BGMetaEntity meta, BGId id) : base(meta, id) {}
// 	public static ItemDatabase FindEntity(Predicate<ItemDatabase> filter) => BGCodeGenUtils.FindEntity(MetaDefault, filter);
// 	public static List<ItemDatabase> FindEntities(Predicate<ItemDatabase> filter, List<ItemDatabase> result=null, Comparison<ItemDatabase> sort=null) => BGCodeGenUtils.FindEntities(MetaDefault, filter, result, sort);
// 	public static void ForEachEntity(Action<ItemDatabase> action, Predicate<ItemDatabase> filter=null, Comparison<ItemDatabase> sort=null) => BGCodeGenUtils.ForEachEntity(MetaDefault, action, filter, sort);
// 	public static ItemDatabase GetEntity(BGId entityId) => (ItemDatabase) MetaDefault.GetEntity(entityId);
// 	public static ItemDatabase GetEntity(int index) => (ItemDatabase) MetaDefault[index];
// 	public static ItemDatabase GetEntity(string entityName) => (ItemDatabase) MetaDefault.GetEntity(entityName);
// 	public static ItemDatabase NewEntity() => (ItemDatabase) MetaDefault.NewEntity();
// 	public static ItemDatabase NewEntity(BGId entityId) => (ItemDatabase) MetaDefault.NewEntity(entityId);
// 	public static ItemDatabase NewEntity(Action<ItemDatabase> callback) => (ItemDatabase) MetaDefault.NewEntity(new BGMetaEntity.NewEntityContext(entity => callback((ItemDatabase)entity)));
// }
//
// public partial interface IDatabaseSlotData  : BGAbstractEntityI
// {
// 	System.String name {get;}
// 	List<SlotTagEnum> Tags {get;}
// 	System.Collections.Hashtable Values {get;}
// }
//
public partial interface IDropItem  : BGAbstractEntityI
{
	System.String name {get;}
	System.Int32 globalId {get;}
	System.Int32 Level {get;}
}
// #pragma warning restore 414
