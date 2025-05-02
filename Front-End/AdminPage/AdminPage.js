document.addEventListener("DOMContentLoaded",function(){
    const navButton=document.querySelectorAll(".sections");
    const content=document.querySelectorAll(".content");

    function showSections(sectionId){
        content.forEach(content=>{
            content.style.display="none";

        })
        const selectsection=document.getElementById(sectionId);
        if(selectsection){
            selectsection.style.display="block";
        }
    

    }

   
navButton.forEach(button=>{
    button.addEventListener("click",()=>{
        const sectionId=button.getAttribute(("data-section"));
        showSections(sectionId);
    })
})



    
})


//OrderSection
async function GetOrders(){
 const url="http://localhost:5296/UserAdmin/GetAllOrders";
 const token=localStorage.getItem("token")
 const respone = await fetch(url,
    {
        method:"GET",
        headers:{
         "Authorization": `Bearer ${token}`,
         'Content-Type': 'application/json'
        }
 })
 if(respone.ok){
    const order= await respone.json();
    order.forEach(order=>{
        const row=document.createElement('tr');
        
        row.innerHTML=`
        <td>${order.id}</td>
        <td>
        ${order.shippingAddress.firstName}
        ${order.shippingAddress.lastName}
        </td>
         <td>${new Date(order.orderDate).toLocaleString('en-CA', {
            year: 'numeric', month: '2-digit', day: '2-digit',
            hour: '2-digit', minute: '2-digit', second: '2-digit',
            hour12: false
        }).replace(',', '')}</td>
         <td>${order.totalPrice}</td>
         <td><select>
         <option>${order.orderStatus}</option>
         <option>Processing</option>
         <option>Deliverd</option>
         <option>Canceled</option>
         </select></td>
        
        `
        

        const datarow=document.querySelector(".OrderList");
        datarow.appendChild(row)
    })
 }
}
GetOrders();

async function GetShippingAddress(){
    const url="http://localhost:5296/UserAdmin/GetAllOrders";
    const token=localStorage.getItem("token")
    const respone = await fetch(url,
       {
           method:"GET",
           headers:{
            "Authorization": `Bearer ${token}`,
            'Content-Type': 'application/json'
           }
    })
    if(respone.ok){
       const order= await respone.json();
       order.forEach(order=>{
           const row=document.createElement('tr');
          
           row.innerHTML=`
           <td>${order.id}</td>
           <td>${order.shippingAddress.firstName}</td>
            <td>${order.shippingAddress.country}</td>
            <td>${order.shippingAddress.city}</td>
            <td>${order.shippingAddress.streetAddress}</td>
            <td>${order.shippingAddress.phoneNumber}</td>
           
           `
           const datarow=document.querySelector(".ShippingList");
           datarow.appendChild(row)
       })
    }
   }
   GetShippingAddress();


  async function GetOrderItems(){
    const url="http://localhost:5296/UserAdmin/GetOrderItems";
    const token=localStorage.getItem("token")
    const respone = await fetch(url,
       {
           method:"GET",
           headers:{
            "Authorization": `Bearer ${token}`,
            'Content-Type': 'application/json'
           }
    })
    if(respone.ok){
       const order= await respone.json();
       order.forEach(order=>{
           const row=document.createElement('tr');
          
           row.innerHTML=`
           <td>${order.orderId}</td>
            <td>${order.id}</td>
            <td>${order.productName}</td>
            <td>${order.quantity}</td>
            <td>${order.price}</td>
            <td>${order.totalPrice}</td>
           `
           const datarow=document.querySelector(".OrderItems");
           datarow.appendChild(row)
       })
    }
   }
   GetOrderItems();



   async function TotalRevenue(){
    const url="http://localhost:5296/DashBoard/TotalRevenue";
    const token=localStorage.getItem("token")
    const respone = await fetch(url,
       {
           method:"GET",
           headers:{
            "Authorization": `Bearer ${token}`,
            'Content-Type': 'application/json'
           }
    })
    if(respone.ok){
        const totalRevenuNum=document.querySelector('.num1');
        const data=await respone.json();
        totalRevenuNum.textContent=`${data.totalRevenue}$`;
    }
   }


   TotalRevenue();

   async function TotalBuyers(){
    const url="http://localhost:5296/DashBoard/TotalBuyers";
    const token=localStorage.getItem("token")
    const respone = await fetch(url,
       {
           method:"GET",
           headers:{
            "Authorization": `Bearer ${token}`,
            'Content-Type': 'application/json'
           }
    })
    if(respone.ok){
        const totalBuyersNum=document.querySelector('.num2');
        const data=await respone.json();
        console.log("Total Buyers:", data);  // Debugging

        totalBuyersNum.textContent=data.totalBuyers;
    }
   }
   TotalBuyers();



   async function TotalOrders(){
    const url="http://localhost:5296/DashBoard/TotalOrders";
    const token=localStorage.getItem("token")
    const respone = await fetch(url,
       {
           method:"GET",
           headers:{
            "Authorization": `Bearer ${token}`,
            'Content-Type': 'application/json'
           }
    })
    if(respone.ok){
        const totalOrdersNum=document.querySelector('.num3');
        const data=await respone.json();
        console.log("Total orders:", data);  // Debugging

        totalOrdersNum.textContent=data.totalOrders;
    }
   }
   TotalOrders();



  async function  ProductManagement(Type,event){
    event.preventDefault();

    if(Type=='Add'){

    const CategoryName=document.querySelector('.category').value;
     const url=`http://localhost:5296/UserAdmin/AddProduct?CategoryName=${CategoryName}
`;
     const Name=document.querySelector('.Name').value;
     const Price = parseFloat(document.querySelector('.Price').value);
     const Description=document.querySelector('.Description').value;
     const ImageInput=document.querySelector('.Image').files[0];
   

     const formData = new FormData();
     formData.append('name', Name);
     formData.append('description', Description);
     formData.append('price', Price);
     formData.append('Image', ImageInput);
     

     if(!Name||!Description||isNaN(Price)){
        alert("Please fill in all filed correctly")
     }

     const token = localStorage.getItem("token");

     const response= await fetch(url,{
        method:"POST",
        headers: {
         'Authorization': `Bearer ${token}`
             },
         body:formData
         
     })
     
     if(response.ok){

        alert('Product Has Created Successfully');
     }
    
    }else if(Type=='Update'){


        const ProductName=document.querySelector('.productName').value;
        const NewProduct=document.querySelector('.newProduct').value;
        const NewDescription=document.querySelector('.newDescription').value;
        const NewPrice=document.querySelector('.newPrice').value;



        const values01={
            NewProduct:NewProduct,
            NewDescription:NewDescription,
            NewPrice:NewPrice
        }


         let url=`http://localhost:5296/UserAdmin/UpdateProduct?Name=${encodeURIComponent(ProductName)}`;

         if(NewProduct) url+=`&NewName=${encodeURIComponent(NewProduct)}`;
         if(NewPrice) url+=`&NewPrice=${encodeURIComponent(NewPrice)}`;
         if(NewDescription) url+=`&NewDescription=${encodeURIComponent(NewDescription)}`;


         const token = localStorage.getItem("token");

        const response= await fetch(url,{
            method:"PUT",
            headers: {
            'Content-Type': 'application/json',
             'Authorization': `Bearer ${token}`
                 },
             body: JSON.stringify(values01)
             
         })
         if(response.ok){
           if(NewProduct){
            alert("Product Name Updated")
          }
           else if(NewPrice){
            alert("Price Updated")
          }
           else if(NewDescription){
            alert("Description Updated")
          }
         }

    }else if(Type=='Delete'){
        const DeleteName=document.querySelector('.DeleteName').value;

        const url=`http://localhost:5296/UserAdmin/ProductDelete?Name=${DeleteName}`;
        const token=localStorage.getItem('token');
        const response = await fetch(url, {
            method: "DELETE",
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        if(response.ok){
            alert('Deleted Succefully');
        }
    }

   }

   document.querySelector('.LogOut').addEventListener('click',()=>{
      
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    window.location.href='http://127.0.0.1:5500/api/Front-End/AuthenticationInterface/Login/Log-In.html';
   })
