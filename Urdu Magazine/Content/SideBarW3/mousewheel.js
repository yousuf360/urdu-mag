// Cache selectors
function enableScrollView(navBarId) {
    var lastId,
    topMenu = $(navBarId),
   
    // All list items
    menuItems = topMenu.find("a"),
    // Anchors corresponding to menu items
    scrollItems = menuItems.map(function () {
        var item = $($(this).attr("href"));
        if (item.length) { return item; }
    });

    // Bind click handler to menu items
    // so we can get a fancy scroll animation
    menuItems.click(function (e) {
        if ($('#main').css('zoom') == 1) {
            var href = $(this).attr("href"),
                offsetTop = href === "#" ? 0 : $(href).offset().top;
            console.log(offsetTop);

            $('html, body').stop().animate({
                scrollTop: offsetTop
            }, 300);
            e.preventDefault();
        }
    });

    // Bind to scroll
    $(window).scroll(function () {
        // Get container scroll position
        var fromTop = $(this).scrollTop()+5 ;

        // Get id of current scroll item
        var cur = scrollItems.map(function () {
            if ($(this).offset().top < fromTop)
                return this;
        });
        // Get the id of the current element
        cur = cur[cur.length - 1];
        var id = cur && cur.length ? cur[0].id : "";

        if (lastId !== id) {
            lastId = id;
            // Set/remove active class
          
          menuItems.siblings().removeClass("active");
            menuItems
              .parent()
              .end().filter("[href='#" + id + "']").addClass("active");
        }
    });
}