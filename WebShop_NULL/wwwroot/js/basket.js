console.log($('#quantitySetter').data('userid'))



function checkValid(quantity, productId) {

    if (quantity < 1) {
        $('#quantitySetter_' + productId).val(1)
        return false
    }
    return true
}

function setQuantity(userId, productId) {
    let quantity = $('#quantitySetter_' + productId).val();
    let hostUrl = window.location.protocol + '//' + window.location.host + '/';
    console.log(productId)
    console.log(userId)
    console.log(quantity)
    if (checkValid(quantity, productId)) {
        $.ajax({
            url: hostUrl + "Basket/SetQuantity/?userId=" + userId + "&productId=" + productId + "&quantity=" + quantity,
            method: "GET",
            success: function (data) {
                console.log("Значение изменено")
                getBasketMenu()
            }
        })
    }
}
function getBasketMenu() {
    let userId = $('#basket-form').data('userid');
        $.ajax({
            url: "Basket/GetBasketMenuPartial/?userId=" + userId,
            method: "GET",
            success: function (data) {
                $('#order-summary').replaceWith(data);
            }
        })
}