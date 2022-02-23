$(document).ready(() => {
    initEvents();
    getAllUsers();
});

function initEvents() {

}

function getAllUsers() {
    fetch('/api/Users/Get')
        .then(response => response.json())
        .then(data => console.log(data));
}