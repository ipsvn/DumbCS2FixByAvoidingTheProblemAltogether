using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Memory.DynamicFunctions;

namespace DumbCS2FixByAvoidingTheProblemAltogether;
public class DumbCS2FixByAvoidingTheProblemAltogether : BasePlugin
{
    public override string ModuleName => "DumbCS2FixByAvoidingTheProblemAltogether";

    public override string ModuleVersion => "0.0.1";

    private static MemoryFunctionVoid<IntPtr, uint, IntPtr, IntPtr> ChangeSignOnState = new(@"\x55\x89\xF0\x48\x89\xE5\x41\x57\x41\x56", Addresses.EnginePath);

    public override void Load(bool hotReload)
    {
        ChangeSignOnState.Hook(ChangeSignOnStateHook, HookMode.Pre);
    }

    public override void Unload(bool hotReload)
    {
        ChangeSignOnState.Unhook(ChangeSignOnStateHook, HookMode.Pre);
    }

    public HookResult ChangeSignOnStateHook(DynamicHook hook)
    {
        var ns = hook.GetParam<uint>(1);
        Server.PrintToConsole($"Hook called {ns}");
        if (ns == 7)
        {
            return HookResult.Handled;
        }

        return HookResult.Continue;
    }

}