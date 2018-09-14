﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transunion.aspx.cs" EnableEventValidation="false" Inherits="WEB.NewBusiness.NewBusiness.Pages.Transunion" %>

<%@ Register Src="~/NewBusiness/UserControls/Common/WUCTransunion.ascx" TagPrefix="uc1" TagName="WUCTransunion" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        /*
CSS: Reset
Description: Codigo para equilibrar valores en diferentes navegadores.
Autor CSS: Dantelin Castro / Amaury Mateo
*/

        /*---------- RESET ----------*/
        * {
            margin: 0;
            padding: 0;
            border: 0;
            outline: 0;
            font-size: 100%;
            vertical-align: baseline;
            background: transparent;
        }

            *,
            *::after,
            *::before {
                -webkit-box-sizing: border-box;
                -moz-box-sizing: border-box;
                box-sizing: border-box;
                padding: 0;
                margin: 0;
            }

                *:focus {
                    outline: 0px none transparent;
                }

        html {
            -webkit-overflow-scrolling: touch;
            -webkit-tap-highlight-color: rgb(52,158,219);
            -webkit-text-size-adjust: 100%;
            -ms-text-size-adjust: 100%;
            height: 100%;
        }

        body {
            line-height: 1;
            height: 100%;
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
            background-color: #F4F3F0;
            font-size: 16px;
            color: #000;
            word-spacing: normal;
            background: #F5F4F1;
            font-family: Verdana, Geneva, Tahoma, sans-serif;
        }

            body form {
                height: 100% !important;
                display: block;
            }

        article, aside, details, figcaption, figure, footer, header, hgroup, main, nav, section, summary {
            display: block;
        }

        .clearfix::before, .clearfix::after {
            content: " ";
            display: table;
        }

        .clearfix::after {
            clear: both;
        }

        article, aside, details, figcaption, figure,
        footer, header, hgroup, menu, nav, section {
            display: block;
        }

        ul, ol {
            list-style: none;
        }

        a {
            text-decoration: none; /*word-wrap: break-word;*/
            vertical-align: baseline;
            background: transparent;
            color: inherit;
        }
        /* Remove the blue Webkit background when element is tapped */
        a, button {
            -webkit-tap-highlight-color: rgba(0,0,0,0);
        }

            a:focus {
                outline: 0;
            }

            a:active, a:hover {
                outline: 0;
                text-decoration: none;
            }

            a img {
                outline: none;
                border: 0;
            }

        svg:not(:root) {
            overflow: hidden;
        }

        b, strong {
            font-weight: bold;
        }

        h1, h2, h3, h4, h5, h6 {
            color: #222;
            padding-left: 1px;
            font-family: Verdana, Geneva, Tahoma, sans-serif;
            font-weight: normal;
        }

        p {
            font-size: 13px;
            line-height: 22px;
            text-align: left;
            margin: 5px 0;
            color: #222;
            font-family: Arial, Helvetica, sans-serif;
            -webkit-hyphens: auto;
            -moz-hyphens: auto;
        }

        textarea {
            resize: none;
            width: 100%;
            background: #FFF;
            font-family: Arial, Helvetica, sans-serif;
            line-height: 20px;
            color: #000000;
            font-size: 13px;
            padding: 5px;
        }

        iframe {
            display: block;
            border: 0;
            width: 100%;
        }

        ul, ol {
            list-style: none;
        }

        a {
            text-decoration: none;
            vertical-align: baseline;
            background: transparent;
            color: inherit;
        }

        a, button {
            -webkit-tap-highlight-color: rgba(0,0,0,0);
        }

            a:focus {
                outline: 0;
            }

            a:active, a:hover {
                outline: 0;
                text-decoration: none;
            }

            a img {
                outline: none;
                border: 0;
            }

        b, strong {
            font-weight: bold;
        }

        h1, h2, h3,
        h4, h5, h6 {
            color: #222;
            padding-left: 1px; /*font-family: Verdana, Geneva, Tahoma, sans-serif;*/
            font-weight: normal;
        }

        p {
            font-size: 13px;
            line-height: 22px;
            text-align: left;
            margin: 5px 0;
            color: #222;
            /*font-family:Arial, Helvetica, sans-serif;*/
            -webkit-hyphens: auto;
            -moz-hyphens: auto;
        }

        textarea {
            resize: none;
            width: 100%;
            background: #FFF; /*font-family:Arial, Helvetica, sans-serif;*/
            line-height: 16px;
            color: #002060;
            font-size: 13px;
            overflow: auto;
        }

        button,
        html input[type="button"],
        input[type="reset"],
        input[type="submit"] {
            cursor: pointer;
            -webkit-appearance: none;
            outline: none;
        }

        input[type="text"] {
            width: 100%;
            font-size: 0.8em;
            text-align: left;
            background-color: #FFFFFF;
            color: #002060;
        }

        button[disabled],
        html input[disabled] {
            cursor: default;
        }

        input[type="checkbox"],
        input[type="radio"] {
            box-sizing: border-box;
            padding: 0;
        }

        input[type="checkbox"] {
            margin: 0 auto;
        }

        input[disabled],
        textarea[disabled],
        select[disabled] {
            background-color: #ECECEC;
            color: #888 !important;
        }

        input, select {
            vertical-align: middle;
            font-size: 13px;
        }

        select {
            width: 100%;
            text-align: left;
        }

        tr a {
            text-decoration: none;
            vertical-align: middle;
        }

        table {
            clear: both;
            border-collapse: collapse;
            border-spacing: 0;
            white-space-collapse: discard;
            width: 100%;
        }

        td, th {
            border-spacing: 0px;
            vertical-align: middle;
            border: 0;
            font-size: 0.75em;
        }

        hr {
            display: block;
            clear: both;
            margin: 0.5em auto;
            border-style: inset;
            border-width: 1px;
            width: 100%;
            border-color: #222;
        }

        input[type="text"],
        select, textarea {
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            -ms-border-radius: 5px;
            -o-border-radius: 5px;
            border-radius: 5px;
        }



        /*jquery datepicker*/
        .no_border {
            border: 0 !important;
        }

        .ui-datepicker.ui-widget.ui-widget-content.ui-helper-clearfix.ui-corner-all {
            max-width: 198px !important;
            z-index: 999 !important;
        }

        .ui-datepicker .ui-datepicker-title span {
            font-size: 13px !important;
            font-weight: bold;
            !important;
        }

        .ui-datepicker .ui-datepicker-title {
            line-height: 1.25em !important;
            height: 28px;
        }

        .ui-datepicker .ui-datepicker-prev-hover {
            left: 2px !important;
            top: 2px !important;
        }

        .ui-datepicker .ui-datepicker-next-hover {
            right: 2px !important;
            top: 2px !important;
        }

        .datepicker {
            background: #fff url(../images/calendar_icon.png) no-repeat 99% center !important;
        }

        .ui-datepicker-month {
            width: 42% !important;
        }

        .ui-datepicker-year {
            width: 58% !important;
        }

        .ui-datepicker-title select {
            background: none !important;
            padding: 0 !important;
            font-size: 13px !important;
        }


        /*---------- MENU SUPERIOR ----------*/

        .rds20 {
            -webkit-border-radius: 20px;
            border-radius: 20px;
        }

        .rds5 {
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            -ms-border-radius: 5px;
            -o-border-radius: 5px;
            border-radius: 5px;
        }

        .box_shw {
            -webkit-box-shadow: 0 0 10px 0 #C7C7C7;
            box-shadow: 0 0 10px 0 #C7C7C7;
        }

        .minH_190 {
            min-height: 190px;
        }

        /*Margenes*/
        .m0 {
            margin: 0px !important;
        }

        .mB {
            margin-bottom: 10px !important;
        }

        .mB20 {
            margin-bottom: 20px !important;
        }

        .mB30 {
            margin-bottom: 30px !important;
        }

        .mT10 {
            margin-top: 10px !important;
        }

        .mT15 {
            margin-top: 15px !important;
        }

        .mT16 {
            margin-top: 16px !important;
        }

        .mT20 {
            margin-top: 20px !important;
        }

        .mT35 {
            margin-top: 35px !important;
        }

        .mT50 {
            margin-top: 50px !important;
        }

        .mR-1-p {
            margin-right: 1% !important;
        }

        .mR0 {
            margin-right: 0px !important;
        }

        .mR {
            margin-right: 10px !important;
        }

        .mR15 {
            margin-right: 15px !important;
        }

        .mR20 {
            margin-right: 20px !important;
        }

        .mR-2-p {
            margin-right: 2% !important;
        }

        .mR-3-p {
            margin-right: 3% !important;
        }

        .mL10 {
            margin-left: 10px !important;
        }

        .mL20 {
            margin-left: 20px !important;
        }

        .mL30 {
            margin-left: 30px !important;
        }

        .vTop {
            vertical-align: top !important;
        }

        .row_A {
            width: 100% !important;
            float: left;
        }


        .ico_G {
            width: 20px;
            height: 20px;
            display: block;
        }

        .ico_G30 {
            width: 30px;
            height: 30px;
            display: block;
        }

        .plus {
            background: url(../images/plus.png) no-repeat center center;
            background-size: 100%;
            margin: 2px 5px 0px;
        }

        .edit_paper {
            background: url(../images/edit_paper.png) no-repeat center center;
            background-size: 100%;
            margin: 2px 5px 0px;
        }

        .pdf_ico {
            background: url(../images/pdf_icon.png);
            background-size: 100%;
        }

        .eye {
            background-image: url(../images/eye.png);
        }

        .eye_no {
            background-image: url(../images/eye_no.png);
        }

        .excel {
            padding-right: 35px;
            position: relative;
        }

        .cotizaPP {
            min-height: 880px !important;
            height: 90% !important;
            width: 100% !important;
            max-width: 1400px !important;
        }

        .excel:after {
            content: "";
            width: 26px;
            height: 26px;
            background-image: url(../images/excell.png);
            position: absolute;
            right: 10px;
        }

        .excel {
            background-image: url(../images/excell.png);
        }

        .wd80 {
            width: 80% !important;
        }

        .wd49 {
            width: 49% !important;
        }

        .wd50 {
            width: 50% !important;
        }

        .wd40 {
            width: 40% !important;
        }

        .hg30 {
            height: 30px;
        }

        .alignR {
            text-align: right !important;
        }

        .alignL {
            text-align: left !important;
        }

        .alignC {
            text-align: center !important;
        }

        .pdL15 {
            padding-left: 15% !important;
        }

        .pdR10 {
            padding-right: 10px !important;
        }

        .pdB10 {
            padding-bottom: 10px !important;
        }

        .min-hg60 {
            min-height: 60px;
        }

        .bdrT_cyan {
            border-top: 2px solid #DDEBF7;
        }

        .textarea_pink {
            min-height: 135px;
            text-align: left !important;
            padding-left: 15px;
        }

        .select_salud_ft {
            position: absolute;
            top: -41px;
            right: 4px;
            width: 120px;
        }

        .btn {
            -webkit-border-radius: 10px;
            border-radius: 10px;
        }

            .btn.bg_grd_vd {
                color: #fff;
                -webkit-box-shadow: 0 2px 5px 0 #808080;
                box-shadow: 0 2px 5px 0 #808080;
            }

        .icon_checked2 {
            background: url(../images/cotejo.png) no-repeat center center;
            width: 30px;
            height: 30px;
            display: block;
        }

        .paper {
            background: url(../images/ico_formas_plantillas/paper.png) no-repeat center center;
            background-size: 100%;
        }

        .btn_pasos {
            padding: 5px 20px;
            margin-right: 32px;
            min-width: 165px;
            height: auto !important;
        }

        .flecha_comp {
            min-height: 390px;
        }

        .arr_comp {
            width: 80%;
            margin: 163px auto 0;
            height: 50px;
            position: relative;
            background-color: #92D050;
            -webkit-box-shadow: 0 2px 5px 0 #808080;
            box-shadow: 0 2px 5px 0 #808080;
        }

            .arr_comp:after,
            .arr_comp:before {
                content: "";
                position: absolute;
                width: 0px;
                height: 0px;
                border-style: solid;
                border-width: 50px;
                top: -25px;
            }

        .prl {
            position: relative;
        }

        .arr_comp:after {
            border-left-width: 25px;
            border-color: transparent transparent transparent #92D050;
            right: -74px;
        }

        .arr_comp:before {
            border-right-width: 25px;
            border-color: transparent #92D050 transparent transparent;
            left: -74px;
        }

        .azul_c {
            background: #5d82cb;
            background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJod…EiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
            background: -moz-linear-gradient(top, #5d82cb 0%, #3e70ca 35%, #2f62bb 100%);
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#5d82cb), color-stop(35%,#3e70ca), color-stop(100%,#2f62bb));
            background: -webkit-linear-gradient(top, #5d82cb 0%,#3e70ca 35%,#2f62bb 100%);
            background: -o-linear-gradient(top, #5d82cb 0%,#3e70ca 35%,#2f62bb 100%);
            background: -ms-linear-gradient(top, #5d82cb 0%,#3e70ca 35%,#2f62bb 100%);
            background: linear-gradient(to bottom, #5d82cb 0%,#3e70ca 35%,#2f62bb 100%);
            -webkit-box-shadow: 0 2px 5px 0 #111;
            box-shadow: 0 2px 5px 0 #111;
            color: #fff;
            text-shadow: 0px 1px 2px rgba(0,0,0, .50);
        }

        .blocked:before {
            background-color: #333;
            position: absolute;
            left: 0;
            top: 0;
            opacity: 0.8;
            width: 100%;
            height: 100%;
        }

        .pttl span {
            height: 50px !important;
            line-height: 40px !important;
        }

            .pttl span.bg_grd_vd {
                background: #7fb75d;
                background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodâ€¦EiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
                background: -moz-linear-gradient(top, #7fb75d 0%, #62a336 100%);
                background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#7fb75d), color-stop(100%,#62a336));
                background: -webkit-linear-gradient(top, #7fb75d 0%,#62a336 100%);
                background: -o-linear-gradient(top, #7fb75d 0%,#62a336 100%);
                background: -ms-linear-gradient(top, #7fb75d 0%,#62a336 100%);
                background: linear-gradient(to bottom, #7fb75d 0%,#62a336 100%);
                font-weight: 600;
                font-size: 1em;
            }

        .label_plus_input.par.pttl {
            height: 60px;
            margin-bottom: 20px;
        }

        .divScrollPP {
            max-height: 885px !important;
            overflow-x: hidden;
        }

        .btn.bgreen {
            -webkit-border-radius: 0px !important;
            border-radius: 0px !important;
        }

        .pay_ico {
            background: url(../images/Pay.png) no-repeat center center;
            background-size: 100%;
            width: 74px;
            height: 75px;
            position: absolute;
            left: -30px;
            bottom: 0px;
        }

        .info_pagos .boxes_step .boxes .box_btn:last-child {
            line-height: 40px;
            padding-left: 70px;
        }

        .col-6.selectDBL {
            width: 50% !important;
        }

        .selectDBL select {
            width: 49% !important;
        }

            .selectDBL select:first-child {
                margin-right: 2%;
            }

        .pd0 {
            padding: 0px !important;
        }

        .txtBold {
            font-weight: bold !important;
        }

        .em1 {
            font-size: 1em !important;
        }

        .em1-2 {
            font-size: 1.2em !important;
        }

        .em1-3 {
            font-size: 1.3em !important;
        }

        article {
            position: relative;
        }

        input[type="checkbox"] {
            width: 18px;
            height: 18px;
            background: #fff;
        }

        /* SQUARED FOUR */
        .check_lb input[type="checkbox"] {
            visibility: hidden;
        }

        .check_lb {
            width: 20px;
            position: relative;
        }

            .check_lb label {
                cursor: pointer;
                position: absolute;
                width: 20px;
                height: 20px;
                top: 0;
                border-radius: 4px;
                background: #fff;
                border: 1px solid #4472C4;
                margin: 0px;
            }

                .check_lb label:after {
                    -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=0)";
                    filter: alpha(opacity=0);
                    opacity: 0;
                    content: '';
                    position: absolute;
                    width: 15px;
                    height: 7px;
                    background: transparent;
                    top: 5px;
                    left: 2px;
                    border: 3px solid #00B111;
                    border-top: none;
                    border-right: none;
                    -webkit-transform: rotate(-45deg);
                    -moz-transform: rotate(-45deg);
                    -o-transform: rotate(-45deg);
                    -ms-transform: rotate(-45deg);
                    transform: rotate(-45deg);
                }

                .check_lb label:hover::after {
                    -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=30)";
                    filter: alpha(opacity=30);
                    opacity: 0.5;
                }

            .check_lb input[type=checkbox]:checked + label:after {
                -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=100)";
                filter: alpha(opacity=100);
                opacity: 1;
            }

        .main_menu_nm {
            width: 100%;
            /*float: left;
	min-height: 90px;*/
            position: relative; /*height:90px;*/
            top: 0;
        }

        .icon_nm-menu {
            background-image: url(../images/menu_toogle.svg);
            background-size: 22px 20px;
            background-repeat: no-repeat;
            background-position: 32px 0px;
        }

        a, li {
            -webkit-tap-highlight-color: rgba(0, 0, 0, 0);
        }


        /*global css aplica para todas las resoluciones*/
        .nav_nm ul {
            /*max-width: 1240px;*/
            margin: 0;
            padding: 0;
            list-style: none;
            /*font-size: 1.5em;*/
            font-weight: 300;
        }

        .displayNone {
            display: none;
        }

        .nav_nm li span {
            display: block;
        }

            .nav_nm li span.icon_nm {
                width: 48px;
                height: 48px;
                text-align: center;
                margin: 0 auto;
                vertical-align: middle;
                background-repeat: no-repeat;
                background-position: center center;
                background-size: 60%;
                border-radius: 100%;
                -moz-border-radius: 100%;
                -webkit-border-radius: 100%;
            }

        .mB {
            margin-bottom: 10px !important;
        }

        .mB20 {
            margin-bottom: 20px !important;
        }

        .mB30 {
            margin-bottom: 30px !important;
        }

        .mT30 {
            margin-top: 30px !important;
        }

        .mT22 {
            margin-top: 22px !important;
        }

        .wd940 {
            width: 940px !important;
        }
        /*disabled*/
        .disabled.button,
        .disabled {
            border-color: #b0b0b0 !important;
            color: #686868 !important;
            background: #fdfdfd;
            background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIwJSIgc3RvcC1jb2xvcj0iI2ZkZmRmZCIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjEwMCUiIHN0b3AtY29sb3I9IiNlMGUwZTAiIHN0b3Atb3BhY2l0eT0iMSIvPgogIDwvbGluZWFyR3JhZGllbnQ+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjEiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
            background: -moz-linear-gradient(top, #fdfdfd 0%, #e0e0e0 100%);
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#fdfdfd), color-stop(100%,#e0e0e0));
            background: -webkit-linear-gradient(top, #fdfdfd 0%,#e0e0e0 100%);
            background: -o-linear-gradient(top, #fdfdfd 0%,#e0e0e0 100%);
            background: -ms-linear-gradient(top, #fdfdfd 0%,#e0e0e0 100%);
            background: linear-gradient(to bottom, #fdfdfd 0%,#e0e0e0 100%);
            cursor: default;
        }

            .disabled.embossed:hover {
                -moz-box-shadow: -3px -5px 3px rgba(0,0,0,.25) inset, 2px 5px 5px rgba(255,255,255,.5) inset;
                box-shadow: -3px -5px 3px rgba(0,0,0,.25) inset, 2px 5px 5px rgba(255,255,255,.5) inset;
                -ms-box-shadow: -3px -5px 3px rgba(0,0,0,.25) inset, 2px 5px 5px rgba(255,255,255,.5) inset;
                -o-box-shadow: -3px -5px 3px rgba(0,0,0,.25) inset, 2px 5px 5px rgba(255,255,255,.5) inset;
                -webkit-box-shadow: -3px -5px 3px rgba(0,0,0,.25) inset, 2px 5px 5px rgba(255,255,255,.5) inset;
            }

            .disabled.button,
            .disabled.boton {
                color: #686868 !important;
            }

            .disabled span {
                border-color: #b0b0b0 !important;
            }

                .disabled span.add {
                    background-image: url(../Content/images/add_icon_dis.png);
                }

                .disabled span.submit_cases {
                    background-image: url(../Content/images/submit_cases_dis.png);
                }

                .disabled span.delete {
                    background-image: url(../Content/images/delete_dis.png);
                }

                .disabled span.edit {
                    background-image: url(../Content/images/edit_dis.png);
                }
        /*============| BOTONES MENU PRINCIPAL | =============*/

        .ncliente_ico {
            background-image: url(../images/top-menu-icon/ncliente.png);
            border: 2px solid #50B33A;
        }

            .ncliente_ico + span {
                color: #50B33A;
            }

        .dashBoard_n {
            background-image: url(../images/top-menu-icon/dashboard.png);
            border: 2px solid #F99A12;
        }

            .dashBoard_n + span {
                color: #F99A12;
            }

        .abono {
            background-image: url(../images/top-menu-icon/abono.png);
            border: 2px solid #E3CC9E;
        }

            .abono + span {
                color: #E3CC9E;
            }

        .renovacion {
            background-image: url(../images/top-menu-icon/renovacion.png);
            border: 2px solid #4383DB;
        }

            .renovacion + span {
                color: #4383DB;
            }

        .cancelacion {
            background-image: url(../images/top-menu-icon/cancelacion.png);
            border: 2px solid #FF1616;
        }

            .cancelacion + span {
                color: #FF1616;
            }

        .reclamacion {
            background-image: url(../images/top-menu-icon/voice10.png);
            border: 2px solid #E9FFC7;
        }

            .reclamacion + span {
                color: #E9FFC7;
            }

        .cotiza {
            background-image: url(../images/top-menu-icon/cotizaciones.png);
            border: 2px solid #73C2BA;
        }

            .cotiza + span {
                color: #73C2BA;
            }

        .commu {
            background-image: url(../images/top-menu-icon/communication.png);
            border: 2px solid #CF7751;
        }

            .commu + span {
                color: #CF7751;
            }

        .pendiente {
            background-image: url(../images/top-menu-icon/pending.png);
            border: 2px solid #F4EADA;
        }

            .pendiente + span {
                color: #F4EADA;
            }

        .formu {
            background-image: url(../images/top-menu-icon/form.png);
            border: 2px solid #40B0C9;
        }

            .formu + span {
                color: #40B0C9;
            }

        .centretenimiento {
            background-image: url(../images/top-menu-icon/training.png);
            border: 2px solid #00AFF1;
        }

            .centretenimiento + span {
                color: #00AFF1;
            }

        .cpoliza {
            background-image: url(../images/top-menu-icon/search102.png);
            border: 2px solid #CDDA60;
        }

            .cpoliza + span {
                color: #CDDA60;
            }

        .contacto_t {
            background-image: url(../images/top-menu-icon/contacts13.png);
            border: 2px solid #B2F3FF;
        }

            .contacto_t + span {
                color: #B2F3FF;
            }

        .rdinversion {
            background-image: url(../images/top-menu-icon/rdinversion.png);
            border: 2px solid #DF07F8;
        }

            .rdinversion + span {
                color: #DF07F8;
            }


        .icon_nm + span {
            font-size: 0.75em;
            text-align: center;
            margin-top: 5px;
            font-weight: 400;
        }

        .nav_nm a {
            height: 90px;
            display: block;
            color: rgba(255, 255, 255, .9);
            text-decoration: none;
            padding: 0.3em 0.25em;
            text-shadow: 0px 1px 2px rgba(0,0,0, .65);
            text-align: center;
            border-left: 1px solid transparent; /*#001320*/
            /*-webkit-box-shadow: 0 0 2px 1px rgba(13, 46, 77, 0.55) inset;
		 -moz-box-shadow: 0 0 2px 1px rgba(13, 46, 77, 0.55) inset;
		 box-shadow: 0 0 2px 1px rgba(13, 46, 77, 0.55) inset;
		*/
            -webkit-transition: color 0.5s, background 0.5s, height 0.5s;
            -moz-transition: color 0.5s, background 0.5s, height 0.5s;
            -o-transition: color 0.5s, background 0.5s, height 0.5s;
            -ms-transition: color 0.5s, background 0.5s, height 0.5s;
            transition: color 0.5s, background 0.5s, height 0.5s;
        }

        .no-touch .nav_nmtoogle a:hover,
        .nav_nm li a:hover,
        .nav_nmtoogle a:active_nm,
        .nav_nmtoogle a:focus, a.active_nm, .nav_nm li a.active_nm {
            /*height: 10em;*/
            background: #11385b;
            background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIwJSIgc3RvcC1jb2xvcj0iIzExMzg1YiIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjEwMCUiIHN0b3AtY29sb3I9IiMwMjExMjIiIHN0b3Atb3BhY2l0eT0iMSIvPgogIDwvbGluZWFyR3JhZGllbnQ+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjEiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
            background: -moz-linear-gradient(top, #11385b 0%, #021122 100%);
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#11385b), color-stop(100%,#021122));
            background: -webkit-linear-gradient(top, #11385b 0%,#021122 100%);
            background: -o-linear-gradient(top, #11385b 0%,#021122 100%);
            background: -ms-linear-gradient(top, #11385b 0%,#021122 100%);
            background: linear-gradient(to bottom, #11385b 0%,#021122 100%);
            -webkit-box-shadow: inset 0px 1px 0px 0px rgba(255,255,255, .1), 0px 1px 0px 0px rgba(0,0,0, .1);
            -moz-box-shadow: inset 0px 1px 0px 0px rgba(255,255,255, .1), 0px 1px 0px 0px rgba(0,0,0, .1);
            box-shadow: inset 0px 1px 0px 0px rgba(255,255,255, .1), 0px 1px 0px 0px rgba(0,0,0, .1);
            border-left: 1px solid #0C131F;
        }



        .no-touch .nav_nm ul:hover a {
            color: rgba(255, 255, 255, .5);
        }

            .no-touch .nav_nm ul:hover a:hover {
                color: #FFF;
                background: #11385b;
                background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIwJSIgc3RvcC1jb2xvcj0iIzExMzg1YiIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjEwMCUiIHN0b3AtY29sb3I9IiMwMjExMjIiIHN0b3Atb3BhY2l0eT0iMSIvPgogIDwvbGluZWFyR3JhZGllbnQ+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjEiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
                background: -moz-linear-gradient(top, #11385b 0%, #021122 100%);
                background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#11385b), color-stop(100%,#021122));
                background: -webkit-linear-gradient(top, #11385b 0%,#021122 100%);
                background: -o-linear-gradient(top, #11385b 0%,#021122 100%);
                background: -ms-linear-gradient(top, #11385b 0%,#021122 100%);
                background: linear-gradient(to bottom, #11385b 0%,#021122 100%);
                -webkit-box-shadow: inset 0px 1px 0px 0px rgba(255,255,255, .1), 0px 1px 0px 0px rgba(0,0,0, .1);
                -moz-box-shadow: inset 0px 1px 0px 0px rgba(255,255,255, .1), 0px 1px 0px 0px rgba(0,0,0, .1);
                box-shadow: inset 0px 1px 0px 0px rgba(255,255,255, .1), 0px 1px 0px 0px rgba(0,0,0, .1);
                border-left: 1px solid #0C131F;
            }



        .no-touch .nav_nmtoogle a:hover,
        .nav_nmtoogle a:active_nm,
        .nav_nmtoogle a:focus, a.active_nm, .nav_nm li a.active_nm {
            /*height: 10em;*/
            background: #11385b;
            background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIwJSIgc3RvcC1jb2xvcj0iIzExMzg1YiIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjEwMCUiIHN0b3AtY29sb3I9IiMwMjExMjIiIHN0b3Atb3BhY2l0eT0iMSIvPgogIDwvbGluZWFyR3JhZGllbnQ+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjEiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
            background: -moz-linear-gradient(top, #11385b 0%, #021122 100%);
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#11385b), color-stop(100%,#021122));
            background: -webkit-linear-gradient(top, #11385b 0%,#021122 100%);
            background: -o-linear-gradient(top, #11385b 0%,#021122 100%);
            background: -ms-linear-gradient(top, #11385b 0%,#021122 100%);
            background: linear-gradient(to bottom, #11385b 0%,#021122 100%);
            -webkit-box-shadow: inset 0px 1px 0px 0px rgba(255,255,255, .1), 0px 1px 0px 0px rgba(0,0,0, .1);
            -moz-box-shadow: inset 0px 1px 0px 0px rgba(255,255,255, .1), 0px 1px 0px 0px rgba(0,0,0, .1);
            box-shadow: inset 0px 1px 0px 0px rgba(255,255,255, .1), 0px 1px 0px 0px rgba(0,0,0, .1);
        }

        /* Styling the toggle menu link and hiding it */
        .nav_nm .nav_nmtoogle {
            display: none;
            width: 100%;
            text-align: left;
            color: #FFF;
            font-size: 1em;
            background: none;
            border: none;
            border-top: 1px solid #020e23;
            border-bottom: 4px solid #020e23;
            padding-top: 2px;
            padding-left: 30px;
            cursor: pointer;
            line-height: 32px;
            background: #01254c;
            -webkit-box-shadow: inset 0px 1px 0px 0px rgba(255,255,255, .1), 0px 1px 0px 0px rgba(0,0,0, .1);
            -moz-box-shadow: inset 0px 1px 0px 0px rgba(255,255,255, .1), 0px 1px 0px 0px rgba(0,0,0, .1);
            box-shadow: inset 0px 1px 0px 0px rgba(255,255,255, .1), 0px 1px 0px 0px rgba(0,0,0, .1);
        }

        .nav_nmtoogle i {
            z-index: 3;
            padding-left: 50px;
        }

        .no-touch .nav_nmtoogle a:hover,
        .nav_nmtoogle a:active_nm,
        .nav_nmtoogle a:focus, a.active_nm, .nav_nm li a.active_nm {
            /*height: 10em;*/
            background: #11385b;
            background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIwJSIgc3RvcC1jb2xvcj0iIzExMzg1YiIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjEwMCUiIHN0b3AtY29sb3I9IiMwMjExMjIiIHN0b3Atb3BhY2l0eT0iMSIvPgogIDwvbGluZWFyR3JhZGllbnQ+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjEiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
            background: -moz-linear-gradient(top, #11385b 0%, #021122 100%);
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#11385b), color-stop(100%,#021122));
            background: -webkit-linear-gradient(top, #11385b 0%,#021122 100%);
            background: -o-linear-gradient(top, #11385b 0%,#021122 100%);
            background: -ms-linear-gradient(top, #11385b 0%,#021122 100%);
            background: linear-gradient(to bottom, #11385b 0%,#021122 100%);
            -webkit-box-shadow: 0 0 2px 1px rgba(13, 46, 77, 0.55) inset;
            -moz-box-shadow: 0 0 2px 1px rgba(13, 46, 77, 0.55) inset;
            box-shadow: 0 0 2px 1px rgba(13, 46, 77, 0.55) inset;
        }


        .main_menu_nm a.active_nm,
        .nav_nm li a.active_nm {
            color: #FFF !important;
        }

        .main_menu_nm {
            height: auto;
        }

        /*==================================*\
	HEADER
\*==================================*/

        .main_menu_nm {
            height: 90px;
            width: 35%; /*float: left;min-width: 410px;}
.main_menu_nm {
	width: 100%; 
	/*float: left;*/
            background-color: #001B2B;
            background-image: -moz-linear-gradient(top,#001B2B,#001320);
            background-image: -webkit-linear-gradient(top,#001B2B,#001320);
            background-image: -webkit-gradient(linear,left top,left bottom,color-stop(0%,#001B2B), color-stop(100%,#001320));
            background-image: -o-linear-gradient(top,#001B2B,#001320);
            background-image: linear-gradient(to bottom,#001B2B,#001320);
        }

        /* Sub menu ATLANTICA PV -------------------- */


        .drop-nav_nm {
            background: #2c3e50;
        }

        .name_dd {
            width: 100%;
            height: 105px;
            color: #fff;
            font-weight: normal;
            z-index: 1;
            top: 33px;
            left: 0;
            line-height: 150px;
            text-shadow: 1px 1px 5px #001827;
            -webkit-border-radius: 10px 10px 0 0;
            border-radius: 10px 10px 0 0;
            background: #11385b;
            background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJod…EiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
            background: -moz-linear-gradient(top, #11385b 0%, #021122 100%);
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#11385b), color-stop(100%,#021122));
            background: -webkit-linear-gradient(top, #11385b 0%,#021122 100%);
            background: -o-linear-gradient(top, #11385b 0%,#021122 100%);
            background: -ms-linear-gradient(top, #11385b 0%,#021122 100%);
            background: linear-gradient(to bottom, #11385b 0%,#021122 100%);
            -webkit-box-shadow: 0 0 2px 1px rgba(13, 46, 77, 0.55) inset;
            -moz-box-shadow: 0 0 2px 1px rgba(13, 46, 77, 0.55) inset;
            box-shadow: 0 0 2px 1px rgba(13, 46, 77, 0.55) inset;
            font-size: 0.9em;
        }

        .dropdown_nm > a {
            z-index: 99;
            background: none !important;
        }

        .dropdown_nm,
        .flyout {
            position: relative;
        }
        /*.dropdown_nm:after {
  content: "\25BC";
  font-size: .5em;
  display: block;
  position: absolute;
  top: 38%;
  right: 12%;
}*/
        .drop-nav_nm,
        .flyout-nav_nm,
        .name_dd {
            position: absolute;
            display: none;
        }

        .drop-nav_nm {
            padding-top: 10px !important;
            color: #fff;
            z-index: 9999;
            background: #001827;
            top: 138px;
            min-width: 200px;
            left: -42px;
            -webkit-border-radius: 10px;
            border-radius: 10px;
            font-size: 0.9em;
        }

            .drop-nav_nm li {
                position: relative;
                float: left;
                height: 35px;
                width: 100% !important;
                white-space: nowrap;
            }

                .drop-nav_nm li:nth-child(1) > a,
                ul.flyout-nav_nm li.flyout:nth-child(1) a {
                    -webkit-border-radius: 10px 10px 0 0;
                    border-radius: 10px 10px 0 0;
                }

                .drop-nav_nm li a {
                    color: #fff !important;
                    display: block;
                    padding: 10px 30px 10px 60px;
                    text-decoration: none;
                    text-align: left;
                    height: 35px;
                    text-shadow: none;
                    font-weight: normal;
                }

                .drop-nav_nm li:last-child {
                    height: 50px;
                }


                .drop-nav_nm li a:hover {
                    background: #0099FF !important;
                    border: 0px !important;
                }

        .flyout-nav_nm li.hg50 a:hover {
            height: 50px !important;
        }

        .dropdown_nm:hover a span {
            position: absolute;
            z-index: 9;
        }

        .dropdown_nm:hover span.icon_nm {
            margin: 0 29.1%;
        }

        .dropdown_nm:hover span.txt_lb {
            margin: 39.1% 29.1%;
        }

        .dropdown_nm:hover a .name_dd,
        .dropdown_nm:hover > .drop-nav_nm,
        .flyout:hover > .flyout-nav_nm {
            display: block;
        }

        .flyout-nav_nm {
            left: 100%;
            top: 0;
        }

        /*.flyout:hover a,*/
        .flyout-nav_nm {
            background: #fff;
            text-align: center;
            -webkit-border-radius: 10px;
            border-radius: 10px;
            overflow: hidden;
        }

        .drop-nav_nm,
        .flyout-nav_nm {
            -webkit-box-shadow: 2px 2px 10px 0 #555;
            box-shadow: 2px 2px 10px 0 #555;
        }

            .flyout-nav_nm li:nth-child(even) {
                background-color: #E5E5E5;
            }

            .flyout-nav_nm li:nth-child(odd) {
                background-color: #fff;
            }

            .flyout-nav_nm li {
                float: left;
                min-width: 200px;
            }

                .flyout-nav_nm li a {
                    text-align: center;
                    color: #001827 !important;
                    padding: 10px 30px;
                }

                    .flyout-nav_nm li a:hover {
                        color: #fff !important;
                    }

        .flyout:hover {
            background: #0099FF;
        }

        /*icon_nmOS SUB MENU*/
        .ico19 {
            width: 19px;
            height: 26px;
        }

        .ico28 {
            width: 28px;
            height: 26px;
        }

        .ico30 {
            width: 30px;
            height: 26px;
        }

        .ico40 {
            width: 40px;
            height: 26px;
        }


        .mn_auto,
        .mn_vida,
        .mn_salud,
        .mn_pers,
        .mn_prop,
        .mn_trans,
        .mn_fianza,
        .mn_rcivil {
            position: absolute;
            top: 5px;
            left: 10px;
        }

        a b.mn_auto {
            background: url(../images/ico_PV_menu.png) no-repeat 0px 0px;
        }

        a:hover b.mn_auto,
        .flyout:hover b.mn_auto {
            background-position: 0px -26px;
        }

        a b.mn_vida {
            position: absolute;
            top: 5px;
            left: 10px;
            background: url(../images/ico_PV_menu.png) no-repeat -31px 0px;
        }

        a:hover b.mn_vida,
        .flyout:hover b.mn_vida {
            background-position: -31px -26px;
        }

        a b.mn_salud {
            position: absolute;
            top: 5px;
            left: 10px;
            background: url(../images/ico_PV_menu.png) no-repeat -61px 0px;
        }

        a:hover b.mn_salud,
        .flyout:hover b.mn_salud {
            background-position: -61px -26px;
        }

        a b.mn_pers {
            position: absolute;
            top: 5px;
            left: 10px;
            background: url(../images/ico_PV_menu.png) no-repeat -92px 0px;
        }

        a:hover b.mn_pers,
        .flyout:hover b.mn_pers {
            background-position: -91px -26px;
        }

        a b.mn_prop {
            position: absolute;
            top: 5px;
            left: 10px;
            background: url(../images/ico_PV_menu.png) no-repeat -123px 0px;
        }

        a:hover b.mn_prop,
        .flyout:hover b.mn_prop {
            background-position: -123px -26px;
        }

        a b.mn_trans {
            position: absolute;
            top: 5px;
            left: 10px;
            background: url(../images/ico_PV_menu.png) no-repeat -152px 0px;
        }

        a:hover b.mn_trans,
        .flyout:hover b.mn_trans {
            background-position: -152px -26px;
        }

        a b.mn_fianza {
            position: absolute;
            top: 5px;
            left: 10px;
            background: url(../images/ico_PV_menu.png) no-repeat -193px 0px;
        }

        a:hover b.mn_fianza,
        .flyout:hover b.mn_fianza {
            background-position: -193px -26px;
        }

        a b.mn_rcivil {
            position: absolute;
            top: 5px;
            left: 10px;
            background: url(../images/ico_PV_menu.png) no-repeat -213px 0px;
        }

        .grupos {
            float: left;
            width: 100%;
        }

        .grid_wrap {
            float: left;
            overflow-y: hidden;
            overflow-x: auto;
            width: 100%;
        }

        .fondo_blanco {
            float: left;
            width: 100%;
        }

        /* Para pantallas mas grandes de 800px */
        @media (min-width: 50em) {

            .nav_nm li {
                float: left;
                width: 7.1%;
                text-align: center;
            }
            /*.nav_nm li:nth-child(11) {width: 10%;}*/

            .nav_nm a {
                display: block;
                width: auto;
            }

            .nav_nm i {
                position: relative;
                display: inline-block;
                margin: 0 auto;
                padding: 0.4em;
            }

            .icon_nm + span {
                padding-top: 0.2em;
                line-height: 1.2em;
            }
        }

        @media (min-width:47.6em) and (max-width: 65em) {
            /* aqui se divide en 2 filas*/
            .nav_nm li {
                display: block;
                float: left;
                width: 15%;
            }

            .nav_nm a {
                padding: 0.625em 0;
                vertical-align: middle;
                height: 2.8125em;
                text-align: left;
                position: relative;
            }

            .nav_nm li span,
            .nav_nm li span.icon_nm {
                display: inline-block;
            }
                /*icon_nmos cuando se collapsa el menu*/
                .nav_nm li span.icon_nm {
                    width: 32px;
                    height: 32px;
                    position: absolute;
                    left: 2px;
                    top: 4px;
                }

            .icon_nm + span {
                font-size: 0.625em;
                margin-left: 40px;
                position: static;
                text-align: left;
                margin-right: 5px;
                margin-top: 3px;
            }

            .nav_nm li i {
                display: inline-block;
            }
        }

        @media (max-width: 47.5em) {

            /*MENU SE COLAPSA EN DROP DOWN*/
            .nav_nm .nav_nmtoogle {
                margin: 0;
                display: block;
            }

            /* Cuando JavaScript esta deshabilitado, escondemos el menu */
            .no-js .nav_nm ul {
                max-height: 35em;
                overflow: hidden;
            }

            /* Cuando JavaScript esta activado, escondemos el menu */
            .js .nav_nm ul {
                max-height: 0em;
                overflow: hidden;
            }

            /* Visualizacion del menu cuando el usuario ha hecho clic en el boton*/
            .js .nav_nm .active_nm + ul {
                max-height: 35em;
                overflow: hidden;
                -webkit-transition: max-height .4s;
                -moz-transition: max-height .4s;
                -o-transition: max-height .4s;
                -ms-transition: max-height .4s;
                transition: max-height .4s;
            }

            .nav_nm li span {
                display: inline-block;
                height: 100%;
            }

            .nav_nm li a {
                text-align: left;
            }

                .nav_nm li a > span {
                    margin-left: 36px;
                }

                    .nav_nm li a > span.icon_nm {
                        width: 30px;
                        height: 30px;
                        padding-top: 4px;
                        margin-left: 32px;
                    }

                        .nav_nm li a > span.icon_nm + span {
                            font-size: 0.6875em;
                            margin-left: 2px;
                        }
            /*degradado menu colapsa*/
            .nav_nm li a {
                height: 38px;
                padding: 0;
                line-height: 25px;
                background: #06263f;
                background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIxJSIgc3RvcC1jb2xvcj0iIzA2MjYzZiIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjEwMCUiIHN0b3AtY29sb3I9IiMwNjE1MjQiIHN0b3Atb3BhY2l0eT0iMSIvPgogIDwvbGluZWFyR3JhZGllbnQ+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjEiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
                background: -moz-linear-gradient(top, #06263f 1%, #061524 100%);
                background: -webkit-gradient(linear, left top, left bottom, color-stop(1%,#06263f), color-stop(100%,#061524));
                background: -webkit-linear-gradient(top, #06263f 1%,#061524 100%);
                background: -o-linear-gradient(top, #06263f 1%,#061524 100%);
                background: -ms-linear-gradient(top, #06263f 1%,#061524 100%);
                background: linear-gradient(to bottom, #06263f 1%,#061524 100%);
                filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#06263f', endColorstr='#061524',GradientType=0 );
                -webkit-box-shadow: 0 0 2px 1px rgba(13, 46, 77, 0.55) inset;
                -moz-box-shadow: 0 0 2px 1px rgba(13, 46, 77, 0.55) inset;
                box-shadow: 0 0 2px 1px rgba(13, 46, 77, 0.55) inset;
            }
        }

        @media (min-width: 1040px) and (max-width: 1280px) {
            /*nav_nm*/
            .icon_nm + span {
                font-size: 0.7em;
                margin-top: 0px;
            }

            .nav_nm li span.icon_nm {
                width: 60px;
                height: 60px;
            }

            .nav_nm a {
                height: 72px;
                padding: 0.6em 0.25em;
            }

            .st-pusher::after {
                content: none !important;
            }

            .logo {
                height: 100px;
            }

            #scroll_1 .accordion {
                border-right: 1px solid #555;
            }
        }

        @media screen and (max-width: 768px), (min-device-width: 768px) and (max-device-width: 1024px) and (orientation : portrait) {
            .nav_nm li {
                min-width: 190px;
            }
        }


        @media (max-width: 1175px) {
            .accordion_tabulado .col-1-2 {
                width: 100% !important;
            }
        }


        #divIllustration .accordion_tabulado {
            overflow: visible;
        }

        .coberageT {
            font-size: 1rem;
            padding: 10px;
        }

            .coberageT .round_blue {
                text-align: center;
            }

            .coberageT .round_blue {
                position: relative;
                top: -20px;
                left: -6px;
                width: 30%;
                min-width: 300px;
                padding: 6px 10px;
                z-index: 1;
            }

            .coberageT.data_Gpl .gradient_azul span {
                font-size: 1.1em;
                height: 30px;
            }

            .coberageT.data_Gpl .gradient_azul th.c2,
            .coberageT.data_Gpl .gradient_azul th.c3 {
                width: 200px;
            }

        #gvIllustration .dxpgCell_DevEx,
        #gvIllustration tr.dxgvDataRow_DevEx td.dxgv {
            text-overflow: ellipsis;
        }

        #gvIllustration .dxgvHSDC > div > table > tbody > tr > td:last-child,
        #gvIllustration .dxgvCSD > table > tbody tr > td:last-child {
            display: none;
        }

        #gvIllustration .dxgvHSDC > div > table > tbody > tr.dxgvFilterRow_DevEx > td:last-child {
            display: block;
            width: 100%;
        }

        #gvPOSCotizaciones .dxpgCell_DevEx,
        #gvPOSCotizaciones tr.dxgvDataRow_DevEx td.dxgv {
            text-overflow: ellipsis;
        }

        #gvPOSCotizaciones .dxgvHSDC > div > table > tbody > tr > td:last-child,
        #gvPOSCotizaciones .dxgvCSD > table > tbody tr > td:last-child {
            display: none;
        }

        #gvPOSCotizaciones .dxgvHSDC > div > table > tbody > tr.dxgvFilterRow_DevEx > td:last-child {
            display: block;
            width: 100%;
        }


        /*=================| Historial de Credito |===================*/
        .bold {
            font-weight: bold;
        }

        .flColor {
            background: #85a6e6;
        }

        .flColorGR {
            background: #ccc;
        }

        .no_border_td td {
            border: 0 !important;
        }

        .wrapp {
            max-width: 960px;
            width: 100%;
            margin: 20px auto 0px;
            overflow-x: auto;
            overflow-y: hidden;
        }

        section h2 {
            width: 100%;
            float: left;
            margin: 10px auto;
            padding: 5px;
            font-size: 0.9em;
            text-transform: uppercase;
            text-align: center;
            background: #85a6e6;
            color: #000;
            border: 1px #002060 solid;
            font-weight: bold;
        }

        strong.ced {
            width: 100%;
            font-size: 0.9em;
            float: left;
            display: block;
            text-transform: uppercase;
            margin-bottom: 5px;
        }

        .ttl_small span {
            font-size: 0.75em;
            font-weight: bold;
            display: block;
            width: 33.33%;
            float: left;
            padding: 2px 5px;
        }

        div.ttl_small > span:nth-child(1) {
            text-align: left;
        }

        div.ttl_small > span:nth-child(2) {
            text-align: center;
        }

        div.ttl_small > span:nth-child(3) {
            text-align: right;
        }

        .cont_tbl {
            margin-bottom: 10px;
            text-transform: uppercase;
            float: left;
            width: 100%;
            min-width: 720px;
        }

        .tbl_sr table {
            clear: both;
            border-collapse: separate;
            border-spacing: 2px;
        }

        .cont_tbl td {
            padding: 3px;
        }

        .tbl_sr th,
        .tbl_sr td {
            border: #888 1px solid;
        }

        .spcc {
            font-size: 0.7em;
            text-transform: uppercase;
            padding: 10px;
            font-weight: bold;
            float: left;
            width: 100%;
        }

        .top1 {
            width: 85%;
            float: left;
        }

        .top2 {
            width: 15%;
            float: left;
            text-align: center;
        }

            .top2 img {
                width: auto;
                height: 105px;
            }

        .lch {
            text-transform: uppercase;
            font-size: 0.9em;
        }

        .lect td {
            padding: 5px;
        }

        .lect .numb {
            border: #888 1px solid;
            height: 30px;
            width: 15px;
            padding: 0;
        }

        .ttl_small {
            float: left;
            width: 100%;
        }

            .ttl_small.lch {
                border-bottom: 1px solid #333;
                margin-bottom: 2px;
            }

        .dca_tbl .head {
            background: #85a6e6;
            border: #888 1px solid;
            font-weight: bold;
        }

        .dca_tbl .head_sm {
            background: #ccc;
            border: #888 1px solid !important;
            font-weight: bold;
            padding: 3px !important;
        }

            .dca_tbl .head_sm em {
                font-weight: normal;
                font-style: normal;
            }

        .dca_tbl .head {
            height: 100px;
            text-transform: none;
            font-size: 0.85em;
            line-height: 0.9em;
        }

            .dca_tbl .head th {
                padding: 5px;
            }

        .dca_tbl .body {
            font-size: 0.85em;
        }

        .dca_tbl table {
            border-spacing: 3px;
        }

            .dca_tbl table .body td {
                border: #888 1px solid;
                border-left: 0px;
                border-right: 0px;
                font-size: 0.85em;
                padding: 1px;
            }

            .dca_tbl table td table td {
                border-left: #888 1px solid !important;
                border-right: #888 1px solid !important;
                text-align: center;
            }

        .dca_tbl .body table {
            font-size: 0.8rem;
        }

        span.btn.boton_wrapper.azul.btn-file.marc {
            margin: 10px auto;
            cursor: pointer;
            display: block;
            font-size: 10pt;
        }

        .label_plus_input.direcc {
            height: 180px;
        }

            .label_plus_input.direcc div.dir {
                padding: 0 10px 0 13px;
            }

                .label_plus_input.direcc div.dir textarea {
                    height: 150px;
                }


        .inspection_radio > div {
            /*width: 50%;*/
            float: left;
            padding: 0 10px;
        }

            .inspection_radio > div input[type="radio"] {
                width: 34px !important;
                height: 22px;
                cursor: pointer;
            }

            .inspection_radio > div label {
                float: left;
                height: 30px;
                padding-top: 4px;
                cursor: pointer;
            }

                .inspection_radio > div label:after {
                    left: 5px !important;
                    top: 2px !important;
                }

                .inspection_radio > div label:before {
                    width: 25px !important;
                    height: 25px !important;
                }
        /*
.inspection_radio > div {
    width: 50%;
    float: left;
    padding: 0 10px;
}

    .inspection_radio > div input[type="radio"] {
        width: 22px !important;
        height: 22px;
        cursor: pointer;
    }
*/



        /*.inspection_radio > div input[type="radio"] { width: 14px !important; height: 29px; cursor: pointer; }*/


        .inspection_radio_table > table {
            width: 50%;
            float: left;
            padding: 0 10px;
        }

            .inspection_radio_table > table input[type="radio"] {
                width: 34px !important;
                height: 22px;
                cursor: pointer;
            }

            .inspection_radio_table > table label {
                float: left;
                height: 30px;
                padding-top: 4px;
                cursor: pointer;
            }

                .inspection_radio_table > table label:after {
                    left: 5px !important;
                    top: 2px !important;
                }

                .inspection_radio_table > table label:before {
                    width: 25px !important;
                    height: 25px !important;
                }

        .cbrage #bodyContent_UCFacultyPosition_gvCoverages_DXHeadersRow0 table {
            font-size: 1rem;
        }

        .cbrage table table tr.dxgvGroupRow_DevEx td:first-child img {
            display: none;
        }

        .cbrage table table tr.dxgvGroupRow_DevEx td {
            border: 1px solid #031324;
            border-left: 0;
        }

        .cbrage table table tr.dxgvGroupRow_DevEx {
            font-weight: 600;
            background: #002060;
            font-size: 0.9rem;
            height: 40px;
            color: #fff;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            -ms-border-radius: 5px;
            -o-border-radius: 5px;
            border-radius: 5px;
        }

        table#bodyContent_UCFacultyPosition_gvVehiculos_DXMainTable input[type="submit"] {
            background-color: #FF9800;
            border-color: #af6c0a;
            color: #fff;
            -moz-box-shadow: -3px -5px 3px rgba(0,0,0,.25) inset, 2px 5px 5px rgba(255,255,255,.5) inset;
            box-shadow: -3px -5px 3px rgba(0,0,0,.25) inset, 2px 5px 5px rgba(255,255,255,.5) inset;
            -ms-box-shadow: -3px -5px 3px rgba(0,0,0,.25) inset, 2px 5px 5px rgba(255,255,255,.5) inset;
            -o-box-shadow: -3px -5px 3px rgba(0,0,0,.25) inset, 2px 5px 5px rgba(255,255,255,.5) inset;
            -webkit-box-shadow: -3px -5px 3px rgba(0,0,0,.25) inset, 2px 5px 5px rgba(255,255,255,.5) inset;
        }

        table#gvCoverages_DXHeaderTable td table td {
            font-size: 1em;
        }

        #gvCoverages_DXMainTable tr.dxgvDataRow_DevEx td.dxgv:nth-child(2) {
            font-size: 1.2em;
            font-weight: 600;
            text-overflow: ellipsis;
        }

        table#gvCoverages_DXHeaderTable tr:nth-child(1) td:nth-child(2),
        #gvCoverages_DXMainTable tr:nth-child(1) td:nth-child(2) {
            width: 30% !important;
        }


        .box_SP .boxCont .labelbox_50_fluid .label_plus_input {
            width: 50%;
            float: left;
            padding: 5px;
        }

        .select_buy1 .box_SP .boxCont {
            height: auto !important;
            width: 99%;
        }

        .select_buy1 .box_SP .boxCont {
            min-height: 157px !important;
        }

        .box_SP .ttl {
            background: #002060;
        }

        .box_SP .ttl {
            min-width: 235px;
            height: 150px;
            -webkit-border-radius: 20px;
            border-radius: 20px;
            margin-bottom: -115px;
            float: left;
            color: #fff;
            font-weight: bold;
            font-size: 1em;
            padding: 10px 20px 0px 30px;
            background: #053e66;
            line-height: 13px;
        }

        .box_SP .boxCont {
            min-width: 320px;
            min-height: 360px;
            width: 99%;
            padding: 10px;
            background-color: #fff;
            -webkit-border-radius: 20px;
            border-radius: 20px;
            float: left;
            border: 1px solid #002060;
            margin-left: 12px;
        }

            .box_SP .boxCont .ckLabel {
                margin: 0px;
                width: 100px;
            }

            .box_SP .boxCont .label_plus_input select {
                width: 55%;
                position: absolute;
                top: 5px;
                right: 0px;
            }

            .box_SP .boxCont .label_plus_input {
                height: 35px;
            }

                .box_SP .boxCont .label_plus_input input,
                .box_SP .boxCont .label_plus_input span {
                    width: 55%;
                    text-align: left;
                    height: 30px;
                }

            .box_SP .boxCont th span {
                font-weight: 600;
            }

            .box_SP .boxCont .conduc_aseg td,
            .box_SP .boxCont .solici_aseg td, {
                font-size: 0.8em;
            }

            .box_SP .boxCont .conduc_aseg th.c5,
            .box_SP .boxCont .conduc_aseg th.c6 {
                width: 30px;
            }

            .box_SP .boxCont .conduc_aseg th span {
                line-height: 40px;
            }

            .box_SP .boxCont .conduc_aseg th.c1 span,
            .box_SP .boxCont .conduc_aseg th.c2 span,
            .box_SP .boxCont .conduc_aseg th.c4 span {
                line-height: 40px;
            }


        .p-info {
            color: #f9f9f9;
            background-color: #217677;
            padding: 8px 8px 8px 21.33333px;
            position: relative;
            border-radius: 3px;
        }

            .p-info:before {
                content: "\f129";
                background-color: #175354;
                font-style: normal;
                font-weight: 400;
                speak: none;
                display: block;
                position: absolute;
                top: 8px;
                left: 0;
                margin-left: -16px;
                width: 32px;
                height: 32px;
                border-radius: 16px;
                text-decoration: inherit;
                text-align: center;
                font-variant: normal;
                text-transform: none;
                line-height: 32px;
                -webkit-font-smoothing: antialiased;
                -moz-osx-font-smoothing: grayscale;
            }
    </style>
    <link href="~/Content/Master.css" rel="stylesheet" />
    <link href="../../Scripts/JQuery/JQueryUI/jquery-ui-1.9.0.custom.min.css" rel="stylesheet" />
    <script src="../../Scripts/JQuery/Core/jquery-1.9.1.js"></script>
    <script src="../../Scripts/JQuery/JQueryUI/jquery-ui-1.10.3.js"></script>
    <script src="../../Scripts/Utilities/JSTools.js"></script>
</head>
<body>
    <form id="frmTransunion" runat="server" >
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" ScriptMode="Release"></asp:ScriptManager>
        <asp:ImageButton ID="ImageButton1" title="Imprimir" ImageUrl="~/Content/images/print_icon.png" runat="server" OnClientClick="javascript:window.print();" />
        <uc1:WUCTransunion runat="server" ID="WUCTransunion" />
        <asp:HiddenField runat="server" ID="containerMessage" Value="body" ClientIDMode="Static" />
    </form>
</body>
</html>