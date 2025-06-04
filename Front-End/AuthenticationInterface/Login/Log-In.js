function SetError(message){

    const errorDisplay = document.querySelector('.error');

    errorDisplay.innerHTML = message;


}




document.querySelector(".LogIn-Form").addEventListener("submit",async (e)=>{

    e.preventDefault();

    const Email=document.querySelector('.Email').value;
    const Password=document.querySelector('.Password').value;


    const user={
        Email:Email,
        Password:Password
    }

const url="http://localhost:5296/User/Login";
const response= await fetch(url,{
    method:"POST",
    headers: {
    'Content-Type': 'application/json'
         },
     body: JSON.stringify(user)
})


if (response.ok) {
 const token=await response.text();
const decodedToken = JSON.parse(atob(token.split('.')[1])); 
const role=decodedToken.role;

localStorage.setItem("token",token);
localStorage.setItem("role",role);
if(role==="Admin"){
    window.location.href="http://127.0.0.1:5500/api/Front-End/AdminPage/AdmimPage.html"
}else if(role==="User"){
    window.location.href="http://127.0.0.1:5500/api/Front-End/MainPage/MainPage.html"
}else{
throw new Error("Unknown Role")
}
 
}else{

            SetError('Invalid email/Password');
    }


})

function RegisterNavigator(){
    window.location.href='http://127.0.0.1:5500/api/Front-End/AuthenticationInterface/Register/Register.html'
}
   


