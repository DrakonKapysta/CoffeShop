function checkItem() {
    const active = "nav-item nav-link active";
    const items = document.getElementsByClassName("nav-item nav-link");
    for (var i = 0; i < items.length; i++) {
        if (window.location.pathname.split('/')[1] === '') {
            items[i].className = active;
            break;
        }
        else if (items[i].textContent.toLowerCase() === window.location.pathname.split('/')[1]) {
            items[i].className = active;
            break;
        }
    }
}
checkItem();
