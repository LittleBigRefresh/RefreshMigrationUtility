﻿using System.Xml.Serialization;

#nullable disable

namespace Refresh.Database.Models.Levels;

[XmlType("customReward")]
[PrimaryKey(nameof(LevelId), nameof(Id))]
public partial class GameSkillReward
{
    [XmlAttribute(AttributeName = "id")] public int Id { get; set; }
    [XmlElement("enabled")] public bool Enabled { get; set; }
    [XmlElement("description")] public string Title { get; set; }
    [XmlElement("amountNeeded")] public float RequiredAmount { get; set; }

    [ForeignKey(nameof(LevelId))]
    [XmlIgnore] public GameLevel Level { get; set; } = null!;
    [XmlIgnore] public int LevelId { get; set; }

    // Realm can't store enums, use recommended workaround
    // ReSharper disable once InconsistentNaming (can't fix due to conflict with ConditionType)
    // ReSharper disable once MemberCanBePrivate.Global
    public int _ConditionType { get; set; }
    [NotMapped] [XmlElement("condition")]
    public GameSkillRewardCondition ConditionType
    {
        get => (GameSkillRewardCondition)this._ConditionType;
        set => this._ConditionType = (int)value;
    }
}