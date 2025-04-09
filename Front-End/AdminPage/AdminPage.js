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

