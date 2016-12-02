
$(function () {
    

    var handleShopCartItemDelete = function (e) {
        var $tr = $(e.currentTarget).closest("tr");
        var itemId = $tr.children('input').val();

        $.ajax({
            type: "DELETE",
            url: "ShoppingCart/Delete",
            dataType: "json",
            data: { itemId: itemId },
            success: function (response) {
                if (response.success) {
                    console.log("Success!!!");
                    $tr.remove();
                    handleAmountChange();
                }
            },
            error: function (xhr, err) {
                alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                alert("responseText: " + xhr.responseText);
                location.reload(true);
            }
        });
    }

    function hasAmount() {
        return $('.order-amount').toArray().some(function (input) { return Number(input.value) > 0 });
    }

    function calculateTotalSum() {
        var total = 0;
        $('.order-amount').toArray().forEach(function (input) {
            var amount = Number(input.value);
            if (amount) {
                total += parseFloat($(input).closest('tr').children('.item-price').text()) * amount;
            }
        })
        return total;
    }

    var handleAmountChange = function () {
        $('#shoppingCartPlaceOrder').prop('disabled', !hasAmount());
        $('#shoppingCartTotalSum').text(calculateTotalSum);
    }

    $('#shoppingCartPlaceOrder').prop('disabled', true);

    $('.shopping-cart-delete').on('click', handleShopCartItemDelete);
    $('.order-amount').change(handleAmountChange)
});