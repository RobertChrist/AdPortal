$(document).ready(function () {
    try {
        $('a[href="' + this.location.pathname + '"]').parent().addClass('active');
    } catch(ex) {
        // No need to do anything, as we only update the nav when we have a match between link and pathname.
    }
});