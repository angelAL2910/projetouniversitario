// JavaScript Document

/* 
stl Responsive Menu Plugin
Version: 1.0
Author: Samson.Onna 
Email : samson3d@gmail.com
----------------------------------------*/

(function ($) {
    $.fn.stlResponsiveMenu = function (options) {

        //plugin's default options
        var defaults = {
            resizeWidth: '768',
            animationSpeed: 'fast',
            accoridonExpAll: false
        };

        //Variables
        var options = $.extend(defaults, options),
            opt = options,
            $resizeWidth = opt.resizeWidth,
            $animationSpeed = opt.animationSpeed,
            $expandAll = opt.accoridonExpAll,
            $stlMenu = $(this),
            $menuStyle = $(this).attr('data-menu-style');

        // Initilizing        
        $stlMenu.find('ul').addClass("sub-menu");
        //$stlMenu.find('ul').siblings('a').append('<span class="arrow "></span>');
        if ($menuStyle == 'accordion') { $(this).addClass('collapse'); }

        // Window resize on menu breakpoint 
        if ($(window).innerWidth() <= $resizeWidth) {
            menuCollapse();
        }
        $(window).resize(function () {
            menuCollapse();
        });

        // Menu Toggle
        function menuCollapse() {
            var w = $(window).innerWidth();
            if (w <= $resizeWidth) {
                $stlMenu.find('li.menu-active').removeClass('menu-active');
                $stlMenu.find('ul.slide').removeClass('slide').removeAttr('style');
                $stlMenu.addClass('collapse hide-menu');
                $stlMenu.attr('data-menu-style', '');
                $('.menu-toggle').show();
            } else {
                $stlMenu.attr('data-menu-style', $menuStyle);
                $stlMenu.removeClass('collapse hide-menu').removeAttr('style');
                $('.menu-toggle').hide();
                if ($stlMenu.attr('data-menu-style') == 'accordion') {
                    $stlMenu.addClass('collapse');
                    return;
                }
                $stlMenu.find('li.menu-active').removeClass('menu-active');
                $stlMenu.find('ul.slide').removeClass('slide').removeAttr('style');
            }
        }

        //ToggleBtn Click
        $('#menu-btn').click(function () {
            $stlMenu.slideToggle().toggleClass('hide-menu');
        });


        // Main function 
        return this.each(function () {
            // Function for Horizontal menu on mouseenter
            $stlMenu.on('mouseover', '> li a', function () {
                if ($stlMenu.hasClass('collapse') === true) {
                    return false;
                }
                $(this).off('click', '> li a');
                $(this).parent('li').siblings().children('.sub-menu').stop(true, true).slideUp($animationSpeed).removeClass('slide').removeAttr('style').stop();
                $(this).parent().addClass('menu-active').children('.sub-menu').slideDown($animationSpeed).addClass('slide');
                return;
            });
            $stlMenu.on('mouseleave', 'li', function () {
                if ($stlMenu.hasClass('collapse') === true) {
                    return false;
                }
                $(this).off('click', '> li a');
                $(this).removeClass('menu-active');
                $(this).children('ul.sub-menu').stop(true, true).slideUp($animationSpeed).removeClass('slide').removeAttr('style');
                return;
            });
            //End of Horizontal menu function

            // Function for Vertical/Responsive Menu on mouse click
            $stlMenu.on('click', '> li a', function () {
                if ($stlMenu.hasClass('collapse') === false) {
                    //return false;
                }
                $(this).off('mouseover', '> li a');
                if ($(this).parent().hasClass('menu-active')) {
                    $(this).parent().children('.sub-menu').slideUp().removeClass('slide');
                    $(this).parent().removeClass('menu-active');
                } else {
                    if ($expandAll == true) {
                        $(this).parent().addClass('menu-active').children('.sub-menu').slideDown($animationSpeed).addClass('slide');
                        return;
                    }
                    $(this).parent().siblings().removeClass('menu-active');
                    $(this).parent('li').siblings().children('.sub-menu').slideUp().removeClass('slide');
                    $(this).parent().addClass('menu-active').children('.sub-menu').slideDown($animationSpeed).addClass('slide');
                }
            });
            //End of responsive menu function

        });
        //End of Main function
    }
})(jQuery);
