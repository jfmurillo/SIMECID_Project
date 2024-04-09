function SignUpController() {
    this.ViewNmae = "User";
    this.ApiService = "User";

    var email = "";
    this.InitView = function () {
        console.log("init view sign up")
        $("#BtnSignIn").click(function () {
            let ec = new SignUpController();
            email = $("#txtEmail").val();
            ec.Create();
            /*ec.ValidateOTP();*/
            $("#codeVerificationForm").submit();
        });

        $("#verifyMe").click(function () {
            var sc = new ValidateOTPController();
            sc.ValidateOTP(email);
        });
    };

    this.Create = function () {
        
        var name = $("#txtName").val();
        var lastName = $("#txtLastName").val();
        var phoneNumber = $("#txtPhoneNumber").val();
        var email = $("#txtEmail").val();
        var password = $("#txtPassword").val();
        var sex = $("#txtSex").val();
        var birthdate = new Date($("#txtBirthdate").val());
        var address = $("#txtAddress").val();
        var user = {
            name: name,
            lastName: lastName,
            email: email,
            phoneNumber: phoneNumber,
            password: password,
            sex: sex,
            birthDate: birthdate,
            role: 'Default',
            status: 'Default',
            address: address,
        };

        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Create";

        ca.PostToAPI(serviceRoute, user, function (response) {
            console.log(response);
            let ee = new EmailController();
            console.log("estoy aqui")
            ee.SendEmail();

            window.location.href = "/CodeVerification";
        });
    };
}


function EmailController() {
    this.ViewNmae = "Email";
    this.ApiService = "Email";

    this.InitView = function () {
        let ee = new EmailController();
        console.log("email controller");
    };

    this.SendEmail = function () {
        var email = $("#txtEmail").val();

        if (email === "") {
            alert("Please enter an email address.");
            return;
        }

        let keysAuth = {
            emailAddress: email,
            otp: 0
        };

        let srvRoute = this.ApiService + "/SendEmail"
        var ca = new ControlActions();

        ca.PostToAPI(srvRoute, keysAuth, function () {
            console.log("estoy aqui - email api")
        })
    }
}


function ValidateOTPController() {
    this.InitView = function () {
        console.log("init view validate otp")
        $("#verifyMe").click(function () {
            let sc = new ValidateOTPController();
            sc.ValidateOTP();
        })
    };

    this.ValidateOTP = function (email) {
        let ec = new SignUpController();
        
        var otp = $("#txtOTP").val();

        if (email === "" || otp === "") {
            /*alert("Por favor ingresa tu correo electrónico y OTP.");*/
            return;
        }

        let srvRoute = this.ApiService + "/ValidateOTP";
        var ca = new ControlActions();

        ca.GetToApi(srvRoute + "?email=" + email + "&otp=" + otp, function (response) {
            if (response === true) {
                alert("OTP is valid")
                console.log("OTP válido");
                //let ec = new SignUpController();
                //ec.Create();
                // Continuar con el proceso de registro del usuario
                // Llamar a la función Create para crear el usuario
            } else {
                console.log("OTP inválido");
                alert("Invalid OTP.");
                // Permitir al usuario corregir el OTP
            }
        });
    };
}

$(document).ready(function () {
    var ec = new SignUpController();
    var ee = new EmailController();
    var vc = new ValidateOTPController();
    ee.InitView();
    ec.InitView();
    vc.InitView();

    // Manejar el evento click del botón "Create Account"
    $("#BtnSignIn").click(function () {
        let ec = new SignUpController();
        ec.Create();
        // Submit del formulario para redirigir al usuario
        $("#codeVerificationForm").submit();
    });

    $("#verifyMe").click(function () {
        // Lógica para verificar el código aquí
        alert("Verification code is being verified...");
        // Ejemplo de redirección a otra página después de la verificación
        window.location.href = "/Login"; // Cambia "/Dashboard" por la ruta deseada
    });

});