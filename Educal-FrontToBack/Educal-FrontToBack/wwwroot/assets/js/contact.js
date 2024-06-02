"use strict";


document.addEventListener('scroll' , () => {
    let header = document.querySelector ("header")
    let body = document.querySelector ('body')
    let logo = document.querySelector ('.dropdown, .pages > ul > li > a ')
    let link = document.querySelectorAll ('.nav-link')
    
    if (window.scrollY > 0) {
        header.classList.add('scroll')
        logo.style.color = "#000"
        //link.style.color = "#000"
        link.forEach(element => {
            element.style.color="#000"
        });
        // body.style.padding = "90px 0 0 0"
    }
    else {
        header.classList.remove('scroll')
        logo.style.color = "#fff"
        link.forEach(element => {
            element.style.color="#fff"
        });
        // body.style.padding = "0"
    }
})
//filterButtons.forEach(button => button.addEventListener("click", filterCards))

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