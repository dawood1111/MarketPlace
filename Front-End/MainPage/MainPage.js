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




let showcart = true;
let card = document.querySelector(".heads")
let cartItemsList = document.querySelector(".cart-items");
// let  button = document.querySelector(".button")
let proud = document.querySelectorAll(".proud")
document.querySelector(".btn").addEventListener("click",show)
function show() {
    if (showcart) {
        // عند إخفاء الكارت، نعيده إلى مكانه الطبيعي
        card.style.transform = "translateX(100%)"; // إخفاء الكارت على اليمين
    } else {
        // عند إظهار الكارت، نحركه لليمين
        card.style.transform = "translateX(-100%)"; // إظهار الكارت على اليمين
    }
    showcart = !showcart; // عكس القيمة

}


let cartCount = 0;
let totalPrice = 0; // إضافة متغير لحساب المجموع الكلي
const cartBadge = document.querySelector(".cart-badge");
const cartItemsContainer = document.querySelector(".cart-items");
const totalElement = document.querySelector(".total span"); // تحديد مكان المجموع داخل السلة

// تحديث عدد المنتجات في أيقونة السلة
function updateCartBadge() {
    cartBadge.textContent = cartCount;
    cartBadge.style.visibility = cartCount > 0 ? "visible" : "hidden";
}

// تحديث المجموع الكلي
function updateTotalPrice() {
    totalElement.textContent = `$${totalPrice.toFixed(2)}`; // عرض المجموع في السلة
}

//كبسة اتمام العملية 
document.addEventListener("DOMContentLoaded", function() {
    const checkoutBtn = document.querySelector(".all button");
    const popup = document.getElementById("popup");
    const overlay = document.getElementById("overlay");
    const totalSpanInPage = document.querySelector(".total span");
    const popupTotal = document.getElementById("popupTotal");
    const closeOrder = document.querySelector(".closeOrder");
    const cartItems = document.querySelector(".cart-items");

    checkoutBtn.addEventListener("click", function () {
        popupTotal.textContent = totalSpanInPage.textContent; // نسخ التوتال من الصفحة
        popup.style.display = "flex";

        overlay.style.display = "block";
    });




      window.submitOrder = async function () {

    const firstName = document.getElementById("FirstName").value;
    const lastName = document.getElementById("LastName").value;
    const Country = document.getElementById("Country").value;
    const City = document.getElementById("City").value;
    const PhoneNumber = document.getElementById("PhoneNumber").value;
    const StreetAddress = document.getElementById("StreetAddress").value;


    if (!firstName || !lastName || !Country||!City || !PhoneNumber || !StreetAddress) {
        alert("يرجى تعبئة جميع الحقول");
        return;
    }


    const cartItemElements = cartItems.querySelectorAll("li");
    const Items=Array.from(cartItemElements).map(
        item=>{
          return{
         productName : item.getAttribute("data-name"),
        quantity : parseInt(item.querySelector(".quantity").textContent)
          }
        })
    const url='http://localhost:5296/CartItem/AddCartItem';
    const token=localStorage.getItem('token');

    const response=await fetch(url,{

     method:"POST",
    headers: {
    'Content-Type': 'application/json',
    "Authorization": `Bearer ${token}`
         },
     body: JSON.stringify(Items)
    })


     if (!response.ok) {
        alert("فشل في إضافة عناصر السلة");
        return;
    }

   
         const ShippingAddressInfo={

            firstName:firstName,
            lastName:lastName,
            Country:Country,
            City:City,
            StreetAddress:StreetAddress,
            PhoneNumber:PhoneNumber

         }
    const url01='http://localhost:5296/api/shippingAddress/Add'
    const response01=await fetch(url01,{

     method:"POST",
    headers: {
    'Content-Type': 'application/json',
    "Authorization": `Bearer ${token}`
         },
     body: JSON.stringify(ShippingAddressInfo)
    })
   

     if (!response01.ok) {
        alert("فشل في إضافة عنوان الشحن");
        return;
    }

    const url02='http://localhost:5296/OrderItem/CreateOrderItem';
         
     const response02=await fetch(url02,{

     method:"POST",
    headers: {
    'Content-Type': 'application/json',
    "Authorization": `Bearer ${token}`
         }
    })
    if(response02.ok){

            alert(`تم تأكيد الطلب باسم: ${firstName}`);

    }
      

    

    

    

   
    popup.style.display = "none";
    overlay.style.display = "none";
      // تفريغ السلة
    cartItems.innerHTML = "";

      // تصفير المجموع
    totalSpanInPage.textContent = "$0";
     document.getElementById("FirstName").value="";
     document.getElementById("LastName").value="";
     document.getElementById("Country").value="";
     document.getElementById("City").value="";
     document.getElementById("PhoneNumber").value="";
     document.getElementById("StreetAddress").value="";

        popup.style.display = "none";
        overlay.style.display = "none";
        card.style.transform = "translateX(100%)";
    const cartBadge = document.querySelector(".cart-badge");
        if (cartBadge) {
        cartBadge.textContent = "0";
} 
    };


    closeOrder.addEventListener("click",function(){
    popup.style.display = "none";
        overlay.style.display = "none";
        card.style.transform = "translateX(100%)";
    })
    
});








document.addEventListener('DOMContentLoaded',async ()=>{
    const url=`http://localhost:5296/UserAdmin/GetProducts`

     const response=await fetch(url)
 const products = await response.json();
    products.forEach(product=>{
         const divEle = document.createElement('div');
      divEle.classList.add('proud');
      divEle.setAttribute('data-name', product.name);
      divEle.setAttribute('data-price', product.price);
      divEle.innerHTML = `
        <a href="#">
          <img src="image/${product.fileName}" alt="" class="ProImage">
          <p>${product.name}</p>
          <h5>${product.price} JOD</h5>
        </a>
        <br>
        <i class="fa-solid fa-cart-plus add-to-cart"></i>
      `;
      if(product.category_Id==1){

         const targetContainer = document.querySelector('.Category-Container[data-category="Fruit & Veg"]');
           if (targetContainer) {
              targetContainer.appendChild(divEle);
             }

    }else if(product.category_Id==2){
         const targetContainer = document.querySelector('.Category-Container[data-category="Poultry,Meat & Seafood"]');
           if (targetContainer) {
              targetContainer.appendChild(divEle);
             }

    }else if(product.category_Id==3){
         const targetContainer = document.querySelector('.Category-Container[data-category="Bakery"]');
           if (targetContainer) {
              targetContainer.appendChild(divEle);
             }

    }else if(product.category_Id==4){
         const targetContainer = document.querySelector('.Category-Container[data-category="Dairy & Eggs"]');
           if (targetContainer) {
              targetContainer.appendChild(divEle);
             }

    }else if(product.category_Id==5){
         const targetContainer = document.querySelector('.Category-Container[data-category="Deli"]');
           if (targetContainer) {
              targetContainer.appendChild(divEle);
             }

    }else if(product.category_Id==6){
         const targetContainer = document.querySelector('.Category-Container[data-category="Snaks & Chocolate"]');
           if (targetContainer) {
              targetContainer.appendChild(divEle);
             }

    }else if(product.category_Id==7){
         const targetContainer = document.querySelector('.Category-Container[data-category="Disposabels"]');
           if (targetContainer) {
              targetContainer.appendChild(divEle);
             }

    }else if(product.category_Id==8){
         const targetContainer = document.querySelector('.Category-Container[data-category="Frozen Food"]');
           if (targetContainer) {
              targetContainer.appendChild(divEle);
             }

    }else if(product.category_Id==9){
         const targetContainer = document.querySelector('.Category-Container[data-category="Coffe & Tea"]');
           if (targetContainer) {
              targetContainer.appendChild(divEle);
             }

    }else if(product.category_Id==10){
         const targetContainer = document.querySelector('.Category-Container[data-category="Drinks"]');
           if (targetContainer) {
              targetContainer.appendChild(divEle);
             }

    }else if(product.category_Id==11){
         const targetContainer = document.querySelector('.Category-Container[data-category="Cleaning & Laundry"]');
           if (targetContainer) {
              targetContainer.appendChild(divEle);
             }

    }else if(product.category_Id==12){
         const targetContainer = document.querySelector('.Category-Container[data-category="Cooking & Baking"]');
           if (targetContainer) {
              targetContainer.appendChild(divEle);
             }

    }
    
})

})


document.addEventListener("click", (event) => {
    const cartButton = event.target.closest(".fa-cart-plus");

    if (cartButton) {
        const productElement = cartButton.closest(".proud");

        if (!productElement) {
            console.error("Could not find product element");
            return;
        }

        const productName = productElement.getAttribute("data-name");
        const productPrice = parseFloat(productElement.getAttribute("data-price").replace('$', ''));

        cartCount++;
        totalPrice += productPrice;
        updateCartBadge();
        updateTotalPrice();

        // Check if product already exists
        let existingItem = [...cartItemsContainer.children].find(item =>
            item.getAttribute("data-name") === productName
        );

        let cartItem;

        if (existingItem) {
            let quantityElement = existingItem.querySelector(".quantity");
            let quantity = parseInt(quantityElement.textContent);
            quantityElement.textContent = quantity + 1;
            totalPrice += productPrice;
            updateTotalPrice();
            return; // No need to re-assign listeners for existing items
        } else {
            cartItem = document.createElement("li");
            cartItem.setAttribute("data-name", productName);
            cartItem.innerHTML = `
                ${productName} - $${productPrice.toFixed(2)} 
                <span class="quantity">1</span> 
                <button class="increase">+</button>
                <button class="decrease">-</button>
                <button class="remove">X</button> 
            `;
            cartItemsContainer.appendChild(cartItem);
        }

        // ✅ Only for NEW items, add event listeners:

        cartItem.querySelector(".increase").addEventListener("click", () => {
            let quantityElement = cartItem.querySelector(".quantity");
            let quantity = parseInt(quantityElement.textContent);
            quantityElement.textContent = quantity + 1;
            cartCount++;
            totalPrice += productPrice;
            updateCartBadge();
            updateTotalPrice();
        });

        cartItem.querySelector(".decrease").addEventListener("click", () => {
            let quantityElement = cartItem.querySelector(".quantity");
            let quantity = parseInt(quantityElement.textContent);

            if (quantity > 1) {
                quantityElement.textContent = quantity - 1;
                totalPrice -= productPrice;
            } else {
                cartItem.remove();
                totalPrice -= productPrice;
            }

            cartCount--;
            updateCartBadge();
            updateTotalPrice();
        });

        cartItem.querySelector(".remove").addEventListener("click", () => {
            let quantityElement = cartItem.querySelector(".quantity");
            let quantity = parseInt(quantityElement.textContent);

            totalPrice -= productPrice * quantity;
            cartCount -= quantity;
            updateCartBadge();
            updateTotalPrice();
            cartItem.remove();
        });
    }
});




