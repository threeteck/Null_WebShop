$(function(){
    window.index_iter = 0;
    $propertyContainer = $('#property-container');
    $createPropertyButton = $('#createProperty');
    window.$submitButton = $('#submit-button');
    window.propertyCount = 0;
    window.$form = $('#create-category-form');
    window.optionEntries = {}
    
    $createPropertyButton.click(function (e){
        e.stopPropagation();
        $propertyContainer.append(getPropertyElement());
        window.index_iter += 1;
        window.$submitButton.removeAttr('disabled');
        window.propertyCount += 1;
        revalidateForm();
    });
});

function getPropertyElement(){
    let index_closure = window.index_iter;
    let html = `
    <div id="property-${index_closure}" class="box border-red w-75">
        <div class="cross">
            <div class="m-0">
                <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" fill="currentColor" class="bi bi-x" viewBox="0 0 16 16">
                    <path d="M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708z"/>
                </svg>
            </div>
        </div>
        <div class="property-field">
            <input type="hidden" name="PropertyInfos.Index" value="${index_closure}">
            <div class="w-100">
                <input class="form-control mt-0 ml-0" placeholder="Название" name="PropertyInfos[${index_closure}].Name" maxlength=64 data-val="true" data-val-required="Название свойства должно быть задано"/>
                <span class="field-validation-valid ml-0" data-valmsg-for="PropertyInfos[${index_closure}].Name" data-valmsg-replace="true"></span>
                <span class="field-validation-valid ml-0" data-valmsg-for="PropertyInfos[${index_closure}].Options" data-valmsg-replace="true"></span>
            </div>
            <select class="form-control mt-0" name="PropertyInfos[${index_closure}].Type">
                <option value="0">Строка</option>
                <option value="1">Число</option>
                <option value="2">Опция</option>
            </select>
        </div>
        <div class="additional-info">
            
        </div>
    </div>
`;
    let result = $(htmlToElement(html));
    let $additionalInfo = result.find('.additional-info');
    result.find('.cross').click(function (){
        window.propertyCount -= 1;
        if(window.propertyCount <= 0)
            window.$submitButton.attr('disabled', 'disabled');
        result.remove();
        revalidateForm();
    });
    result.find('select').change(function (){
        $this = $(this);
        if($this.val() === '0' || $this.val() === '1')
            $additionalInfo.attr('hidden', 'hidden');
        else
        {
            $additionalInfo.removeAttr('hidden');
            if($additionalInfo[0].childElementCount === 0) {
                optionEntries[index_closure] = 0;
                $optionField = getOptionFieldElement(index_closure, $additionalInfo);
                $optionField.find('.minus').remove();
                $additionalInfo.append($optionField);
                revalidateForm();
            }
        }
    });
    
    return result;
}

function getOptionFieldElement(index, $additionalInfo){
    let html = `
    <div class="option-field">
        <div class="w-100">
            <input type="text" class="form-control" placeholder="Текст опции" name="PropertyInfos[${index}].Options" maxlength=64 data-val="true" data-val-required="Текст опции не может быть пустым"/>
        </div>
        <div class="icon-tab">
            <div class="plus">
                <div class="m-0">
                    <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" fill="currentColor" class="bi bi-plus" viewBox="0 0 16 16">
                      <path d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4z"/>
                    </svg>
                </div>
            </div>
            <div class="minus">
                <div class="m-0">
                    <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" fill="currentColor" class="bi bi-dash" viewBox="0 0 16 16">
                        <path d="M4 8a.5.5 0 0 1 .5-.5h7a.5.5 0 0 1 0 1h-7A.5.5 0 0 1 4 8z"/>
                    </svg>
                </div>
            </div>
        </div>
    </div>
`
    
    let result = $(htmlToElement(html));
    result.find('.plus').click(function (){
        optionEntries[index]++;
        $additionalInfo.append(getOptionFieldElement(index, $additionalInfo))
        revalidateForm();
    });
    
    result.find('.minus').click(function (){
        result.remove();
        revalidateForm();
    });
    
    return result;
}

function htmlToElement(html) {
    var template = document.createElement('template');
    html = html.trim();
    template.innerHTML = html;
    return template.content.firstChild;
}

function revalidateForm(){
    var form = $form
        .removeData("validator") /* added by the raw jquery.validate plugin */
        .removeData("unobtrusiveValidation");  /* added by the jquery unobtrusive plugin*/

    $.validator.unobtrusive.parse(form);
}