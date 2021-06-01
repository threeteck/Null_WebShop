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
    $('#upload-alert').attr('hidden', 'hidden');
    $('#upload-alert-error').attr('hidden', 'hidden');
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
        },
        error: function (data){
            $('#upload-alert-error').removeAttr('hidden');
        }
    });
};


(function ($) {
    $.validator.unobtrusive.parseDynamicContent = function (selector) {
        //use the normal unobstrusive.parse method
        $.validator.unobtrusive.parse(selector);

        //get the relevant form
        var form = $(selector).first().closest('form');

        //get the collections of unobstrusive validators, and jquery validators
        //and compare the two
        var unobtrusiveValidation = form.data('unobtrusiveValidation');
        var validator = form.validate();

        $.each(unobtrusiveValidation.options.rules, function (elname, elrules) {
            if (validator.settings.rules[elname] == undefined) {
                var args = {};
                $.extend(args, elrules);
                args.messages = unobtrusiveValidation.options.messages[elname];
                //edit:use quoted strings for the name selector
                $("[name='" + elname + "']").rules("add", args);
            } else {
                $.each(elrules, function (rulename, data) {
                    if (validator.settings.rules[elname][rulename] == undefined) {
                        var args = {};
                        args[rulename] = data;
                        args.messages = unobtrusiveValidation.options.messages[elname][rulename];
                        //edit:use quoted strings for the name selector
                        $("[name='" + elname + "']").rules("add", args);
                    }
                });
            }
        });
    }
})($);

function htmlToElement(html) {
    var template = document.createElement('template');
    html = html.trim();
    template.innerHTML = html;
    return template.content.firstChild;
}

function getPaginationBarElement(page, totalPages, onPageJumpHandler){
    let html = ``;
    if(totalPages > 1) {
        html = `
        <div class="pages">
            <nav class="d-flex justify-content-center">
                <ul class="pagination">
                    <li class="page-item ${(page === 0 ? "disabled" : "")}">
                        <button class="page-link page-link-previous" ${(page === 0 ? "disabled" : "")} type="button">
                            <span aria-hidden="true">&laquo;</span>
                            <span class="sr-only">Previous</span>
                        </button>
                    </li>
        `

        if (page >= 4) {
            html += `
        <li>
            <button class="page-link" type="button" page-val="0">1</button>
        </li>
        <li class="page-item disabled"><span>...</span></li>
        `
        }

        for (let i = Math.max(0, page - 3); i < Math.min(totalPages, page + 4); i++) {
            html += `
        <li class="page-item ${(page === i ? "active" : "")}">
            <button class="page-link" type="button" page-val="${i}">${i + 1}</button>
        </li>
        `
        }

        if (page + 3 < totalPages - 1) {
            html += `
        <li class="page-item disabled"><span>...</span></li>
        <li class="page-item">
            <button class="page-link" type="button" page-val="${totalPages - 1}">${totalPages}</button>
        </li>
        `
        }

        html += `
                <li class="page-item ${(page === totalPages - 1 ? "disabled" : "")}">
                    <button class="page-link page-link-next" ${(page === totalPages - 1 ? "disabled" : "")} type="button">
                        <span aria-hidden="true">&raquo;</span>
                        <span class="sr-only">Next</span>
                    </button>
                </li>
            </ul>
        </nav>
    </div>
    `
    }
    
    let result = $(htmlToElement(html));
    
    result.find('.page-link[page-val]').each(function (){
        $this = $(this);
        let pageVal = Number($this.attr('page-val'));
        $this.click(async () => {
            await onPageJumpHandler(pageVal);
        });
    });
    
    result.find('.page-link-previous').click(async ()=>{
        await onPageJumpHandler(page - 1);
    });

    result.find('.page-link-next').click(async ()=>{
        await onPageJumpHandler(page + 1);
    });
    
    return result;
}
