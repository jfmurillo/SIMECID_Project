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
            Email: email,
            Password: password 
        }

        serviceRoute = "Login/Authenticate"
        let ca = new ControlActions();
        ca.PostToAPI(serviceRoute, loginData, (response) => {
            if (response.status == 200) {
                console.log("Login successful");
                this.ManageRol(email);
                
            } else {
                console.log(response)
                console.log("Error during login:", response.message); 
                throw new Error("Error during login")
            }
        });
    }

    this.ManageRol = function (email) {
        let route = "User/RetrieveRoleByUserEmail"
        let ca = new ControlActions();

        let user = {
            Email: email
        }

        console.log(email)
        ca.PostToAPI(route, user, (response) => {
            if (response.status == 200) {
                console.log("manage rol");
                let role = response.role

                switch (role) {
                    case "Nurse":
                        setTimeout(function () {
                            window.location.href = `/Nurse-Profile`;
                        }, 500);
                        break;
                    case "Secretary":
                        setTimeout(function () {
                            window.location.href = `/Secretary-Profile`;
                        }, 500);
                        break;
                    case "Doctor":
                        setTimeout(function () {
                            window.location.href = `/Doctor-Profile`;
                        }, 500);
                        break;
                    case "User":
                        setTimeout(function () {
                            window.location.href = `/Patient-UserProfile`;
                        }, 500);
                        break;
                    case "Admin":
                        setTimeout(function () {
                            window.location.href = `/UserProfile`;
                        }, 500);
                        break;
                    default:
                        setTimeout(function () {
                            window.location.href = `/Error`;
                        }, 500);
                        break;
                }

            } else {
                console.log(response)
                console.log("Error during login:", response.message);
                throw new Error("Error during login")
            }
        });






        /*ca.PostToAPI(route, data, (response) => {
            console.log(response)
            let role = response.Role

            switch (role) {
                case "Nurse":
                    setTimeout(function () {
                        window.location.href = `/Nurse-Profile`;
                    }, 500);
                    break;
                case "Secretary":
                    setTimeout(function () {
                        window.location.href = `/Secretary-Profile`;
                    }, 500);
                    break;
                case "Doctor":
                    setTimeout(function () {
                        window.location.href = `/Doctor-Profile`;
                    }, 500);
                    break;
                case "User":
                    setTimeout(function () {
                        window.location.href = `/Patient-Profile`;
                    }, 500);
                    break;
                case "Admin":
                    setTimeout(function () {
                        window.location.href = `/UserProfile`;
                    }, 500);
                    break;
                default:
                    setTimeout(function () {
                        window.location.href = `/Error`;
                    }, 500);
                    break;
            }
        });*/
    }

/*    function hashPassword(password) {
        let hashedPassword = CryptoJS.SHA256(password).toString(CryptoJS.enc.Base64);
        return hashedPassword;
    }*/

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
                    }, 1000);
                    
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
            }, 2000);
        });
    };
}

    $(document).ready(function () {
        let lc = new LoginController();
        lc.InitView();
    })