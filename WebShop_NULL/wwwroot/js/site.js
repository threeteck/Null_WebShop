// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

$(()=>{
    window.reader = new FileReader();
    window.imageEl = $('#upload-image')[0]

    reader.onload = e => {
        imageEl.setAttribute('src', e.target.result);
    };
})

var dropZone = $('#upload-container');
dropZone.on('drag dragstart dragend dragover dragenter dragleave drop', function () {
    return false;
});
dropZone.on('dragover dragenter', function () {
    dropZone.addClass('dragover');
});

dropZone.on('dragleave', function (e) {
    dropZone.removeClass('dragover');
});

dropZone.on('dragleave', function (e) {
    let dx = e.pageX - dropZone.offset().left;
    let dy = e.pageY - dropZone.offset().top;
    if ((dx < 0) || (dx > dropZone.width()) || (dy < 0) || (dy > dropZone.height())) {
        dropZone.removeClass('dragover');
    };
});

dropZone.on('drop', function (e) {
    dropZone.removeClass('dragover');
    let files = e.originalEvent.dataTransfer.files;
    sendFiles(files);
});

$('#file-input').change(function () {
    let files = this.files;
    sendFiles(files);
});


function sendFiles(files) {
    let maxFileSize = 5242880;
    let Data = new FormData();
    $(files).each(function (index, file) {
        if ((file.size <= maxFileSize) && ((file.type == 'image/png') || (file.type == 'image/jpeg'))) {
            Data.append('image', file);
        }
    });
    $.ajax({
        url: dropZone.attr('action'),
        type: dropZone.attr('method'),
        data: Data,
        contentType: false,
        processData: false,
        success: function (data) {
            $('#upload-alert').removeAttr('hidden');
            imageEl.setAttribute('style', 'display: block;\n' +
                '    width: 300px;\n' +
                '    height: 300px;\n' +
                '    object-fit: contain;');
            reader.readAsDataURL(files[0]);
        }
    });
};
