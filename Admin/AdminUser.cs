using api.Data;
using api.Interface;
using api.Model;
using Microsoft.AspNetCore.Identity;

namespace api.Admin
{
    public class AdminUser:IAdmin
    {
        private readonly UserManager<User> _user;
        public AdminUser(UserManager<User> user)
        {
            _user=user;
            
        }
        public async Task CreateAdmin(){
        String Email="dawoodadmin888@gmail.com";
        String Password="P@ssword_Admin888";
        String UserName="DawoodAdmin";
        string Role="Admin";
        var AdminUser=new User{
            UserName=UserName,
              Email=Email,
              Role=Role
        };
        if(await _user.FindByEmailAsync(Email)==null){
            var CreateUserAdmin=await _user.CreateAsync(AdminUser,Password);
            if(CreateUserAdmin.Succeeded){
                var AddRole=await _user.AddToRoleAsync(AdminUser,"Admin");
                if(AddRole.Succeeded){
                    Console.WriteLine("Successed");
                }
            }
        }
        }

       

    }
}