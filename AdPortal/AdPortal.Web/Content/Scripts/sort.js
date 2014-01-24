/* Updates anchors in table headers to update the querystring with new sort 
*  and direction parameters on click.
*
* @Requires queryString.js { local } 
* @Requires jQuery
*/
$(document).ready(function () {
    var toggleSorting = function (params, newSortBy) {
        if (params.sortBy === newSortBy && params.ascending === true) {
            params.ascending = false;
        } else {
            params.sortBy = newSortBy;
            params.ascending = true;
        }

        return params;
    };

    var updateSortingGlyphs = function (params, $thisTh) {
        var $glyph = $thisTh.find('span');

        $('th span').removeClass();

        $glyph.addClass('glyphicon');
        if (params.ascending === true) {
            $glyph.addClass('glyphicon-arrow-up');
        } else {
            $glyph.addClass('glyphicon-arrow-down');
        }
    };

    try {
        var params = window.QueryString.getQueryStringParams();

        if (params.sortBy) {
            updateSortingGlyphs(params, $('a[data-param="' + params.sortBy + '"]').parent());
        }

        $('th a').click(function (event) {
            try {
                var paramName = $(this).data('param');

                params = toggleSorting(params, paramName);

                event.preventDefault();
                window.QueryString.updateQueryString(params);

            } catch (ex) {
                console.error(ex);
            }
        });
    } catch (ex) {
        console.error(ex);
    }
});