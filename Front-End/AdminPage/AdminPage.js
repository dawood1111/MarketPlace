

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
let SelectedId=null;
let SelectedID=null;

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
            hour: '2-digit', minute: '2-digit',
            hour12: false
        }).replace(',', '')}</td>
         <td>${order.totalPrice} JOD</td>
         <td>
         <p class="Status" style="color:white; padding:5px 1px 5px 1px;border-radius:5px">${order.orderStatus}</p>
         </td>
         <td>
         <button class="Update-btn" data-order-id=${order.id}>Update</button>
         </td>
         <td>
         <button class="Trash" data-id=${order.id}><i class="fa-solid fa-trash"></i></button>
         </td>
        
        `



        const datarow=document.querySelector(".OrderList");

        datarow.appendChild(row);
        const ExitBtn=document.querySelector('.exit');
        const UpdateContainer=document.querySelector(".UpdateContainer01");
        document.querySelectorAll('.Update-btn').forEach(button=>{
         
        button.addEventListener('click',()=>{
        SelectedId=button.getAttribute('data-order-id');
        UpdateContainer.classList.remove('hidden')

    })
  })

  ExitBtn.addEventListener('click',()=>{

    UpdateContainer.classList.add('hidden');
  })
    })

    const updateButton=document.querySelector('.Update-Btn');


    updateButton.addEventListener('click',async ()=>{
        const OrderStatus=document.querySelector('.menu').value;
        const url=`http://localhost:5296/UserAdmin/UpdateOrderStatus?OrderId=${SelectedId}`;
        
        const token=localStorage.getItem('token')
        const respone=await fetch(url,{
            method : 'PUT',
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
              },
              body: JSON.stringify({
                orderStatus: OrderStatus
              })
        })
        if(respone.ok){
            alert('OrderStatus has Updated');   
            console.log(OrderStatus)
        }
         
    })

 }

           document.querySelectorAll('.Status').forEach(orderStatus=>{
            switch(orderStatus.textContent.trim()){
                case'Pending':
                orderStatus.style.backgroundColor='rgba(240, 173, 78, 0.1)';
                orderStatus.style.color="#f0ad4e";
                break;
                case'Deliverd':
                orderStatus.style.backgroundColor='rgba(40, 167, 69, 0.1)';
                orderStatus.style.color="#28a745";
                
                break;
                case'Shipped':
                orderStatus.style.backgroundColor=' rgba(0, 123, 255, 0.1)';
                orderStatus.style.color="#007bff";
                break;
                case'Canceled':
                orderStatus.style.backgroundColor=' rgba(220, 53, 69, 0.1)';
                orderStatus.style.color="#dc3545";
                break;

            }
           })
           
          document.querySelectorAll('.Trash').forEach( Trash=>{
            Trash.addEventListener('click',async ()=>{
                SelectedID=Trash.getAttribute('data-id');
                const url=`http://localhost:5296/UserAdmin/DeleteOrder?OrderId=${SelectedID}`;
                const token=localStorage.getItem('token');
                const response=await fetch(url,{
                    method : 'DELETE',
                    headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
                    }
              
                })
                if(respone.ok){
                    alert(`Order ${SelectedID} was deleted`)
                }
            })


          })

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
            <td>${order.price} JOD</td>
            <td>${order.totalPrice} JOD</td>
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
        totalRevenuNum.textContent=`${data.totalRevenue} JOD`;
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
    const encodedCategory = encodeURIComponent(CategoryName);

     const url=`http://localhost:5296/UserAdmin/AddProduct?CategoryName=${encodedCategory}

`;
     const Name=document.querySelector('.Name').value;
     const Price = parseFloat(document.querySelector('.Price').value);
     const ImageInput=document.querySelector('.Image').files[0];
   

     const formData = new FormData();
     formData.append('name', Name);
     formData.append('price', Price);
     formData.append('Image', ImageInput);
     

     if(!Name||isNaN(Price)){
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
        alert('added Successfully')

          const productResponse = await response.json();
          const ProductTool={
          id:productResponse.id,
          name:Name,
          price:Price,
          category:CategoryName,
          imageUrl: `${encodeURIComponent(ImageInput.name)}`
        }

  /*
        let products = JSON.parse(localStorage.getItem('Products')) || [];

        products.push(ProductTool);


        localStorage.setItem('Products', JSON.stringify(products));

        localStorage.setItem('Category',CategoryName);
        console.log(localStorage.getItem('Products'))
        */

     }else{
        console.warn("Response body is empty.");
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

           const product={
            originalName:ProductName,
            updated:{
                name:NewProduct,
                price:NewPrice
            }

           }

          localStorage.setItem('UpdatedProduct',JSON.stringify(product));
          

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
            localStorage.setItem('DeletedProduct',DeleteName)
           
            
        }else{
          alert('Failed To Delete')
        }
    }

   }

   document.querySelector('.LogOut').addEventListener('click',()=>{
      
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    window.location.href='http://127.0.0.1:5500/api/Front-End/AuthenticationInterface/Login/Log-In.html';
   })
   async function OrderStatusCount(){
    const url="http://localhost:5296/UserAdmin/GetStatus";
    const token=localStorage.getItem('token');
    await fetch(url,{
        method:'GET',
        headers: {
            'Authorization': `Bearer ${token}`
        }
    }).then(res=>res.json()).
    then(data=>{
        const lable =data.map(item=>item.status);
        const count=data.map(item=>item.count);
        const OrderStatusChart=document.getElementById('OrderStatus');
         new Chart(OrderStatusChart,{
            type:'pie',
            data:{
                labels:lable,
                datasets:[{
                    label:'Orders Status Distribution',
                    data:count,
                    backgroundColor: ['#facc15', '#3b82f6', '#10b981']
                }]
            },options: {
                responsive: false, // Important for fixed size
                plugins: {
                  legend: {
                    position: 'bottom',
                    labels: {
                      color: 'black' // font color of legend
                    }
                  }
                }
              }
         })

         

    })
   }
   OrderStatusCount();


   async function PerDayUser(){
    const url="http://localhost:5296/UserAdmin/UserPerMonth";
    const token =localStorage.getItem('token');
   const res=  await fetch(url,{
        method:'GET',
        headers: {
            'Authorization': `Bearer ${token}`
        }
    })
const PerDayUser=document.getElementById('UserPerDay');
 const data = await res.json();

 const DayName = ["Saturday", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday"];
  const labels = data.map(item => DayName[item.Day - 1]);
  const counts = data.map(item => item.count);


  new Chart(PerDayUser, {
    type: 'line',
    data: {
      labels: labels,
      datasets: [{
        label: 'New Users Per Month',
        data: counts,
        borderColor: '#3b82f6',
        backgroundColor: 'rgba(59, 130, 246, 0.2)',
        fill: true,
        tension: 0.4
      }]
    },
    options: {
      responsive: true,
      plugins: {
        legend: {
          position: 'bottom',
        }
      },
      scales: {
        y: {
          beginAtZero: true,
          title: {
            display: true,
            text: 'Users'
          }
        }
      }
    }
  });
            
            }

PerDayUser();