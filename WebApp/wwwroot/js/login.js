function LoginController() {
    this.ViewName = "Login";
    this.ApiService = "Login";
    var email;
    var ca = new ControlActions();

    this.InitView = function () {
        console.log("login init view")

        $("#btn").click(function () {
            let lc = new LoginController();
            lc.Authenticate();
        });

        $("#btnTry2").click(function () {
            let lc = new LoginController();
            var otp = $("#txtOTP").val();
            let urlParams = new URLSearchParams(window.location.search);
            email = urlParams.get("email")
            lc.ValidateOTP(email, otp)
        })

        $("#go").click(function () {
            let lc = new LoginController();
            let urlParams = new URLSearchParams(window.location.search);
            email = urlParams.get("email")
            lc.NewPasswordUpdate();
        })

    }

    this.Authenticate = function () {
        let email = $("#email").val();
        let password = $("#password").val();

        let loginData = {
            email,
            password
        }

        serviceRoute = "Login/Authenticate"
        let ca = new ControlActions();
        ca.PostToAPI(serviceRoute, loginData, function (response) {
            if (response.status == 200) {
                console.log("login successful")
            } else {
                console.log("error")
            }

            setTimeout(function () {
                window.location.href = `/UserProfile`;
            }, 1000);
        })
    }

    this.ValidateOTP = function (email, otp) {
        if (email === "" || otp === "") {
            return;
        }

        let data = {
            email,
            otp
        }

        var srv = "RecoverPassword/CreateData"
        ca.PostToAPI(srv, data, function (response) {
            console.log("Creating data for database");
            var serviceRoute = "RecoverPassword/VerifyOtp";

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
                    setTimeout(function () {
                        window.location.href = `/NewPassword?email=${data.email}`;
                    }, 2000);
                } else {
                    console.log("Invalid OTP");
                }
            }, function (error) {
                console.error("Error validating OTP:", error);
            });
        });
    };

    this.NewPasswordUpdate = function () {
        let newPassword = $("#new-pass").val();
        let confirmPassword = $("#conf-pass").val();
        let email;

        if (newPassword !== confirmPassword) {
            throw new Error("Passwords don't match")
        }

        let urlParams = new URLSearchParams(window.location.search);
        email = urlParams.get("email");

        let data = {
            email: email,
            newPassword: newPassword,
            confirmPassword: confirmPassword
        };

        let serviceRoute = "RecoverPassword/Update";

        ca.PostToAPI(serviceRoute, data, function (response) {
            console.log(response);

            setTimeout(function () {
                window.location.href = `/Login`;
            }, 1000);
        });
    };
}

    $(document).ready(function () {
        let lc = new LoginController();
        lc.InitView();
    })