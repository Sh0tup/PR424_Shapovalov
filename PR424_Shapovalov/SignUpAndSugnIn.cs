using System;
using System.Linq;

namespace PR424_Shapovalov
{
    public class SignUpAndSugnIn
    {

        public AppUser Authenticate(string nickname, string password)
        {
            if (string.IsNullOrEmpty(nickname) || string.IsNullOrEmpty(password))
                return null;

            var user = Core.Context.AppUser.FirstOrDefault(x => x.Nickname == nickname);
            if (user == null || !VerifyPassword(user, password))
                return null;
            kkk
            return user;
        }

        public bool IsAuthenticated(string nickname, string password)
        {
            return Authenticate(nickname, password) != null;
        }

        public void Register(string nickname, string password)
        {
            if (string.IsNullOrEmpty(nickname))
                Console.WriteLine("Логин не может быть пустым.");
            if (string.IsNullOrEmpty(password))
                Console.WriteLine("Пароль не может быть пустым.");
            if (Core.Context.AppUser.Any(x => x.Nickname == nickname))
            {
                Core.Context.AppUser.Add(new AppUser
                {
                    Nickname = nickname,
                    Password = password
                });
            }
            Core.Context.SaveChanges();
        }

        private bool VerifyPassword(AppUser user, string password)
        {
            return user != null && user.Password == password;
        }
    }
}
