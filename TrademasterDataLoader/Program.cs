using PoETrademasterAPI.Models;
using PoETrademasterAPI.Repository;
using System.Composition;
using System.Text.Json;
using TrademasterDataLoader.Classes;

Console.WriteLine("Starting import of 'Craft of Exile' data...");

using (var functionalityRepository = new FunctionalityRepository())
{
    functionalityRepository.ResetAllTables();
}

List<BaseJson> deserialized;
using (StreamReader r = new StreamReader("C:\\Users\\atma_\\source\\repos\\PoETrademasterAPI\\TrademasterDataLoader\\coe_data.json"))
{
    string json = r.ReadToEnd();
    deserialized = JsonSerializer.Deserialize<List<BaseJson>>(json);
}

var bgGroups = deserialized?[0]?.bgroups?.seq;
List<BaseGroupModel>? addedBaseGroups = null;
using (var baseGroupRepository = new BaseGroupRepository())
{
    var ignoreBaseGroups = new List<string> { "Invitations", "Maps", "Heist", "Sentinels" };
    foreach (BaseGroup bg in bgGroups)
    {
        /* bgroups.seq (BASE GROUP) 
        // "id_bgroup"      => BaseGroup.BaseGroupId
        // "name_bgroup"    => BaseGroup.BaseGroupName
        // "max_affix"      => BaseGroup.MaxAffixes
        // "is_rare"        => BaseGroup.CanBeRare
        // "is_influenced"  => BaseGroup.CanBeInfluenced
        // "is_fossil"      => BaseGroup.CanUseFossil
        // "is_ess"         => BaseGroup.CanUseEssence
        // "is_craftable"   => BaseGroup.AllowsCraftedAffix
        // "is_notable"     => ????? (only cluster jewels?)
        // "is_catalyst"    => BaseGroup.CanUseCatalyst
        // "has_items"      => ?????
        */
        if (!ignoreBaseGroups.Contains(bg.name_bgroup))
        {
            baseGroupRepository.AddBaseGroup(new BaseGroupModel()
            {
                BaseGroupId = int.Parse(bg.id_bgroup),
                BaseGroupName = bg.name_bgroup,
                MaxAffixes = int.Parse(bg.max_affix),
                CanBeRare = Convert.ToBoolean(Convert.ToInt16(bg.is_rare)),
                CanBeInfluenced = Convert.ToBoolean(Convert.ToInt16(bg.is_influenced)),
                CanUseFossil = Convert.ToBoolean(Convert.ToInt16(bg.is_fossil)),
                CanUseEssence = Convert.ToBoolean(Convert.ToInt16(bg.is_ess)),
                AllowsCraftedAffix = Convert.ToBoolean(Convert.ToInt16(bg.is_craftable)),
                IsNotable = Convert.ToBoolean(Convert.ToInt16(bg.is_notable)),
                CanUseCatalyst = Convert.ToBoolean(Convert.ToInt16(bg.is_catalyst)),
            });
        }
    }

    addedBaseGroups = baseGroupRepository.GetBaseGroups();
}

var bases = deserialized?[0]?.bases?.seq.OrderBy(b => int.Parse(b.id_base)).ToList();
List<BaseModel>? addedBases = null;
using (var baseRepository = new BaseRepository())
{
    var baseGroupIds = addedBaseGroups.Select(bg => bg.BaseGroupId).ToList();
    /* bases.seq (BASE/ITEM (if has_childs == true)
    // "id_bgroup"      => Base.BaseGroupId / IGNORE
    // "id_base"        => Base.BaseId / Item.ItemId
    // "name_base"      => Base.BaseName / Item.ItemName
    // "is_jewellery"   => Check if BaseGroup.BaseGroupName == "Jewellery" (DEFAULT THIS VALUE TO 1 FOR JEWELLERY)
    // "base_type"      => IGNORE
    // "has_childs"     => Base.ItemRequired
    // "master_base"    => IGNORE/Item.BaseId
    // "unique_notable" => IGNORE
    // "enchant"        => Item.Enchantment
    // "is_legacy"      => ITEMS WITH THIS AS "1" CAN BE IGNORED (No longer drops in the game)
    */
    foreach (Base baseItem in bases)
    {
        if (baseItem.is_legacy != "1"
            && baseGroupIds.Contains(int.Parse(baseItem.id_bgroup)))
        {
            baseRepository.AddBase(new BaseModel
            {
                BaseId = int.Parse(baseItem.id_base),
                BaseName = baseItem.name_base,
                BaseGroupId = int.Parse(baseItem.id_bgroup),
                ItemRequired = Convert.ToBoolean(Convert.ToInt16(baseItem.has_childs)),
                ParentBaseId = baseItem.master_base is null ? null : int.Parse(baseItem.master_base)
            });
        }
        else
        {
            // TODO: write some code to analize the unadded items
            var y = 1;
        }
    }

    addedBases = baseRepository.GetBases();
}

var items = deserialized?[0]?.bitems?.seq;
List<ItemModel>? addedItems = null;
using (var itemRepository = new ItemRepository())
using (var itemPropertyRepository = new ItemPropertyRepository())
{
    /* bitems.seq (ITEM)
    // "id_bitem"       => Item.ItemId
    // "id_base"        => Item.BaseId/Base.BaseId 
    // "name_bitem"     => Item.ItemName
    // "drop_level"     => IGNORE
    // "properties"     => ItemProperty.Property
    // "requirements"   => ItemProperty.Property
    // "implicits"      => ItemProperty.Property
    // "exp"            => Item.IsExperimentedBase
    // "imgurl"         => Item.ImgLocation
    // "is_legacy"      => ITEMS WITH THIS AS "1" CAN BE IGNORED (No longer drops in the game)
    // "exmods"         => IGNORE 
    */
    var baseIdList = addedBases.Select(bg => bg.BaseId).ToList();
    foreach (var item in items)
    {
        // TODO: split properties
        // TODO: check enchants on base
        if (item.is_legacy != "1"
            && baseIdList.Contains(int.Parse(item.id_base)))
        {
            itemRepository.AddItem(new ItemModel
            {
                ItemId = int.Parse(item.id_bitem),
                BaseId = int.Parse(item.id_base),
                ItemName = item.name_bitem,
                ImgLocation = item.imgurl,
                IsExperimentedBase = Convert.ToBoolean(Convert.ToInt16(item.exp)),
            });
        }
        else
        {
            // TODO: write some code to analize the unadded items
            var y = 1;
        }
    }
}

var affixOrigin = deserialized?[0]?.mgroups?.seq;
List<AffixOriginModel>? addedAffixOrigin = null;
using (var affixOriginRepository = new AffixOriginRepository())
{
    /* mgroups.seq (BASE AFFIX ORIGIN) 
    // "is_influence"   => AffixOrigin.IsInfluence
    // "id_mgroup"      => AffixOrigin.AffixOriginId
    // "name_mgroup"    => AffixOrigin.AffixOriginName
    // "poedb_id"       => IGNORE
    // "paste_link"     => IGNORE
    // "is_main"        => IGNORE
    // "max_chosen"     => AffixOrigin.AffixLimit (If value is 6 or 0, set to null)
    // "is_compute"     => IGNORE
    */
    foreach (var affixOriginItem in affixOrigin)
    {
        if (affixOriginItem.id_mgroup != "Sentinel"
            && affixOriginItem.id_mgroup != "Special")
        {
            var maxChosen = int.Parse(affixOriginItem.max_chosen);
            affixOriginRepository.AddAffixOrigin(new AffixOriginModel
            {
                AffixOriginId = int.Parse(affixOriginItem.id_mgroup),
                AffixOriginName = affixOriginItem.name_mgroup,
                AffixLimit = maxChosen == 0 || maxChosen == 6 ? null : maxChosen,
                IsInfluence = Convert.ToBoolean(Convert.ToInt16(affixOriginItem.is_influence)),
                IsEldritch = false,
            });
        }
    }

    // Add implicit items
    affixOriginRepository.AddAffixOrigin(new AffixOriginModel
    {
        AffixOriginId = 40,
        AffixOriginName = "Searing Exarch",
        AffixLimit = 1,
        IsInfluence = true,
        IsEldritch = true,
    });

    affixOriginRepository.AddAffixOrigin(new AffixOriginModel
    {
        AffixOriginId = 41,
        AffixOriginName = "Eater of Worlds",
        AffixLimit = 1,
        IsInfluence = true,
        IsEldritch = true,
    });

    affixOriginRepository.AddAffixOrigin(new AffixOriginModel
    {
        AffixOriginId = 42,
        AffixOriginName = "Vaal Implicit",
        AffixLimit = 2,
        IsInfluence = false,
        IsEldritch = false,
    });

    addedAffixOrigin = affixOriginRepository.GetAffixOrigins();
}

var tags = deserialized?[0]?.mtypes?.seq;
List<TagModel>? addedTagModels = null;
using (var tagRepository = new TagRepository())
{
    /* mtypes.seq (TAG)
    // "id_mtype"       => Tag.TagId
    // "poedb_id"       => IGNORE
    // "jewellery_tag"  => ?????
    // "harvest"        => IGNORE
    // "tangled"        => IGNORE
    // "parent_id"      => Tag.TagId (Ref to parent for Elemental/Defenses)
    // "name_mtype"     => Tag.TagName
    */
    foreach (var tag in tags)
    {
        tagRepository.AddTag(new TagModel
        {
            TagId = int.Parse(tag.id_mtype),
            TagName = tag.name_mtype,
        });
    }

    addedTagModels = tagRepository.GetTags();
}

var affixes = deserialized?[0]?.modifiers?.seq;
List<AffixModel>? affixModels = null;
using (var affixRepository = new AffixRepository())
using (var affixGroupRepository = new AffixGroupRepository())
using (var tagRepository = new TagRepository())
{
    var elevatedBaseAffixes = deserialized?[0]?.maeven.bmods.Keys.ToList();
    string? elevatedKey, elevatedId;
    Maeven? maevenModel;
    /* modifiers.Seq (AFFIX/BASE AFFIX)
    // "id_modifier"    -> Affix.AffixId
    // "modgroup"       -> AffixGroup.AffixGroup
    // "modgroups"      -> AffixGroup.AffixGroup (multiple)
    // "affix"          -> Affix.IsPrefix (implement implicits later)
    // "id_mgroup"      -> Affix.AffixOriginId
    // "name_modifier"  -> Affix.Affix
    // "mtypes"         -> Affix_r_Tag.TagId
    // "mtags"          -> IGNORE (""/"caster"/"attack"/null) (this is in mtypes)
    // "notable"        -> Affix.IsNotablePassive
    // "vex"            -> BaseAffix.CannotBeRolled (Syndicate exclusive Veiled mod)
    // "amg"            -> ???? (Local Item Quality/Flat Attribute/Specific Weapon/Conversion/Taken As)
    // "exkey"          -> ???? (no_effect_flask_mod ???????)
    // "ubt"            -> BaseAffix.CannotBeRolled (Special Grasping Mail affixes)
    // "ha"             -> Affix.HasAttribute
    */
    foreach (var affix in affixes)
    {
        // TODO: include filter for the affixes that won't be added due to legacy/ignored bases
        elevatedKey = elevatedBaseAffixes.FirstOrDefault(eba => eba.EndsWith(affix.id_modifier));
        elevatedId = elevatedKey != null
            ? deserialized?[0]?.maeven.bmods[elevatedKey]
            : null;

        maevenModel = elevatedId != null
            ? deserialized?[0]?.maeven.ind[elevatedId]
            : null;

        if (affix.affix == "eldritch_red")
        {
            affix.id_mgroup = "40";
        }
        else if (affix.affix == "eldritch_blue")
        {
            affix.id_mgroup = "41";
        }
        else if (affix.affix == "corrupted")
        {
            affix.id_mgroup = "42";
        }

        affixRepository.AddAffix(new AffixModel
        {
            AffixId = int.Parse(affix.id_modifier),
            Affix = affix.name_modifier,
            ElevatedAffix = maevenModel?.oname,
            IsPrefix = affix.affix == "prefix",
            IsImplicit = affix.affix == "corrupted" || affix.affix == "eldritch_red" || affix.affix == "eldritch_blue",
            IsNotablePassive = Convert.ToBoolean(Convert.ToInt16(affix.notable)),
            HasAttribute = Convert.ToBoolean(Convert.ToInt16(affix.ha)),
            // HasResistance = // TODO: how to find Has Resistance???
            AffixOriginId = int.Parse(affix.id_mgroup),
        });

        affixGroupRepository.AddAffixGroup(new AffixGroupModel
        {
            AffixId = int.Parse(affix.id_modifier),
            AffixGroupName = affix.modgroup
        });

        var mods = affix.modgroups.Substring(1, affix.modgroups.Length - 2)
            .Split(',')
            .Select(m => m.Substring(1, m.Length - 2))
            .ToList();

        mods.Remove(affix.modgroup);
        foreach (var mod in mods)
        {
            affixGroupRepository.AddAffixGroup(new AffixGroupModel
            {
                AffixId = int.Parse(affix.id_modifier),
                AffixGroupName = mod
            });
        }

        if (affix.mtypes is not null 
            && affix.mtypes.Length > 2)
        {
            var affixTags = affix.mtypes.Substring(1, affix.mtypes.Length - 2)
                .Split('|')
                .Select(t => int.Parse(t))
                .ToList();

            foreach (var tagId in affixTags)
            {
                tagRepository.AddAffixTag(int.Parse(affix.id_modifier), tagId);
            }
        }
    }

    affixModels = affixRepository.GetAffixes();
}

var basemods = deserialized?[0]?.basemods;
var affixIds = affixModels.Select(a => a.AffixId).ToHashSet();
var baseIds = addedBases.Select(b => b.BaseId).ToHashSet();
Dictionary<string, BaseAffixModel>? baseAffixModels = null;
using (var baseAffixRepository = new BaseAffixRepository())
{
    /* basemods
    * basemods.number      -> Base.BaseId
    * basemods.number[]    -> list of AffixId
    */

    /* modbases
    * modbases.number      -> Affix.AffixId
    * modbases.number[]    -> list of BaseId
    */
    foreach (KeyValuePair<string, HashSet<string>> kvp in basemods)
    {
        if (baseIds.Contains(int.Parse(kvp.Key)))
        {
            foreach (string item in kvp.Value)
            {
                if (affixIds.Contains(int.Parse(item)))
                {
                    baseAffixRepository.AddBaseAffix(new BaseAffixModel
                    {
                        AffixId = int.Parse(item),
                        BaseId = int.Parse(kvp.Key),
                    });
                }
            }
        }
    }

    baseAffixModels = baseAffixRepository.GetBaseAffixes().ToDictionary(ba => $"{ba.AffixId}-{ba.BaseId}");
}

var tiers = deserialized?[0]?.tiers;
var maeven = deserialized?[0]?.maeven;
using (var baseAffixTierRepository = new BaseAffixTierRepository())
{
    /* tiers (BASE AFFIX TIER)
    // tier.Number      -> Reference to the AffixId
    // tier.__.Number   -> Reference to the BaseId
    // "ilvl"           -> BaseAffixTier.ILvlRequirement
    // "weighting"      -> BaseAffixTier.Weight
    // "nvalues"        -> BaseAffixTier.Stat1/2/3Start/EndValue
    // "alias"          -> ????? (Text for an/1 items)
    */

    /* Elevated Mods/maeven
    * bmods
    * bmods[{baseId}-{affixId}]    -> elevatedId (in ind[{elevatedId}]
    * 
    * ind
    * ind[{elevatedId}]            -> elevated mod data
    * mvid                         -> ElevatedId
    * mid                          -> AffixId (of mod the ElevatedId is for)
    * ilvl                         -> ILvl (looks to be the same of the T1 for the AffixId... VERIFY THIS!)
    * name                         -> Display affix of elevated (with values in place)
    * nvalues                      -> BaseAffixTier.Stat1/2/3Start/EndValue (same as tiers)
    * oname                        -> Affix.ElevatedAffix
    * mtypes                       -> Affix.Tags
    */
    foreach (KeyValuePair<string, Dictionary<string, List<Tier>>> affixKvp in tiers)
    {
        if (affixIds.Contains(int.Parse(affixKvp.Key)))
        {
            foreach (KeyValuePair<string, List<Tier>> baseKvp in affixKvp.Value)
            {
                if (baseIds.Contains(int.Parse(baseKvp.Key)))
                {
                    foreach (Tier tier in baseKvp.Value)
                    {
                        var baseAffix = baseAffixModels[$"{affixKvp.Key}-{baseKvp.Key}"];
                        var baseAffixTier = new BaseAffixTierModel
                        {
                            BaseAffixId = baseAffix.BaseAffixId,
                            IsElevated = false,
                            ILvlRequirement = int.Parse(tier.ilvl),
                            Weight = int.Parse(tier.weighting),
                        };

                        SplitTierStats(baseAffixTier, tier.nvalues);
                        baseAffixTierRepository.AddBaseAffixTier(baseAffixTier);
                        if (maeven.bmods.Keys.Contains($"{baseKvp.Key}-{affixKvp.Key}"))
                        {
                            var mvid = maeven.bmods[$"{baseKvp.Key}-{affixKvp.Key}"];
                            var elevatedMod = maeven.ind[mvid];
                            var elevatedBaseAffixTier = new BaseAffixTierModel
                            {
                                BaseAffixId = baseAffix.BaseAffixId,
                                IsElevated = true,
                                ILvlRequirement = int.Parse(elevatedMod.ilvl),
                                Weight = 0,
                            };

                            SplitTierStats(elevatedBaseAffixTier, tier.nvalues);
                            baseAffixTierRepository.AddBaseAffixTier(elevatedBaseAffixTier);
                        }
                    }
                }
            }
        }
    }
}

void SplitTierStats(BaseAffixTierModel baseAffixTier, string stats)
{
    // all of these are valid for the stats string:
    // [[1,2]]
    // [1,2]
    // [1]
    // [[1,2],[3,4],[1,3]]
    // [1,2,[1,10]]
    // []
    //
    // for 2178 (# Physical Damage taken from Attack Hits)

    var deshelledString = stats.Substring(1, stats.Length - 2);
    List<(decimal?, decimal?)> finalGroups = new List<(decimal?, decimal?)>();
    for (var i = 0; i < deshelledString.Length; i++)
    {
        var currentChar = deshelledString[i];
        if (currentChar == '[')
        {
            var rightBracketIndex = deshelledString.IndexOf(']', i);
            var innerString = deshelledString.Substring(i+1, rightBracketIndex - i - 1);
            var innerStringComma = innerString.IndexOf(',');
            decimal first = decimal.Parse(innerString.Substring(0, innerStringComma));
            decimal last = decimal.Parse(innerString.Substring(innerStringComma + 1));
            finalGroups.Add((first, last));
            i = rightBracketIndex + 1;
        }
        else
        {
            var nextCommaIndex = deshelledString.IndexOf(',', i);
            if (nextCommaIndex == -1) { nextCommaIndex = deshelledString.Length; }
            var listItem = deshelledString.Substring(i, nextCommaIndex - i);
            finalGroups.Add((decimal.Parse(listItem), null));
            i = nextCommaIndex;
        }
    }

    if (finalGroups.Count > 0)
    {
        baseAffixTier.Stat1StartValue = finalGroups[0].Item1;
        baseAffixTier.Stat1EndValue = finalGroups[0].Item2; 
    }

    if (finalGroups.Count > 1)
    {
        baseAffixTier.Stat2StartValue = finalGroups[1].Item1;
        baseAffixTier.Stat2EndValue = finalGroups[1].Item2;
    }

    if (finalGroups.Count > 2)
    {
        baseAffixTier.Stat3StartValue = finalGroups[2].Item1;
        baseAffixTier.Stat3EndValue = finalGroups[2].Item2;
    }

    if (finalGroups.Count > 3)
    {
        baseAffixTier.Stat4StartValue = finalGroups[3].Item1;
        baseAffixTier.Stat4EndValue = finalGroups[3].Item2;
    }

    if (finalGroups.Count > 4)
    {
        throw new Exception("oopsy you made a fucky wuckie!!!!");
    }
}