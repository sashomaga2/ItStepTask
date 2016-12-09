$(function () {
    var $submitBtn = $('#orderStatusEdit');

    function hasChange() {
        return $('.order-status').toArray().some(function (input) {
            //console.log("order-status", input.value);
            //console.log("order-last-selected", $(input).closest('tr').children('.order-last-selected').val());
            console.log("has change:", input.value !== $(input).closest('tr').children('.order-last-selected').val());
            return input.value !== $(input).closest('tr').children('.order-last-selected').val();
        });
    }

    var handleOrderStatusChange = function (e) {
        $('#orderStatusEdit').prop('disabled', !hasChange());
        return false;
    }

    $submitBtn.prop('disabled', true);
    $(document).on('change', '.order-status', handleOrderStatusChange);
});