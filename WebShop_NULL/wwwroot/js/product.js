$(function (){
    let $reviewButton = $('#reviews-tab')
    let $propertiesButton = $('#properties-tab')
    
    let $reviewContainer = $('#product-reviews-container')
    let $propertiesContainer = $('#product-details-container')

    $reviewButton.click(function (){
        $reviewContainer.removeAttr('hidden');
        $propertiesContainer.attr('hidden', 'hidden');
        
        $reviewButton.removeClass('active');
        $propertiesButton.removeClass('active');
        $reviewButton.addClass('active');
    });

    $propertiesButton.click(function (){
        $propertiesContainer.removeAttr('hidden');
        $reviewContainer.attr('hidden', 'hidden');

        $reviewButton.removeClass('active');
        $propertiesButton.removeClass('active');
        $propertiesButton.addClass('active');
    });
});