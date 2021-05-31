
$(document).ready(function () {
    $("#Address").suggestions({
        token: "c5b043354a04009bbeeca1da66740ca6f5d458b6",
        type: "ADDRESS",
        /* Вызывается, когда пользователь выбирает одну из подсказок */
        onSelect: function (suggestion) {
            console.log(suggestion);
        }
    });
});
