$(()=>{
    $('tr.product-item').each(function() {
        $this = $(this)
        id = $this.attr('id');
        $this.find('.delete-button').click(async ()=>{
            const response = await fetch(window.location.origin + `/adminpanel/api/deleteProduct?productId=${id}`);
            if(response.ok) {
                $this.remove();
            }
        })
    });
})