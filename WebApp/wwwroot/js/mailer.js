function EmailController() {
    this.ViewNmae = "Email";
    this.ApiService = "Email";

    this.InitView = function () {
        console.log("init view mail")
        let urlParams = new URLSearchParams(window.location.search);
        console.log(urlParams.get(`email`));
        $("#btnTry").click(function () {
            let ec = new EmailController();
            ec.SendEmail();
        });
    };



    this.SendEmail = function () {
        let email = $("#email").val();

        if (email === "") {
            
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

            setTimeout(function () {
                window.location.href = `/RecoverPassword?email=${email}`;
            }, 1000);
        })
    }
}


$(document).ready(function () {
    var ec = new EmailController();
    ec.InitView();
});

