function LogoutController() {
    this.InitView = function () {
        $("#LogOut").click(function () {
            window.location.href = "/Logout";
        });
    };
}

$(document).ready(function () {
    let lc = new LogoutController();
    lc.InitView();
});
