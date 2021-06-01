let hostUrl = window.location.protocol +'//'+ window.location.host + '/';

window.onload = loadProducts();
function loadProducts(category) {
    let menu = $('#products')
    if (category == undefined) {
        currentUrl = hostUrl + 'Catalog/GetProducts'
    }
    else {
        currentUrl = hostUrl + 'Catalog/GetProducts?category=' + category
    }
    menu.empty()
    console.log(currentUrl)
    $.ajax({
        url: currentUrl,
        type: 'get',
        success: function (data) {
            $('#products').append(data);
        },
        error: function (data) {
            console.log('Ошибка\n',data)
        }
    })
}
function addToBasketBtnClick() {
    let userId = $('#addToBasketBtn').data('userid');
    let productId = $('#addToBasketBtn').data('productid');
    console.log(userId);
    reqUrl = hostUrl + 'Catalog/GetAddToBasketBtn/?userId=' + userId + '&productId=' + productId;
    $.ajax({
        url: reqUrl,
        method: 'GET',
        success: function (data) {
            $('#addToBasketBtnContainer').empty().append(data);
        }
    })
}
function setQuantity() {
    let productId = $('#quantitySetter').data('productId');
    let userId = $('#quantitySetter').data('userId');
    let quantity = $('#quantitySetter').val();
    if (checkValid(quantity)) {
        $.ajax({
            url=hostUrl + "Basket/SetQuantity/?userId=" + userId + "&productId=" + productId + "&quantity=" + quantity,
            method: "GET",
            success: function (data) {
                console.log(data)
            }
        })
    }
}
function checkValid(quantity) {

    if (quantity < 1) {
        $('#quantitySetter').val(1)
        return false
    }
    return true
}