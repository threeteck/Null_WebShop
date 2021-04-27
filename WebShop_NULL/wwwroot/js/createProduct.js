$(() => {
    $propertyContainer = $('#properties-container');
    $categorySelect = $('#Category');
    $imageInput = $('#inputGroupFile01');
    $imageLabel = $('#image-label');
    $submitButton = $('#submit-button')
    
    $imageInput.change(()=>{
        let fileName = $imageInput[0].files[0].name;
        $imageLabel.html(fileName);
    })
    
    $categorySelect.change(async (e) => {
        let formData = new FormData();
        $submitButton.attr('disabled', 'disabled');
        const response = await fetch(window.location.origin + `/adminpanel/api/getproperties?categoryId=${$categorySelect.val()}`);
        if(response.ok){
            $propertyContainer.empty();
            let data = await response.json();
            data.forEach((p) => {
                $propertyContainer.append(getElementFromProperty(p))
            });
            
            $submitButton.removeAttr('disabled');
        }
    });
});

function getElementFromProperty(property){
    let html = `
    <div class="property-field">
        <input type="hidden" name="PropertyInfos.Index" value="${property.id}">
        <input type="hidden" name="PropertyInfos[${property.id}].PropertyId" value="${property.id}"/>
        <span class="property-name">${property.name}</span>
`;
    
    if(property.propertyType === "Integer" || property.propertyType === "Decimal")
        html += `<input class="form-control" placeholder="Значение" type="number" name="PropertyInfos[${property.id}].Value" max="1000000000">`
    
    if(property.propertyType == "Nominal")
        html += `<input class="form-control" placeholder="Значение" type="text" name="PropertyInfos[${property.id}].Value" maxlength=64>`

    if(property.propertyType == "Option") {
        html += `<select class="form-control" name="PropertyInfos[${property.id}].Value">`
        let filterInfo = JSON.parse(property.filterInfo)
        filterInfo.options.forEach((option) => {
            html += `<option value="${option}">${option}</option>`
        });
        html += '</select>'
    }
    
    html += '</div>'

    return htmlToElement(html);
}

function htmlToElement(html) {
    var template = document.createElement('template');
    html = html.trim();
    template.innerHTML = html;
    return template.content.firstChild;
}