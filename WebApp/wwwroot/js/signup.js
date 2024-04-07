function EmailController() {
    this.ViewNmae = "Email";
    this.ApiService = "Email";

    this.InitView = function () {
        console.log("init view sign up")
        $("#btnTry").click(function () {
            let ec = new EmailController();
            ec.SendEmail();
            alert("Email has been sent");
        });
    };

    this.SendEmail = function () {
        let email = $("#email").val().trim();

        if (email.trim() === "") {
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
        })
    }
}


$(document).ready(function () {
    var ec = new EmailController();
    ec.InitView();
});
