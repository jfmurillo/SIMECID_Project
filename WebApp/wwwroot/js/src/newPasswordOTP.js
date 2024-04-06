window.onload = function () {
    localStorage.clear();
};

document.getElementById("newPassForm").addEventListener("submit", function (event) {
    debugger
    event.preventDefault();


    let newPassword = document.getElementById("new-pass").value;
    let confirmPassword = document.getElementById("conf-pass").value;

    let newPass = localStorage.setItem("newPass", newPass);
    let confPass = localStorage.setItem("confPass", confPass);


    if (newPassword === "" || confirmPassword === "") {
        alert("Please fill out all values");
        return;
    }

    if (!email) {
        alert("No email address was found.");
    }

    if (newPassword !== confirmPassword) {
        alert("Passwords do not match.");
        return;
    }

    let keysAuth = {
        email: email,
        password: newPassword
    };

    try {
        const ca = new ControlActions();
        ca.PostToAPI("authentication-service", keysAuth, function (response) {
            console.log(response);
            const OTP = generateOTP();
            sendOTPByEmail(email, OTP);
        });
    } catch (error) {
        alert(error.message);
    }
});

function generateOTP() {
    let digits = '0123456789';
    let OTP = '';
    let len = digits.length;
    for (let i = 0; i < 4; i++) {
        OTP += digits[Math.floor(Math.random() * len)];
    }
    return OTP;
}

function sendOTPByEmail(email, OTP) {
    const mailOptions = {
        from: 'simecid.services@gmail.com',
        to: email,
        subject: 'OTP Verification',
        text: `Your SIMECID verification code is: ${OTP}`
    };

    fetch('/send-otp', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(mailOptions)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to send OTP');
            }
            console.log('Email sent successfully');
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Failed to send OTP');
        });
    }
