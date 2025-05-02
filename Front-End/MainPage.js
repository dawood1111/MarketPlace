
document.querySelector('.Login-button').addEventListener("click",()=>{
    window.location.href='http://127.0.0.1:5500/api/Front-End/AuthenticationInterface/Login/Log-In.html';

   
})
document.querySelector('.Register-button').addEventListener('click',()=>{
    window.location.href='http://127.0.0.1:5500/api/Front-End/AuthenticationInterface/Register/Register.html';
})


window.addEventListener('DOMContentLoaded',()=>{
    const token= localStorage.getItem('token');
    if(token){


        const loginButton=document.querySelector(".Login-button");
        const RegisterButton=document.querySelector(".Register-button");
        const LogoutButton=document.querySelector('.Logout-button');

         loginButton.classList.add('hidden');
         RegisterButton.classList.add('hidden');
         LogoutButton.style.display='block';

    }else{
        LogoutButton.style.display='none';
    }

})


document.querySelector('.Logout-button').addEventListener('click',()=>{
    
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    window.location.href='http://127.0.0.1:5500/api/Front-End/AuthenticationInterface/Login/Log-In.html';
})