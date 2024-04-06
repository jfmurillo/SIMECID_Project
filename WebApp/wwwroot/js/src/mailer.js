
window.onload = function () {
    localStorage.clear();
};

document.getElementById("entEmail").addEventListener("submit", function (event) {
    debugger
    event.preventDefault();


    let email = document.getElementById("email").value;
    /*const userEmail = localStorage.setItem("userEmail", userEmail);*/


    if (email === "") {
        alert("Please fill email value");
        return;
    }

    if (!email) {
        alert("No email address was found.");
    }

    let keysAuth = {
        email: email,
        //password: newPassword
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
