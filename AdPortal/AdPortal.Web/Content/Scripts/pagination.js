/* Updates anchors in table headers to update the querystring with new sort 
*  and direction parameters on click.
* 
* @Requires QueryString.js
* @Requires jQuery
*/
window.Pagination = (function () {
    var getTemplate = function (pageNumber) {
        var $template = $('<a class="page-numbers">' + pageNumber.toString() + '</a>');

        $template.click(function (event) {
            try {
                var params = window.QueryString.getQueryStringParams();
                params.page = pageNumber;

                event.preventDefault();
                window.QueryString.updateQueryString(params);

            } catch (ex) {
                console.error(ex);
            }
        });

        return $template;
    };

    return {
        getPaginationMenu: function (pageCount, currentPage) {
            var max_earlier_pages_displayed = 5;
            var max_after_pages_displayed = 5;
            var getMenuObj = function (pageNumber) {
                return { pageNum: pageNumber, template: getTemplate(pageNumber) };
            };

            var menu = [];

            var lastPage = pageCount;
            var firstPage = 0;
            if (lastPage < 0) { lastPage = 0; }

            var startPage = currentPage - max_earlier_pages_displayed;
            if (startPage <= firstPage) {
                startPage = firstPage;
            }
            else {
                menu.push(getMenuObj(firstPage + 1));
            }

            var appendLast = false;
            var endPage = currentPage + max_after_pages_displayed;
            if (endPage >= lastPage) {
                endPage = lastPage;
            }
            else {
                appendLast = true;
            }

            for (var i = startPage; i < endPage; i++) {
                menu.push(getMenuObj(i + 1));
            }

            if (appendLast) {
                menu.push(getMenuObj(lastPage));
            }

            return menu;
        }
    };

    
})()

$(document).ready(function () {
    try {
        var $pagination = $('#pagination');
        var pageCount = $("#totalPageCount").data("totalpagecount") - 1;
        var currentPage = $("#page").data("page");

        var menu = window.Pagination.getPaginationMenu(pageCount, currentPage);
        while (menu.length) {
            $pagination.prepend(menu.pop().template);
        }
    } catch (ex) {
        console.error(ex);
    }
});