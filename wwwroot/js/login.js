$(document).ready(() => {
    console.log("Entra");
    initEvents();
});

function initEvents() {
    $("#btnLogin").on("click", () => {
        console.log("Click");
        window.location = "/Index";
    });
}