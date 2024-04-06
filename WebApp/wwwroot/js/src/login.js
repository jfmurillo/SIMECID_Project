window.onload = function () {
    localStorage.clear();
};
document.getElementById("loginForm")
    .addEventListener("submit", function (event) {
        event.preventDefault();
        let email = document.getElementById("email").value;
        let password = document.getElementById("password").value;

        
        localStorage.setItem("userEmail", email);

        if (email === "" || password === "") {
            alert("Please fill out all values");
        } else {
            let keysAuth = {
                email: email,
                password: password
            };

            try {
                const ca = new ControlActions();
                ca.PostToAPI("authentication-service", keysAuth, function (response) {
                    console.log(response)
                });
            } catch (error) {
                alert(error.message)
            }
        }
    })