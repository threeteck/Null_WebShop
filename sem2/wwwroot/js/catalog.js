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
