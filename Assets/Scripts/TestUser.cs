public class TestUser {

    private string userName;

    public string UserName
    {
        get { return userName; }
        set { userName = value; }
    }
    
    public static TestUser _testUser;

    public TestUser(string username)
    {
        UserName = username;
    }
}
