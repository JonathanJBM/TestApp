$(document).ready(() => {
    initEvents();
    rebuildTableUsers();
});

function initEvents() {
    $("#txtEmail").on("keyup", function (e) {
        if (validateEmail($(this).val())) {
            $(this).addClass("is-valid");
            $(this).removeClass("is-invalid");
        } else {
            $(this).addClass("is-invalid");
            $(this).removeClass("is-valid");
        }
    });

    $("#txtUsername").on("keyup", function (e) {
        if ($(this).val().length > 7) {
            $(this).addClass("is-valid");
            $(this).removeClass("is-invalid");
        } else {
            $(this).addClass("is-invalid");
            $(this).removeClass("is-valid");
        }
    });

    $("#txtPassword").on("keyup", function (e) {
        if (validatePassword($(this).val()) && $(this).val().length > 10) {
            $(this).addClass("is-valid");
            $(this).removeClass("is-invalid");
        } else {
            $(this).addClass("is-invalid");
            $(this).removeClass("is-valid");
        }
    });

    $("#txtRePassword").on("keyup", function (e) {
        if ($(this).val() === $("#txtPassword").val()) {
            $(this).addClass("is-valid");
            $(this).removeClass("is-invalid");
        } else {
            $(this).addClass("is-invalid");
            $(this).removeClass("is-valid");
        }
    });

    $("#newUserModal").on("hidden.bs.modal", function (e) {
        $("#txtEmail").val("");
        $("#txtUsername").val("");
        $("#txtPassword").val("");
        $("#txtRePassword").val("");
        $(".sex-radio").prop("checked", false);
    });

    $("#btnNewUser").on("click", function (e) {
        e.preventDefault();
        $("#btnSaveUser").data("operation",1);
    });

    $("#btnEditUser").on("click", function (e) {
        e.preventDefault();
        $("#btnSaveUser").data("operation", 2);
        var user = $("#usersTable").bootstrapTable("getSelections")[0];

        $("#txtEmail").val(user.email);
        $("#txtUsername").val(user.username);
        $("#txtPassword").val(atob(user.password));
        $("#txtRePassword").val(atob(user.password));
        $(`input[name=sexRadio][value=${user.genre}]`).prop("checked", true);

    });

    $("#btnDelUser").on("click", function (e) {
        e.preventDefault();
        var user = $("#usersTable").bootstrapTable("getSelections")[0];

        Swal.fire({
            title: 'Eliminar',
            text: `¿Quieres eliminar el registro "${user.username}"?`,
            showCancelButton: true,
            confirmButtonText: 'Eliminar',
            cancelButtonText:"Cancelar",
            confirmButtonColor: 'red',
        }).then((result) => {
            if (result.isConfirmed) {
                deleteUser(user.id).then((response) => {
                    if (response.isSuccess) {
                        rebuildTableUsers();
                        Swal.fire('Eliminado', 'Registro eliminado correctamente', 'success')
                    } else {
                        Swal.fire('Error', `Ocurrió un error al eliminar el registro\n${response.message}`, 'error')
                    }
                }).catch((error) => {
                    console.log(error);
                    Swal.fire('Error', `Ocurrió un error al eliminar el registro\n${JSON.stringify(error)}`, 'error')
                });
                
            }
        })
    });

    $("#btnSaveUser").on("click", function (e) {
        e.preventDefault();
        $(this).prop("disabled", true);
        var idSelectedUser = $("#usersTable").bootstrapTable("getSelections")[0];

        var user = {
            Id: (idSelectedUser != 0 && idSelectedUser != undefined) ? idSelectedUser.id : 0,
            Email: $("#txtEmail").val(),
            Username: $("#txtUsername").val(),
            Password: btoa($("#txtPassword").val()),
            Genre: $('input[name=sexRadio]:checked').val(),
            CreationDate: new Date().toISOString(),
            Status: false
        };
        console.log(user);

        var operation = $(this).data("operation");
        saveInfoUser(user, operation);
        $(this).prop("disabled", false);
    });

    $("#usersTable").on("check.bs.table", function (row, $element) {
        $("#btnEditUser").prop("disabled", false);
        $("#btnDelUser").prop("disabled", false);
    });

    $("#usersTable").on("uncheck.bs.table", function (row, $element) {
        $("#btnEditUser").prop("disabled", true);
        $("#btnDelUser").prop("disabled", true);
    });
}

function saveInfoUser(user, operation) {
    switch (operation) {
        case 1:
            createUser(user).then(response => {
                if (response.isSuccess) {
                    Swal.fire({
                        icon: 'success',
                        title: "Correcto",
                        text: `Información guardada existosamente`,
                        showConfirmButton: false,
                        timer: 2000
                    });
                    closeModals();

                    rebuildTableUsers();
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: "Ocurrió un error",
                        text: `Se produjo un error al guardar los datos.\n${JSON.stringify(response)}`,
                        showConfirmButton: true
                    });
                }
                console.log(response);
            }).catch(error => {
                Swal.fire({
                    icon: 'success',
                    title: "Ocurrió un error",
                    text: `${JSON.stringify(response)}`,
                    showConfirmButton: true
                });
                console.error(error)
            });
            break;
        case 2:
            updateUser(user).then(response => {
                if (response.isSuccess) {
                    Swal.fire({
                        icon: 'success',
                        title: "Correcto",
                        text: `Información guardada existosamente`,
                        showConfirmButton: false,
                        timer: 2000
                    });
                    closeModals();

                    rebuildTableUsers();
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: "Ocurrió un error",
                        text: `Se produjo un error al guardar los datos.\n${JSON.stringify(response)}`,
                        showConfirmButton: true
                    });
                }
                console.log(response);
            }).catch(error => {
                Swal.fire({
                    icon: 'success',
                    title: "Ocurrió un error",
                    text: `${JSON.stringify(response)}`,
                    showConfirmButton: true
                });
                console.error(error)
            });
            break;
        default:
    }
}

async function rebuildTableUsers() {
    $("#usersTable").bootstrapTable("destroy").bootstrapTable("showLoading");
    var response = await getAllUsers().then(response => response.json());
    console.log(response);
    if (response.isSuccess) {
        if (response.content.length) {
            $("#usersTable").bootstrapTable("destroy").bootstrapTable({ data: response.content });
        } else {
            $("#usersTable").bootstrapTable("destroy").bootstrapTable();
        }
    } else {
        $("#usersTable").bootstrapTable("destroy").bootstrapTable();
        Swal.fire({
            position: 'top-end',
            icon: 'error',
            title: "Ocurrió un error",
            text: `Se produjo un error al obtener los usuarios.\n${JSON.stringify(response)}`,
            showConfirmButton: false,
            timer: 3000
        })
    }
    $("#btnEditUser").prop("disabled", true);
    $("#btnDelUser").prop("disabled", true);
}

async function getAllUsers() {
    return await fetch('/api/Users/GetAll');
}

async function getUser(id) {
    return await fetch(`/api/Users/Get?Id=${id}`);
}

async function createUser(user) {
    let response = await fetch('/api/Users/Create', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    });
    return response.json();
}

async function updateUser(user) {
    let response = await fetch('/api/Users/Edit', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    });
    return response.json();
}

async function deleteUser(id) {
    let response = await fetch(`/api/Users/Delete?Id=${id}`, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        }
    });
    return response.json();
}

function loadingTemplate() {
    return '<i class="fa fa-spinner fa-spin fa-fw fa-2x"></i>'
}

function closeModals() {
    $('.modal').modal('hide');
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();
}

function statusFormatter(value, row) {
    console.log(value);
    var icon = value ? 'fa-check' : 'fa-times'
    return '<i class="fa ' + icon + '"></i> ' + (value ? "Activo":"Inactivo")
}