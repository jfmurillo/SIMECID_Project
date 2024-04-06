const express = require('express');
const nodemailer = require('nodemailer');
const bodyParser = require('body-parser');
const cors = require('cors');

const app = express();
const PORT = 7144;

app.use(bodyParser.json());
app.use(cors({
    origin: 'https://localhost:7144'
}));

const transporter = nodemailer.createTransport({
    service: 'gmail',
    auth: {
        user: 'simecid.services@gmail.com',
        pass: 'Cenfotec123!'
    }
});

app.post('/send-otp', (req, res) => {
    const { email, otp } = req.body;

    if (!email || !otp) {
        return res.status(400).json({ error: 'Email address and OTP are required.' });
    }

    const mailOptions = {
        from: 'simecid.services@gmail.com',
        to: email,
        subject: 'Verificaci�n OTP',
        text: `Tu c�digo de verificaci�n OTP es: ${otp}`
    };

    transporter.sendMail(mailOptions, function (error, info) {
        if (error) {
            console.log(error);
            return res.status(500).json({ error: 'Error al intentar enviar el correo electr�nico' });
        } else {
            console.log('Correo electr�nico enviado: ' + info.response);
            return res.status(200).json({ message: 'Correo electr�nico enviado exitosamente.' });
        }
    });
});

// Inicia el servidor
app.listen(PORT, () => {
    console.log(`Servidor en ejecuci�n en el puerto ${PORT}`);
});
