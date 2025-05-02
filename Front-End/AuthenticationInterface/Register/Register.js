document.querySelector(".Register-Form").addEventListener("submit",async (e)=>{
e.preventDefault();
const UserName=document.querySelector('.UserName').value;
const Email=document.querySelector('.Email').value;
const Password=document.querySelector('.Password').value;
const User={
    UserName:UserName,
    Email:Email,
    Password:Password
}

const url="http://localhost:5296/User/Resgister";
const response= await fetch(url,{
    method:"POST",
    headers: {
    'Content-Type': 'application/json'
         },
     body: JSON.stringify(User)
})
if(response.ok){
    const token=response.text();
    localStorage.setItem('token',token)
    window.location.href="http://127.0.0.1:5500/api/Front-End/MainPage.html"
}


})

function LoginNavigator(){
window.location.href='http://127.0.0.1:5500/api/Front-End/AuthenticationInterface/Login/Log-In.html'
}
