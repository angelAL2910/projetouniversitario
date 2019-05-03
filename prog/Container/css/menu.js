@charset "utf-8";
/* CSS Document */


/* 
Ace Responsive Menu Plugin
Version: 1.0
Author: Samson Onna
E-mail: samson3d@gmail.com
----------------------------------------*/
/* Import Fonts
----------------------------------------*/
@import url(http://fonts.googleapis.com/css?family=Roboto);

/* Ace Responsive Menu
----------------------------------------*/
a {
    text-shadow: none;
    color: #0d638f;
}
ul {
    margin: 0px;
    padding: 0px;
}
.ace-responsive-menu {
    list-style: none;
    margin: 0;
    padding: 0;
    background: #333;  
    float:left;
    width:100%;
    font-family: 'Roboto', sans-serif;
    border-bottom: 3px solid #FD5025;        
}
.ace-responsive-menu li{
    list-style: none;
}
.ace-responsive-menu li ul {
    display:none;
}
.ace-responsive-menu > li {
    display: block;
    margin: 0;
    padding: 0;
    border: 0px;
    float: left;
}
.ace-responsive-menu li a {
        color:#c0c0c0;
}
.ace-responsive-menu > li > a {
    display: block;
    position: relative;
    margin: 0;
    border: 0px;
    padding: 18px 20px 18px 12px;
    text-decoration: none;
    font-size: 15px;
    font-weight: 300;
    color: #c0c0c0;
}
.ace-responsive-menu li a i {
    padding-right: 5px;
    color: #FF5737;
}
.ace-responsive-menu > li > a i {
    font-size: 16px;
    text-shadow: none;
    color: #FF5737;
}
.ace-responsive-menu li ul.sub-menu li a i {
    padding-right: 10px;
}
.ace-responsive-menu li.menu-active > a {
    background: #272727 !important;
    color:#fff;
}
.ace-responsive-menu li .menu-active {
    position: relative;
}
.ace-responsive-menu > li > a > .arrow:before {  
    margin-left: 15px;
    display: inline;
    font-size: 16px;
    font-family: FontAwesome;
    height: auto;
    content: "\f107";
    font-weight: 300;
    text-shadow: none;
    width: 10px;
    display: inline-block;
}
.ace-responsive-menu li ul.sub-menu li > a > .arrow:before {
    content: "\f105" !important;
}
.ace-responsive-menu > li > ul.sub-menu {
    display: none;
    list-style: none;
    clear: both;
    margin: 0;
    position: absolute;
}
.ace-responsive-menu li ul.sub-menu {
    background: #333;
}
.ace-responsive-menu li ul.sub-menu > li {
    width: 185px;
}
.ace-responsive-menu li ul.sub-menu li a {
    display: block;
    margin: 0px 0px;
    padding: 12px 20px 12px 15px;
    text-decoration: none;
    font-size: 13px;
    font-weight: normal;
    background: none;
}
.ace-responsive-menu > li > ul.sub-menu > li {
    position: relative;
}
.ace-responsive-menu > li > ul.sub-menu > li ul.sub-menu {
    position: absolute;
    left: 185px;
    top: 0px;
    display: none;
    list-style: none;
}
.ace-responsive-menu > li > ul.sub-menu > li ul.sub-menu > li ul.sub-menu {
    position: absolute;
    left: 185px;
    top: 0px;
    display: none;
    list-style: none;
}
.ace-responsive-menu > li > ul.sub-menu li > a > .arrow:before {
    float: right;
    margin-top: 1px;
    margin-right: 0px;
    display: inline;
    font-size: 16px;
    font-family: FontAwesome;
    height: auto;
    content: "\f104";
    font-weight: 300;
    text-shadow: none;
}

/* Menu Toggle Btn
----------------------------------------*/
.menu-toggle {
    display: none;
    float: left;
    width: 100%;
    background: #333;
}
.menu-toggle h3 {
    float: left;
    color: #FFF;
    padding: 0px 10px;
    font-weight: 600;
    font-size: 16px;
}
.menu-toggle .icon-bar {
    display: block !important;
    width: 18px;
    height: 2px;
    background-color: #F5F5F5 !important;
    -webkit-border-radius: 1px;
    -moz-border-radius: 1px;
    border-radius: 1px;
    -webkit-box-shadow: 0 1px 0 rgba(0, 0, 0, 0.25);
    -moz-box-shadow: 0 1px 0 rgba(0, 0, 0, 0.25);
    box-shadow: 0 1px 0 rgba(0, 0, 0, 0.25);
    margin: 3px;
}
.menu-toggle .icon-bar:hover {
    background-color: #F5F5F5 !important;
}
.menu-toggle #menu-btn {
    float: right;
    background: #202020;
    border: 1px solid #0C0C0C;
    padding: 8px;
    border-radius: 5px;
    cursor: pointer;
    margin: 10px;
}
.hide-menu {
    display: none;
}


/* Accordion Menu Styles
----------------------------------------*/

ul[data-menu-style="accordion"] {
    width: 250px;
}
ul[data-menu-style="accordion"] > li {    
    display: block;
    margin: 0;
    padding: 0;
    border: 0px;
    float: none !important;
}
ul[data-menu-style="accordion"] > li:first-child {
    border-top: 2px solid #FD5025;
}
ul[data-menu-style="accordion"] li ul.sub-menu > li {
    width: 100%;
}
ul[data-menu-style="accordion"] > li > a > .arrow:before {
    float: right;
    content: "\f105";
}
ul[data-menu-style="accordion"] li.menu-active > a > .arrow:before {
    content: "\f107" !important;
}
ul[data-menu-style="accordion"] > li > ul.sub-menu {
    position: static;
}
ul[data-menu-style="accordion"] > li > a i {
    padding-right: 10px;
    color: #FF5737;
}
ul[data-menu-style="accordion"] > li > ul.sub-menu > li ul.sub-menu {
    position: static;
}
ul[data-menu-style="accordion"] > li > ul.sub-menu > li ul.sub-menu > li ul.sub-menu {
    position: static;
}
ul[data-menu-style="accordion"] > li {
    border-bottom: 1px solid #242424;
}
ul[data-menu-style="accordion"] li a:hover {
    background: #272727 !important;
}
ul[data-menu-style="accordion"] ul.sub-menu li.menu-active > a > .arrow:before {
    content: "\f107" !important;
}

/* Vertical Menu Styles
----------------------------------------*/

ul[data-menu-style="vertical"] {
    width: 200px;
}
ul[data-menu-style="vertical"] > li {
    float: none;
}
ul[data-menu-style="vertical"] > li:first-child {
    border-top: 2px solid #FD5025;
}
ul[data-menu-style="vertical"] li ul.sub-menu > li {
    width: 100%;
}
ul[data-menu-style="vertical"] > li > a > .arrow:before {
    float: right;
    content: "\f105";
}
ul[data-menu-style="vertical"] > li.menu-active {
position:relative;
}
ul[data-menu-style="vertical"] > li > ul.sub-menu {
    position: absolute;
    left:200px;
    top:0px;
    width:200px;
}
ul[data-menu-style="vertical"] > li > a i {
    padding-right: 10px;
    color: #FF5737;
}
ul[data-menu-style="vertical"]> li > ul.sub-menu > li ul.sub-menu {
    position: absolute;
    width:200px;
    left: 200px;
}
ul[data-menu-style="vertical"] > li > ul.sub-menu > li ul.sub-menu > li ul.sub-menu {
    position: absolute;
    width:200px;
    left: 200px;
}
ul[data-menu-style="vertical"] > li {
    border-bottom: 1px solid #242424;
}
ul[data-menu-style="vertical"] li a:hover {
    background: #272727 !important;
}

/* Responsive Menu Styles
----------------------------------------*/
/*Note: change the max-width asper your requirment and change the same in aceResponsiveMenu({resizeWidth: "768" }) function*/

@media screen and (max-width: 768px) {
    .demo {
		width:96%;
		padding:2%;
    }
    ul[data-menu-style="vertical"] , ul[data-menu-style="accordion"],
    ul[data-menu-style="vertical"] li ul.sub-menu {
        width: 100% !important;
    } 
    .ace-responsive-menu {
        float: left;
        width:100%;
    }
    .ace-responsive-menu > li {
        border-bottom: 1px solid #242424;
       float: none;
    }   
    .ace-responsive-menu li a:hover {
        background: #272727 !important;
    }
    .ace-responsive-menu > li:first-child {
        border-top: 2px solid #FD5025;
    }    
    .ace-responsive-menu > li > a i {
        padding-right: 10px;
        color: #FF5737;
    }
    .ace-responsive-menu > li > a > .arrow:before {
        float: right;
        content: "\f105";
    }
    li.menu-active > a > .arrow:before {
        content: "\f107" !important;
    }
    .ace-responsive-menu li ul.sub-menu > li {
        width: 100%;
    }
    .ace-responsive-menu li ul.sub-menu li ul.sub-menu li a
        {
        padding-left: 30px;
    }  
    .ace-responsive-menu li ul.sub-menu li ul.sub-menu li ul.sub-menu li a 
       {
        padding-left: 50px;
    }  
    .ace-responsive-menu > li > ul.sub-menu {
        position: static;
    }
    .ace-responsive-menu > li > ul.sub-menu > li ul.sub-menu {
        position: static;
    }
    .ace-responsive-menu > li > ul.sub-menu > li ul.sub-menu > li ul.sub-menu {
        position: static;
    }
    .ace-responsive-menu li ul.sub-menu li.menu-active > a > .arrow:before {
        content: "\f107" !important;
    }
}
