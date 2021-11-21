using IspcaNotas.Features.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using Xamarin.Essentials;
using Newtonsoft.Json;
using Splat;

namespace IspcaNotas.Features.Service.Login
{
    public class LoginService : ILogin
    {
        FirebaseAuthProvider auth { get; } = Locator.Current.GetService<FirebaseAuthProvider>();
        public async Task<string> SignIn(string email, string password)
        {
            var content = await auth.SignInWithEmailAndPasswordAsync(email, password);
            var result = await content.GetFreshAuthAsync();
            var serialized = JsonConvert.SerializeObject(result);
            var userId = result.User.LocalId;
            Preferences.Set("MyFirebaseToken", serialized);
            return await Task.FromResult(userId);
        }
        public async Task<string> SignUp(string email, string password, string displayname)
        {
            var conn = await auth.CreateUserWithEmailAndPasswordAsync(email, password, displayname);
            var id = conn.User.LocalId;
            return await Task.FromResult(id);
        }
        public async Task DeleteAccount(string email, string password)
        {
            var request = await auth.SignInWithEmailAndPasswordAsync(email, password);
            string token = request.FirebaseToken;
            await auth.DeleteUserAsync(token);
        }
        public void SignOut(string email, string password)
        {
            Preferences.Remove("MyFirebaseToken");
        }
    }
}
