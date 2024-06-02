"use strict";



let a = document.querySelectorAll("button")
let icon = document.querySelector (".icon")
let exam = document.querySelectorAll (".exam")
let cards = document.querySelector('.cards')
let exam_photo = document.querySelectorAll(".exam-photo")
let d_text = document.querySelectorAll(".d-text")
let card_title = document.querySelectorAll(".card-title")


a.forEach(d => {
    d.addEventListener('click', () => {
        document.querySelector('.active').classList.remove('active')
        d.classList.add('active');
        if(d.classList.contains("icon")) {
            cards.style.gridTemplateColumns = "1fr"
            exam.forEach(e => {
                e.style.display = "flex"
                // e.style.height = "px"
                exam_photo.forEach(b => {
                    b.style.height = "362px"
                    b.style.width = "372px"
                    b.style.flexShrink = "0"
                })
                d_text.forEach(c => {
                    c.classList.remove("d-none")
                })
                card_title.forEach(g => {
                    g.style.fontSize = "36px"
                })
            })
        }
        else {
            cards.style.gridTemplateColumns = "1fr 1fr 1fr"
            exam.forEach(e => {
                e.style.display = "block"
                e.style.height = "auto"
                exam_photo.forEach(b => {
                    b.style.height = "215px"
                })
                d_text.forEach(c => {
                    c.classList.add("d-none")
                })
                card_title.forEach(g => {
                    g.style.fontSize = "20px"
                })
            })
            // exam.style.display = "flex"
        }
    })
    
})




document.addEventListener('scroll' , () => {
    let header = document.querySelector ("header")
    let logo = document.querySelector ('.dropdown, .pages > ul > li > a ')
    let link = document.querySelectorAll ('li .nav-link')
    
    if (window.scrollY < 1) {
        header.classList.remove('scroll')
        logo.style.color = "#fff"
        link.forEach(f => {
            f.style.color="rgb(255,255,255)"
        });
        // body.style.padding = "90px 0 0 0"
    }
    else if(window.scrollY > 1){
        // body.style.padding = "0"
        header.classList.add('scroll')
        logo.style.color = "#000 "
        //link.style.color = "#000"
        link.forEach(f => {
            f.style.color="#000 !important"
        });
    }
})
// filterButtons.forEach(button => button.addEventListener("click", filterCards))

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




















    // a.forEach(element => {
    //     element.addEventListener("click",function(){
         
    //         a.forEach(element => {
    //             element.classList.remove("active")
    //             if (element.getElementsByClassName("active")) {
    //                 element.style.background = "#2b4eff"
    //             }
    //             else {
    //                 element.style.background = "transparent"
    //             }
    
    
    
    //     });
    
    //         element.classList.add("active")

           
    
    
    
    //       } )
    // }) 
    // a.forEach(element => {
    //     if (element.getElementsByClassName("active")) {
    //         element.style.background = "#2b4eff"
    //     }
    //     else {
    //         element.style.background = "transparent"
    //     }
    // })

