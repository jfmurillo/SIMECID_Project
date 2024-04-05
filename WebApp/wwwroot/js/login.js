/// <reference path="controlactions.js" />
window.onload = function () {
    localStorage.clear();
};
document.getElementById("loginForm")
    .addEventListener("submit", function (event) {
        event.preventDefault();
        let email = document.getElementById("email").value;
        let password = document.getElementById("password").value;

        if (email === "" || password === "") {
            alert("Please fill out all values");
        } else {
            let keysAuth = {
                email: email,
                password: password
            };

            try {
                const ca = new ControlActions();
                ca.PostToAPI(keysAuth);







            } catch (Error error) {
                alert(error)
            }
        }

/*    document.getElementById("emailError").textContent = " ";
    document.getElementById("passwordError").textContent = " ";

    let email = document.getElementById("email").value;
    let password = document.getElementById("password").value;

    if (email.trim() === "") {
        document.getElementById("emailError").textContent = "¡Email address is required!";
        return;
    }

    if (password.trim() === "") {
        document.getElementById("passwordError").textContent = "¡Password is required!";
        return;
    }

    var ca = new ControlActions;

    ca.PostToAPI();

    alert("Log-In successful" + email);*/

})