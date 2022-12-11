using Microsoft.Extensions.Configuration;

namespace playground.UserSetup
{
    internal static class UserCreator
    {
        public static async Task createUsers(IConfigurationRoot configuration)
        {
            var password = configuration["DefaultUserPassword"];
            using StreamWriter file=  new("userLog.json", append:true);
            
            CognitoUserManagement userManagement = new CognitoUserManagement("default");
            for (int i = 7885; i < 10000; i++)
            {
                try
                {
                    await userManagement.AdminCreateUserAsync($"heymanu+{i}@amazon.com", password, "us-east-1_QBoIUnnt6", "2rfj89s9q5mvaviqd9ha8sggs4", new List<Amazon.CognitoIdentityProvider.Model.AttributeType>());
                    await file.WriteLineAsync($"heymanu+{i}@amazon.com");
                    Console.WriteLine($"heymanu+{i}@amazon.com");
                }
                catch (Exception e)
                {
                       Console.WriteLine(e);
                }
            };
        }
    }
}
