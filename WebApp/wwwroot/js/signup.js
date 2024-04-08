function SignUpController() {
    this.ViewNmae = "User";
    this.ApiService = "User";

    this.InitView = function () {
        console.log("init view sign up")
        $("#BtnSignIn").click(function () {
            let ec = new SignUpController();
            ec.Create();
        });
    };
    this.Create = function () {
        
        var name = $("#txtName").val();
        var lastName = $("#txtLastName").val();
        var phoneNumber = $("#txtPhoneNumber").val();
        var email = $("#txtEmail").val();
        var password = $("#txtPassword").val();
        var sex = $("#txtSex").val();
        var birthdate = $("#txtBirthdate").val();
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
            otp: ''
        };

        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Create";

        ca.PostToAPI(serviceRoute, user, function (response) {
            console.log(response);
            let ee = new EmailController();
            ee.SendEmail()
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
        let email = $("#txtEmail").val();

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

        ca.PostToAPI(srvRoute, keysAuth, function (response) {
            console.log(response)
            alert("Email has been sent");
        })
    }
}

$(document).ready(function () {
    var ec = new SignUpController();
    var ee = new EmailController();
    ee.InitView();
    ec.InitView();
});
