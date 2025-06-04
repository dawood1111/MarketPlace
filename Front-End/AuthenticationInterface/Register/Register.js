 
 
 
 


function SetError(element,message){
     const InputElement=element.parentElement;
    const ErrorDisplay=InputElement.querySelector('.error');
    ErrorDisplay.innerHTML=message;
    InputElement.classList.add('error')
    InputElement.classList.remove('success')
    

}
function SetSuccess(element){
    const InputElement=element.parentElement;
    const ErrorDisplay=InputElement.querySelector('.error');

    ErrorDisplay.innerHTML='';
    InputElement.classList.add('success')
     InputElement.classList.remove('error'); 


}


function ValidateInput(){
 const UserName = document.querySelector('.UserName');
    const Email = document.querySelector('.Email');
    const Password = document.querySelector('.Password');
 

    const hasUppercase=/[A-Z]/.test(Password.value)
 const hasNonAlphanumeric = /[^a-zA-Z0-9\s]/.test(Password.value);


    if(UserName.value.trim()===''){
        SetError(UserName,'UserName is required')

    }else{

        SetSuccess(UserName)
    }


    if(Email.value.trim()===''){
        SetError(Email,'Email is required')
    }else{
        SetSuccess(Email)
    }


    if(Password.value===''){
        SetError(Password,'Password is required')
    }else{
        SetSuccess(Password)
    }


    if(Password.value.length<10){
        SetError(Password,'Password length should be at least 10')
    }else{
        SetSuccess(Password)
    }

    if(!hasUppercase){
        SetError(Password,'Should contain at least one Upper case')
    }else{
        SetError(Password)
    }
    if(!hasNonAlphanumeric){

        SetError(Password,'Should contain NonAlphaNumaric')
    }else{
        SetSuccess(Password)
    }



}


 
 
 
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
 ValidateInput();

const url="http://localhost:5296/User/Resgister";

const response= await fetch(url,{
    method:"POST",
    headers: {
    'Content-Type': 'application/json'
         },
     body: JSON.stringify(User)
})
if(response.ok){
    const token= await response.text();
    localStorage.setItem('token',token)
    window.location.href="http://127.0.0.1:5500/api/Front-End/MainPage/MainPage.html"
}


})

function LoginNavigator(){
window.location.href='http://127.0.0.1:5500/api/Front-End/AuthenticationInterface/Login/Log-In.html'
}





