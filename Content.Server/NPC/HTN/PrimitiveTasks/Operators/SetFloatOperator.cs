using System.Threading.Tasks;

namespace Content.Server.NPC.HTN.PrimitiveTasks.Operators;

/// <summary>
/// Just sets a blackboard key to a float
/// </summary>
public sealed class SetFloatOperator : HTNOperator
{
    [DataField("targetKey", required: true)] public string TargetKey = string.Empty;

    [ViewVariables(VVAccess.ReadWrite), DataField("amount")]
    public float Amount;

    public override async Task<(bool Valid, Dictionary<string, object>? Effects)> Plan(NPCBlackboard blackboard)
    {
        return (true, new Dictionary<string, object>()
        {
            {TargetKey, Amount},
        });
    }
}
