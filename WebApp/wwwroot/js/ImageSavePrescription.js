$(document).ready(function () {
    $('#uploadForm').submit(function (e) {
        e.preventDefault(); // Evita el envío del formulario por defecto

        var formData = new FormData($(this)[0]);

        $.ajax({
            url: 'Prescription/UploadFile',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                // Maneja la respuesta exitosa (opcional)

            },
            error: function (xhr, status, error) {
                // Maneja el error (opcional)

            }
        });
    });
});