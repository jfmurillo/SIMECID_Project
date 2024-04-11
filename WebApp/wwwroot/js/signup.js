function SignUpController() {
    this.ViewName = "User";
    this.ApiService = "User";
    var email;

    this.InitView = function () {
        console.log("init view sign up");
        let urlParams = new URLSearchParams(window.location.search);
        console.log(urlParams.get(`email`));
        $("#BtnSignIn").click(function () {
            let ec = new SignUpController();
            ec.SendEmail(); // Envía el correo electrónico con el OTP
        });

        $("#verifyMe").click(function () {
            var sc = new EmailController2();
            var otp = $("#txtOTP").val();
            let urlParams = new URLSearchParams(window.location.search);
            email = urlParams.get(`email`)
            sc.ValidateOTP(email, otp); // Valida el OTP ingresado por el usuario
        });
    };

    this.SendEmail = function () {
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

        var emailController = new EmailController2();
        emailController.SendEmail(email, function () {
            this.ValidateOTP(email);
        });

        setTimeout(function () {
            window.location.href = `/CodeVerification?email=${user.email}`;
        }, 1000);
    };
}

function EmailController2() {
    this.ViewName = "Email";
    this.ApiService = "Email";
    var ca = new ControlActions();

    this.SendEmail = function (email, callback) {
        var keysAuth = {
            emailAddress: email,
            otp: 0
        };

        var serviceRoute = this.ApiService + "/SendEmail";
        ca.PostToAPI(serviceRoute, keysAuth, function (response) {
            console.log("Email sent successfully");
            if (callback && typeof callback === 'function') {
                callback(); // Llama a la función de retorno si está definida
            }
        }, function (error) {
            console.error("Error sending email:", error);
            alert("An error occurred while sending the email. Please try again later.");
        });
    };


    this.ValidateOTP = function (email, otp) {
        if (email === "" || otp === "") {
            alert("Email or OTP cannot be null or empty.");
            return;
        }

        let data = {
            email,
            otp
        }s

        var srv = "ValidateOTP/CreateData"
        ca.PostToAPI(srv, data, function (response) {
            console.log("crear data para bd")
            var serviceRoute = "ValidateOTP/VerifyOtp";

            let valiodateOTP = ca.GetToApi(serviceRoute, function (response) {
                console.log(response)
                if (response === true) {
                    alert("OTP is valid");
                    console.log("OTP valido");

                    window.location.href = "/Login";
                } else {
                    console.log("OTP inválido");
                    alert("Invalid OTP.");
                }
            }, function (error) {
                console.error("Error validating OTP:", error);
                alert("An error occurred while validating the OTP. Please try again.");
            });
        })
    };
}

    $(document).ready(function () {
        var ec = new SignUpController();
        ec.InitView();
    });
