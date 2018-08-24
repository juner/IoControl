namespace IoControl.Tests
{
    public static class CheckTool
    {
        public static bool IsAdministrator() => new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent()).IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
    }
}
