(function ($) {
    // Navigation scrolls
    $(".navbar-nav li a").on('click', function (event) {
        $('.navbar-nav li').removeClass('active');
        $(this).closest('li').addClass('active');
        var $anchor = $(this);
        var nav = $($anchor.attr('href'));
        if (nav.length) {
            $('html, body').stop().animate({
                scrollTop: $($anchor.attr('href')).offset().top
            }, 1500, 'easeInOutExpo');

            event.preventDefault();
        }
    });
    $(".navbar-collapse a").on('click', function () {
        $(".navbar-collapse.collapse").removeClass('in');
    });

    // Add smooth scrolling to all links in navbar
    $("a.mouse-hover, a.get-quote").on('click', function (event) {
        var hash = this.hash;
        if (hash) {
            event.preventDefault();
            $('html, body').animate({
                scrollTop: $(hash).offset().top
            }, 1500, 'easeInOutExpo');
        }
    });
})(jQuery);
(function countChar(val) {
    var len = val.value.length;
    if (len >= 200) {
        val.value = val.value.substring(0, 200);
    } else {
        $('#charNum').text(200 - len);
    }
})();
$(function () {
    $('.fadein div:gt(0)').hide();
    setInterval(function () {
        $('.fadein :first-child').fadeOut().next('div').fadeIn().end().appendTo('.fadein');
    }, 5000);
})(jQuery);
