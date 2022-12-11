using Amazon.Runtime.CredentialManagement;
using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider;
using Amazon;
using Amazon.CognitoIdentityProvider.Model;

namespace playground.UserSetup
{
    public class CognitoUserManagement
    {
        private readonly AWSCredentials awsCredentials;
        private readonly AmazonCognitoIdentityProviderClient adminAmazonCognitoIdentityProviderClient;
        private readonly AmazonCognitoIdentityProviderClient anonymousAmazonCognitoIdentityProviderClient;

        public CognitoUserManagement(string profileName)
        {
            CredentialProfileStoreChain credentialProfileStoreChain = new CredentialProfileStoreChain();

            if (credentialProfileStoreChain.TryGetAWSCredentials(profileName, out AWSCredentials internalAwsCredentials))
            {
                awsCredentials = internalAwsCredentials;
                adminAmazonCognitoIdentityProviderClient = new AmazonCognitoIdentityProviderClient(
                    awsCredentials);
                anonymousAmazonCognitoIdentityProviderClient = new AmazonCognitoIdentityProviderClient(
                    new AnonymousAWSCredentials());
            }
            else
            {
                throw new ArgumentNullException(nameof(AWSCredentials));
            }
        }

        public async Task AdminCreateUserAsync(
            string username,
            string password,
            string userPoolId,
            string appClientId,
            List<AttributeType> attributeTypes)
        {
            AdminCreateUserRequest adminCreateUserRequest = new AdminCreateUserRequest
            {
                Username = username,
                TemporaryPassword = password,
                UserPoolId = userPoolId,
                UserAttributes = attributeTypes,
                MessageAction= MessageActionType.SUPPRESS,
            };
            AdminCreateUserResponse adminCreateUserResponse = await adminAmazonCognitoIdentityProviderClient
                .AdminCreateUserAsync(adminCreateUserRequest)
                .ConfigureAwait(false);

            AdminUpdateUserAttributesRequest adminUpdateUserAttributesRequest = new AdminUpdateUserAttributesRequest
            {
                Username = username,
                UserPoolId = userPoolId,
                UserAttributes = new List<AttributeType>
                    {
                        new AttributeType()
                        {
                            Name = "email_verified",
                            Value = "true"
                        }
                    }
            };

            AdminUpdateUserAttributesResponse adminUpdateUserAttributesResponse = adminAmazonCognitoIdentityProviderClient
                .AdminUpdateUserAttributesAsync(adminUpdateUserAttributesRequest)
                .Result;

            var response= await adminAmazonCognitoIdentityProviderClient.AdminSetUserPasswordAsync(new AdminSetUserPasswordRequest()
            {
                Password = password,
                Username = username,
                Permanent = true,
                UserPoolId = userPoolId

            });


            AdminInitiateAuthRequest adminInitiateAuthRequest = new AdminInitiateAuthRequest
            {
                UserPoolId = userPoolId,
                ClientId = appClientId,
                AuthFlow = "ADMIN_NO_SRP_AUTH",
                AuthParameters = new Dictionary<string, string>
            {
                { "USERNAME", username},
                { "PASSWORD", password}
            }
            };
        }

        public async Task AdminAddUserToGroupAsync(
            string username,
            string userPoolId,
            string groupName)
        {
            AdminAddUserToGroupRequest adminAddUserToGroupRequest = new AdminAddUserToGroupRequest
            {
                Username = username,
                UserPoolId = userPoolId,
                GroupName = groupName
            };

            AdminAddUserToGroupResponse adminAddUserToGroupResponse = await adminAmazonCognitoIdentityProviderClient
                .AdminAddUserToGroupAsync(adminAddUserToGroupRequest)
                .ConfigureAwait(false);
        }

        public async Task<AdminInitiateAuthResponse> AdminAuthenticateUserAsync(
            string username,
            string password,
            string userPoolId,
            string appClientId)
        {
            AdminInitiateAuthRequest adminInitiateAuthRequest = new AdminInitiateAuthRequest
            {
                UserPoolId = userPoolId,
                ClientId = appClientId,
                AuthFlow = "ADMIN_NO_SRP_AUTH",
                AuthParameters = new Dictionary<string, string>
            {
                { "USERNAME", username},
                { "PASSWORD", password}
            }
            };
            return await adminAmazonCognitoIdentityProviderClient
                .AdminInitiateAuthAsync(adminInitiateAuthRequest)
                .ConfigureAwait(false);
        }

        public async Task AdminRemoveUserFromGroupAsync(
            string username,
            string userPoolId,
            string groupName)
        {
            AdminRemoveUserFromGroupRequest adminRemoveUserFromGroupRequest = new AdminRemoveUserFromGroupRequest
            {
                Username = username,
                UserPoolId = userPoolId,
                GroupName = groupName
            };

            await adminAmazonCognitoIdentityProviderClient
                .AdminRemoveUserFromGroupAsync(adminRemoveUserFromGroupRequest)
                .ConfigureAwait(false);
        }

        public async Task AdminDisableUserAsync(
            string username,
            string userPoolId)
        {
            AdminDisableUserRequest adminDisableUserRequest = new AdminDisableUserRequest
            {
                Username = username,
                UserPoolId = userPoolId
            };

            await adminAmazonCognitoIdentityProviderClient
                .AdminDisableUserAsync(adminDisableUserRequest)
                .ConfigureAwait(false);
        }

        public async Task AdminDeleteUserAsync(
            string username,
            string userPoolId)
        {
            AdminDeleteUserRequest deleteUserRequest = new AdminDeleteUserRequest
            {
                Username = username,
                UserPoolId = userPoolId
            };

            await adminAmazonCognitoIdentityProviderClient
                .AdminDeleteUserAsync(deleteUserRequest)
                .ConfigureAwait(false);
        }
    }
}
