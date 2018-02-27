namespace ASM31
{
    public enum State
    {
        Stopped,
        Starting,
        Running
    };

    public enum ServerType
    {
        Web,
        DB,
        Game,
        Login,
        Chat
    };

    public enum Races
    {
        All,
        Asmo,
        Elyo
    };

    public enum Membership
    {
        Free = (byte)0,
        Premium = (byte)1,
        VIP = (byte)2
    };

    public enum AccountType
    {
        Player = (byte)0,
        Moderator = (byte)1,
        Asst_GM = (byte)2,
        GM = (byte)3,
        Boss_GM = (byte)4,
        Asst_Admin = (byte)5,
        Admin_L1 = (byte)6,
        Admin_L2 = (byte)7,
        Admin_L3 = (byte)8,
        Co_Owner = (byte)9,
        Owner = (byte)10
    };
}