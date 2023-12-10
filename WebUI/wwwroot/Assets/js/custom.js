(function ($) {
    const sectionClasses = $('main').attr('class').split(/\s+/);
    $(".main-menu li a").each(function () {
        var $this = $(this);
        for (var i = 0; i < sectionClasses.length; i++) {
            if ($this.hasClass(sectionClasses[i])) {
                $this.addClass('active');
            }
        }
    })
})(jQuery);