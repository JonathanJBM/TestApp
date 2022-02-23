$(document).ready(() => {
    sessionStorage.clear();
    initEvents();
});

function initEvents() {
    $("#btnLogin").on("click", (e) => {
        e.preventDefault();

        var credential = {
            Username: $("#txtUsername").val(),
            Password: btoa($("#txtPassword").val()),
            Salt: btoa("TEST")
        }

        credential.Password = credential.Password + credential.Salt;

        loginUser(credential).then((response) => {
            if (response.isSuccess) {
                sessionStorage.setItem("UserData", JSON.stringify(response.content));
                window.location = "/Index";
            } else {
                Swal.fire({
                    icon: 'error',
                    title: "Error",
                    html: `Credencial inválida<br>${response.message}`,
                    showConfirmButton: false
                });
            }
        }).catch((error) => {
            console.error(error);
            Swal.fire({
                icon: 'error',
                title: "Error",
                text: `Falló conexión con el servidor`,
                showConfirmButton: false
            });
        });
    });

    $("#txtPassword").on("keyup", (e) => {
        if (e.keyCode == 13) {
            $("#btnLogin").click();
        }
    });
}

async function loginUser(credential) {
    let response = await fetch('/api/Auth/Login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(credential)
    });
    return response.json();
}