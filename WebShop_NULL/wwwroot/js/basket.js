console.log($('#quantitySetter').data('userid'))
let hostUrl = window.location.protocol + '//' + window.location.host + '/';


function checkValid(quantity) {

    if (quantity < 1) {
        $('#quantitySetter').val(1)
        return false
    }
    return true
}

function setQuantity() {
    let productId = $('#quantitySetter').data('productid');
    let userId = $('#quantitySetter').data('userid');
    let quantity = $('#quantitySetter').val();
    console.log(productId)
    console.log(userId)
    console.log(quantity)
    if (checkValid(quantity)) {
        $.ajax({
            url: hostUrl + "Basket/SetQuantity/?userId=" + userId + "&productId=" + productId + "&quantity=" + quantity,
            method: "GET",
            success: function (data) {
                console.log("Значение изменено")
            }
        })
    }
}