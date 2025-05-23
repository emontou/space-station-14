using Content.Shared.Parallax.Biomes;
using Content.Shared.Procedural;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype.List;

namespace Content.Shared.Salvage.Expeditions.Modifiers;

/// <summary>
/// Affects the biome to be used for salvage.
/// </summary>
[Prototype]
public sealed partial class SalvageBiomeModPrototype : IPrototype, ISalvageMod
{
    [IdDataField] public string ID { get; private set; } = default!;

    [DataField("desc")] public LocId Description { get; private set; } = string.Empty;

    /// <summary>
    /// Cost for difficulty modifiers.
    /// </summary>
    [DataField("cost")]
    public float Cost { get; private set; } = 0f;

    /// <summary>
    /// Is weather allowed to apply to this biome.
    /// </summary>
    [DataField("weather")]
    public bool Weather = true;

    // 🌟Starlight🌟
    [DataField("difficulties", customTypeSerializer: typeof(PrototypeIdListSerializer<SalvageDifficultyPrototype>))]
    public List<string>? Difficulties { get; private set; } = null;

    [DataField("biome", required: true, customTypeSerializer:typeof(PrototypeIdSerializer<BiomeTemplatePrototype>))]
    public string? BiomePrototype;
}
