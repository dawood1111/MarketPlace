using System.Security.Cryptography.X509Certificates;
using api.Data;
using api.DTO;
using api.Interface;
using api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controller
{
    [ApiController]
    [Route("User")]
    public class UserController:ControllerBase
    {
        private readonly UserManager<User> _Users;
        private readonly SignInManager<User> _SigninUser;
        private readonly ITokenService _TokenServices;
        private readonly ApplicationDBContext _context;
        public UserController(UserManager<User> Users,SignInManager<User> signInManager,ITokenService tokenServicea,ApplicationDBContext context)
        {
            _Users=Users;
            _SigninUser=signInManager;
            _TokenServices=tokenServicea;
            _context=context;
        }
        [HttpPost("Resgister")]
     
public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
{
   
   

    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    // Create the user
    var user = new User
    {
        UserName = registerDto.UserName,
        Email = registerDto.Email
    };

    // Save the user to the database
    var createUserResult = await _Users.CreateAsync(user, registerDto.Password);
    if (createUserResult.Succeeded){
    var addToRoleResult = await _Users.AddToRoleAsync(user, "User");
    if (addToRoleResult.Succeeded)
    {
      
       
       return Ok(_TokenServices.CreateToken(user));
    }else{
        return StatusCode(500, addToRoleResult.Errors);
    }
      
    }else{
        return StatusCode(500, createUserResult.Errors);
    }

  

  

    
}
         [HttpPost("Login")]
         public async Task<IActionResult> Login(LoginDto Logindto){
            if(!ModelState.IsValid)return BadRequest(ModelState);
            var FindEmail=await _Users.Users
            .FirstOrDefaultAsync(s=>s.Email==Logindto.Email);
            if(FindEmail==null)return Unauthorized("Invalid Email");
            var CheckLog=await _SigninUser.CheckPasswordSignInAsync(FindEmail,Logindto.Password,false);
            if(!CheckLog.Succeeded)return Unauthorized("Invalid Email Or/and Password");
            return Ok(_TokenServices.CreateToken(FindEmail));

         }

        
    }
}