$(async () => {
    window.reviewPage = 0;
    window.reviewsTotalPages = Number($('#review-total-pages').val());
    window.$reviewsContainer = $('#reviews-container');
    window.productId = Number($('#product-id').val());
    window.$reviewsPaginationBar = $('#reviews-pagination-bar');
    
    await changePage(0);
});

async function changePage(page){
    window.reviewPage = page;
    if(reviewPage >= reviewsTotalPages)
        window.reviewPage = reviewsTotalPages - 1
    if(reviewPage < 0)
        window.reviewPage = 0
    
    $reviewsContainer.empty();
    await appendReviews(window.reviewPage);
    
    $reviewsPaginationBar.empty();
    $reviewsPaginationBar.append(getPaginationBarElement(window.reviewPage, 
        window.reviewsTotalPages,
        async function(p){
            await changePage(p);
        })
    );
}

async function appendReviews(page){
    let response = await fetch(window.location.origin + `/product/${productId}/reviews?page=${page}`);
    if(response.ok){
        let data = await response.json();
        data.forEach(function(review){
            let reviewElement = getElementFromReview(review);
            $reviewsContainer.append(reviewElement);
        });
    }
}

function srgbInverseCompanding(r, g, b)
{
    let r_res = inverseComponent(r / 255) * 255;
    let g_res = inverseComponent(g / 255) * 255;
    let b_res = inverseComponent(b / 255) * 255;

    return {r:r_res, g:g_res, b:b_res};

    function inverseComponent(comp)
    {
        if (comp > 0.04045)
            return Math.pow((comp + 0.055)/1.055, 2.4);
        return comp / 12.92;
    }
}

function SrgbCompanding(r, g, b)
{
    var r_res = compandComponent(r / 255) * 255;
    var g_res = compandComponent(g / 255) * 255;
    var b_res = compandComponent(b / 255) * 255;

    return {r:r_res, g:g_res, b:b_res};

    function compandComponent(comp)
    {
        if (comp > 0.0031308)
            return 1.055 * Math.pow(comp, 1 / 2.4) - 0.055;
        return comp * 12.92;
    }
}

function getElementFromReview(review){
    let percent = (review.rating - 1) / 9;
    let fullRed = srgbInverseCompanding(255, 0, 0);
    let fullGreen = srgbInverseCompanding(0, 255, 0);

    let red = ((1 - percent) * fullRed.r + percent * fullGreen.r);
    let green = ((1 - percent) * fullRed.g + percent * fullGreen.g);
    let blue = ((1 - percent) * fullRed.b + percent * fullGreen.b);

    let result = SrgbCompanding(red, green, blue);
    
    let rgb = `rgb(${result.r}, ${result.g}, ${result.b})`;
    let html = `
<div class="box p-3">
    <div class="review">
        <div class="review-name">
            <a href="${window.location.origin +`/profile/${review.userId}`}">${review.userName}</a>
        </div>
        <div class="review-image-container d-flex justify-content-center align-items-center">
            <img src="${window.location.origin +`/${review.userImagePath}`}" class="review-image"/>
        </div>
        <div class="review-content">
            ${review.content}
        </div>
        <div class="review-rating-container">
            <div class="review-rating-star-text d-flex justify-content-center align-items-center">
                <div>${review.rating}</div>
            </div>
            <div class="review-rating-star">
                <svg xmlns="http://www.w3.org/2000/svg" width="128" height="128" fill="currentColor" class="bi bi-star-fill" viewBox="0 0 16 16">
                    <path d="M3.612 15.443c-.386.198-.824-.149-.746-.592l.83-4.73L.173 6.765c-.329-.314-.158-.888.283-.95l4.898-.696L7.538.792c.197-.39.73-.39.927 0l2.184 4.327 4.898.696c.441.062.612.636.282.95l-3.522 3.356.83 4.73c.078.443-.36.79-.746.592L8 13.187l-4.389 2.256z"
                          style="color: ${rgb};"/>
                </svg>
            </div>
        </div>
    </div>
</div>
`;

    return htmlToElement(html);
}