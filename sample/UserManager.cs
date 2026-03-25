using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace sample
{
    public static class UserManager
    {
        // 获取所有用户（用于管理界面）
        public static List<User> GetAllUsers()
        {
            return ApiClient.Get<List<User>>("users.php");
        }

        // 更新用户信息（用于管理界面）
        public static void SaveUsers(List<User> users)
        {
            foreach (var user in users)
            {
                ApiClient.Post<dynamic>("update_user.php", user);
            }
        }

        // 登录验证（通过 API）
        public static User ValidateUser(string username, string password)
        {
            var response = ApiClient.Post<LoginResponse>("auth.php", new
            {
                username = username,
                password = password
            });

            if (response != null && response.success)
            {
                return new User
                {
                    Username = response.username,
                    IsAdmin = response.is_admin,
                    IsActivated = response.is_activated,
                    Password = "", // 不返回密码
                    ActivatedCDK = ""
                };
            }

            return null;
        }
    }

    // 与 API 返回格式匹配的模型
    public class LoginResponse
    {
        public bool success { get; set; }
        public string username { get; set; }
        public bool is_admin { get; set; }
        public bool is_activated { get; set; }
        public string message { get; set; }
    }

    // 本地用户类（用于管理）
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActivated { get; set; }
        public string ActivatedCDK { get; set; }
        public bool IsAdmin { get; set; }
    }
}
