﻿// Obtener el correo electrónico de la URL
const urlParams = new URLSearchParams(window.location.search);
const email = urlParams.get("email");

// Verificar si se encontró un correo electrónico en la URL
if (email) {
    // Obtener el elemento del input de email por su ID
    const emailInput = document.getElementById("email");

    // Verificar si se encontró el elemento del input de email
    if (emailInput) {
        // Establecer el valor del correo electrónico como placeholder del input
        emailInput.placeholder = email;
    }
}

// Controlador para redireccionar
function RedirectController() {
    this.ViewName = "Redirect";
    var ca = new ControlActions();

    this.InitView = function () {
        console.log("Redirect init view");

        // Configurar redirección en función del rol o acción

        // Redirección para Examination
        $("#examinationMenuId").click(function () {
            this.RedirectToPage("/Nurse-Examination", email);
        }.bind(this));

        // Redirección para Prescriptions
        $("#prescriptionsMenuId").click(function () {
            this.RedirectToPage("/Nurse-Prescription", email);
        }.bind(this));

        // Redirección para Medical Report
        $("#medicalReportMenuId").click(function () {
            this.RedirectToPage("/Nurse-MedicalReport", email);
        }.bind(this));

        // Redireccion para perfil
        $("#profileMenuId").click(function () {
            this.RedirectToPage("/Nurse-Profile", email);
        }.bind(this));
        $("#settingsMenuId").click(function () {
            this.RedirectToPage("/Nurse-Settings", email);
        }.bind(this));
    }

    this.RedirectToPage = function (pageName, email) {
        // Construir la URL de redirección con el email como parámetro
        let redirectUrl = `${window.location.origin}${pageName}?email=${encodeURIComponent(email)}`;

        // Redirigir a la página correspondiente
        window.location.href = redirectUrl;
    }
}

$(document).ready(function () {
    let rc = new RedirectController();
    rc.InitView();
});