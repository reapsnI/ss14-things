using System.Threading.Tasks;

namespace Content.Server.NPC.HTN.PrimitiveTasks;

/// <summary>
/// Concrete code that gets run for an NPC task.
/// </summary>
[ImplicitDataDefinitionForInheritors]
public abstract class HTNOperator
{
    /// <summary>
    /// Called once whenever prototypes reload. Typically used to inject dependencies.
    /// </summary>
    public virtual void Initialize(IEntitySystemManager sysManager)
    {
        IoCManager.InjectDependencies(this);
    }

    /// <summary>
    /// Called during planning.
    /// </summary>
    /// <param name="blackboard">The blackboard for the NPC.</param>
    /// <returns>Whether the plan is still valid and the effects to apply to the blackboard.
    /// These get re-applied during execution and are up to the operator to use or discard.</returns>
    public virtual async Task<(bool Valid, Dictionary<string, object>? Effects)> Plan(NPCBlackboard blackboard)
    {
        return (true, null);
    }

    /// <summary>
    /// Called during the NPC's regular updates. If the logic requires coordination between NPCs (e.g. steering or combat)
    /// this may be better off using a component and letting an external system handling it.
    /// </summary>
    public virtual HTNOperatorStatus Update(NPCBlackboard blackboard, float frameTime)
    {
        return HTNOperatorStatus.Finished;
    }

    /// <summary>
    /// Called the first time an operator runs.
    /// </summary>
    public virtual void Startup(NPCBlackboard blackboard) {}

    /// <summary>
    /// Called whenever the operator stops running.
    /// </summary>
    public virtual void Shutdown(NPCBlackboard blackboard, HTNOperatorStatus status) {}
}
