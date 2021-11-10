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
        FirebaseAuthProvider auth = new FirebaseAuthProvider(new FirebaseConfig("API KEY"));
        public async Task<string> SignIn(string email, string password)
        {
                var content = await auth.SignInWithEmailAndPasswordAsync(email, password);
                var result = await content.GetFreshAuthAsync();
                var serialized = JsonConvert.SerializeObject(result);
                Preferences.Set("MyFirebaseToken", serialized);
               return await Task.FromResult(serialized);
        }
        public async Task<string> SignUp(string email, string password)
        {
                var conn =await auth.CreateUserWithEmailAndPasswordAsync(email, password,"Abel Tomás");
                var token = conn.FirebaseToken;
                return await Task.FromResult(token);
        }
        public void SignOut(string email, string password)
        {
            Preferences.Remove("MyFirebaseToken");
        }
    }
}
