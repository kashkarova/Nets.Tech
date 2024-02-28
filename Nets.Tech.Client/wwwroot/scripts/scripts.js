$('form input').on('change', function () {
    $(this).closest('form').submit();
});