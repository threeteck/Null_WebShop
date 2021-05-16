$(async () => {
    window.$propertyContainer = $('#properties-container');
    window.$categorySelect = $('#Category');
    window.$imageInput = $('#inputGroupFile01');
    window.$imageLabel = $('#image-label');
    window.$submitButton = $('#submit-button');
    window.$form = $('#product-create-form');
    
    $imageInput.change(()=>{
        let fileName = $imageInput[0].files[0].name;
        $imageLabel.html(fileName);
    })
    
    $categorySelect.change(async (e) => {
        await setProperties();
    });
    
    if($categorySelect.val() !== null){
        await setProperties();
    }
});

async function setProperties(){
    let formData = new FormData();
    $submitButton.attr('disabled', 'disabled');
    const response = await fetch(window.location.origin + `/adminpanel/api/getproperties?categoryId=${$categorySelect.val()}`);
    if(response.ok){
        $propertyContainer.empty();
        let data = await response.json();
        data.forEach((p) => {
            $propertyContainer.append(getElementFromProperty(p));
        });
        revalidateForm();

        $submitButton.removeAttr('disabled');
    }
}

function revalidateForm(){
    var form = $form
        .removeData("validator") /* added by the raw jquery.validate plugin */
        .removeData("unobtrusiveValidation");  /* added by the jquery unobtrusive plugin*/

    $.validator.unobtrusive.parse(form);
}

function getElementFromProperty(property){
    let filterInfo = JSON.parse(property.filterInfo)
    let constraints = JSON.parse(property.constraints)
    
    let html = `
    <div class="property-field">
        <input type="hidden" name="PropertyInfos.Index" value="${property.id}">
        <input type="hidden" name="PropertyInfos[${property.id}].PropertyId" value="${property.id}"/>
        <span class="property-name">${property.name}</span>
`;
    
    html += '<div class="w-100">'
    if(property.propertyType === "Integer" || property.propertyType === "Decimal")
        html += `<input class="form-control" placeholder="Значение" type="number" name="PropertyInfos[${property.id}].Value" data-val="true" data-val-required="Значение не задано" data-val-range="Значение должно быть в пределах от ${constraints.minValue} до ${constraints.maxValue}" data-val-range-max="${constraints.maxValue}" data-val-range-min="${constraints.minValue}">`
    
    if(property.propertyType == "Nominal")
        html += `<input class="form-control" placeholder="Значение" type="text" name="PropertyInfos[${property.id}].Value" maxlength=64 data-val="true" data-val-required="Значение не задано">`

    if(property.propertyType == "Option") {
        html += `<select class="form-control" name="PropertyInfos[${property.id}].Value">`
        filterInfo.options.forEach((option) => {
            html += `<option value="${option}">${option}</option>`
        });
        html += '</select>'
    }
    
    html += `<span class="field-validation-valid" data-valmsg-for="PropertyInfos[${property.id}].Value" data-valmsg-replace="true"></span>`
    html += '</div></div>'

    return htmlToElement(html);
}