"use strict";


document.addEventListener('scroll' , () => {
    let header = document.querySelector ("header")
    let body = document.querySelector ('body')
    
    if (window.scrollY > 0) {
        header.classList.add('scroll')
        // body.style.padding = "90px 0 0 0"
    }
    else {
        header.classList.remove('scroll')
        // body.style.padding = "0"
    }
})

    let filterButtons=document.querySelectorAll(".nav button")
    let filterableCards = document.querySelectorAll (".cards .exam")
 
    const filterCards = e => {
        document.querySelector(".active").classList.remove("active")
        e.target.classList.add("active")

        filterableCards.forEach(card => {
            card.classList.add("hide");
            if(card.dataset.name === e.target.dataset.name || e.target.dataset.name === "all"){
                card.classList.remove("hide")
            }
        })
    }





    filterButtons.forEach(button => button.addEventListener("click", filterCards))
let bar=document.querySelector(".bar")
let sidebar = document.querySelector(".sidebar")
    function klik() {
        //sidebar.style.display = "none"
    }
  

    let sidebarr=document.querySelector(".sidebar button");
// console.log(sidebarr)
    sidebarr.addEventListener("click",function(){
     sidebar.style.display="none"
   
    // alert("sj")
    })






    
 
    
