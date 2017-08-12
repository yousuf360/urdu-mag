var content_height = 1140;	// the height of the content, discluding the header/footer
var page = 1;
var pageForIndex = 0;
var pageNumInUrduGlobal = "۱";// the beginning page number to show in the footer
function startColumnizing(rating) {

    $('.tempTableOfContent').each(function (index, element) {
        var columns = 1
        buildNewsletterWithOutPageNumber(element, columns,"index");
    });
    $('.article').each(function (index, element) {
        console.log('#indexPage-' + (index + 1));
        $('#indexPage-' + (index + 1)).html(pageNumInUrduGlobal)

        var countForTableOfContent = page + 4;//kyun k actual mae tou 4 pages choray ek cover ek blank and ek table of content phir ek blank
        $('#table-of-content-element-' + (index + 1)).prop("href", "#article-" + (index+1));



        var columns = parseInt($(element).attr('columns'));
        buildNewsletter(index + 1, element, columns);
        if (rating) {
            var columns = 1
            buildNewsletterWithOutPageNumber($('#rating-div-' + index), columns, 'rating-' + index);
            
        }
     
    }
    );
}

function buildNewsletterWithOutPageNumber(item, vColumns,elementId) {
    if ($(item).contents().length > 0) {
        // when we need to add a new page, use a jq object for a template
        // or use a long HTML string, whatever your preference
        $page = $("#page_template").clone().addClass("page").css("display", "block").attr('id', elementId);
        $page.attr('data-article-num', -1);
        $page.attr('data-pg-num', pageForIndex);
        pageForIndex--;
        // fun stuff, like adding page numbers to the footer

        //to convert page number in urdu format

        $("#main").append($page);
        



        // here is the columnizer magic
        $(item).columnize({
            buildOnce: true,
            columns: vColumns,
            target: ".page:last .content",
            columnFloat: "right",
            overflow: {
                height: content_height,
                id: '#' + $(item).attr('id'),
                doneFunc: function () {
                    console.log("done with page");
                    buildNewsletterWithOutPageNumber(item, vColumns,elementId);
                }
            }
        });
    }
}
function buildNewsletter(articleNum,item, vColumns) {
    if ($(item).contents().length > 0) {
        // when we need to add a new page, use a jq object for a template
        // or use a long HTML string, whatever your preference
        $page = $("#page_template").clone().addClass("page").css("display", "block").attr('id', 'article-' + articleNum);
        $page.attr('data-article-num', articleNum);
        $page.attr('data-pg-num', page);
        //.addClass('zoomTarget').attr('data-targetsize',"0.6").attr('data-closeclick',"true")
        // fun stuff, like adding page numbers to the footer

        //to convert page number in urdu format
        var pageInStr = '' + page;
        var pageNumInUrdu = "";
        for (var i = 0; i < pageInStr.length; i++) {

            var a = typeUrdu(pageInStr.charCodeAt(i), String.fromCharCode(pageInStr.charCodeAt(i)));
            console.log(a.newKey);
            pageNumInUrdu += a.newKey;

        }
        pageInStr = "" + (page + 1);

        //Index Page Number
        pageNumInUrduGlobal = "";
        for (var i = 0; i < pageInStr.length; i++) {

            var a = typeUrdu(pageInStr.charCodeAt(i), String.fromCharCode(pageInStr.charCodeAt(i)));
            console.log(a.newKey);
            pageNumInUrduGlobal += a.newKey;

        }
        //Index Page Number


        $page.find("#pgNum").append(pageNumInUrdu);
        $("#main").append($page);


        page++;

        // here is the columnizer magic
        $(item).columnize({
            buildOnce: true,
            columns: vColumns,
            target: ".page:last .content",
            columnFloat: "right",
            overflow: {
                height: content_height,
                id: '#' + $(item).attr('id'),
                doneFunc: function () {
                    console.log("done with page");
                    buildNewsletter(articleNum,item, vColumns);
                }
            }
        });
    }
}