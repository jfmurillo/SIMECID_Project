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
            ec.Create();
        });

        $("#verifyMe").click(function () {
            var sc = new EmailController2();
            var otp = $("#txtOTP").val();
            let urlParams = new URLSearchParams(window.location.search);
            email = urlParams.get(`email`)
            sc.ValidateOTP(email, otp);

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
        let user = {
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
        let user = {
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

        let ca = new ControlActions();
        let srvR = "User/Create"

        ca.PostToAPI(srvR, user, function (response) {
            console.log(response);
            setTimeout(function () {
                window.location.href = `/CodeVerification?email=${user.email}`;
            }, 1000);
        })
    }
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
                callback();
            }
        }, function (error) {
            console.error("Error sending email:", error);
        });
    };


    this.ValidateOTP = function (email, otp) {
        if (email === "" || otp === "") {
            return;
        }

        let data = {
            email,
            otp
        }

        var srv = "ValidateOTP/CreateData"
        ca.PostToAPI(srv, data, function (response) {
            console.log("Creating data for database");
            var serviceRoute = "ValidateOTP/VerifyOtp";

            ca.GetToApi(serviceRoute, function (response) {
                console.log(response);
                var validOTP = false;
                for (var i = 0; i < response.length; i++) {
                    if (response[i].otp === otp && response[i].email === email) {
                        validOTP = true;
                        break;
                    }
                }
                if (validOTP) {
                    console.log("Valid OTP");
                    let sc = new SignUpController();
                    sc.Create();
                    window.location.href = "/Login";
                } else {
                    console.log("Invalid OTP");
                }
            }, function (error) {
                console.error("Error validating OTP:", error);
            });
        });
    };
}

    $(document).ready(function () {
        var ec = new SignUpController();
        ec.InitView();
    });