function EmailController() {
    this.ViewNmae = "Email";
    this.ApiService = "Email";

    this.InitView = function () {
        console.log("init view mail")
        $("#btnTry").click(function () {
            let ec = new EmailController();
            ec.SendEmail();
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
            alert("Email has been sent");
        })
    }
}


$(document).ready(function () {
    var ec = new EmailController();
    ec.InitView();
});

