/* A helper object for accessing and updating the location bar.
*
* @Requires jQuery_deparam { local }
* @Requires jQuery
*/
window.QueryString = (function () {
    var uri = decodeURIComponent(window.location.href).split("?");

    return {
        getQueryStringParams: function () {
            var queryString = uri[1];
            var params;

            if (!queryString || !(queryString.trim())) {
                params = {};
            } else {
                params = $.deparam(queryString, true);
            }

            return params;
        },
        updateQueryString: function (newParams) {
            window.location.href = uri[0] + '?' + $.param(newParams);
        }
    };
})()