


let starContainer = document.getElementsByClassName("stars-container-test")[0];


starContainer.addEventListener("click", modifyReviewStars);
console.log(starContainer);

function modifyReviewStars(event) {
    var starNumber = event.target.dataset.star_number;
    if (starNumber == undefined) {
        return;
    }
    let starGroup = starContainer.querySelectorAll(".review-star");
    for (let i = 0; i < starNumber; i++) {
        starGroup[i].classList.remove("far");
        starGroup[i].classList.add("fas");
    }

    for (let i = starNumber; i < 5; i++) {
        starGroup[i].classList.remove("fas");
        starGroup[i].classList.add("far");
    }
}