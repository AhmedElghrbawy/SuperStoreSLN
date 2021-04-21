
// Review Stars events

let starContainer = document.getElementsByClassName("stars-container-test")[0];


starContainer.addEventListener("click", modifyReviewStars);

function modifyReviewStars(event) {
    let starNumber = event.target.dataset.star_number;
    if (starNumber == undefined) {
        return;
    }
    document.getElementById("stars-bind").value = starNumber; // bind for form submit

    starNumber = parseInt(starNumber)

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





// Review collapse button events

document.getElementById("Review-toggle-button").addEventListener("click", event => {
    let collapseIcon = document.querySelector("#Review-toggle-icon");
    collapseIcon.classList.toggle("fa-plus");
    collapseIcon.classList.toggle("fa-chevron-up");
})
